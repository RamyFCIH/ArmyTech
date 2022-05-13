using ArmyTech.Models;
using ArmyTech.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArmyTech.Controllers
{
    public class InvoicesController : Controller
    {

        private readonly ArmyTechTaskContext _context;
        public InvoicesController(ArmyTechTaskContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var invoices = (from invoiceHeader in _context.InvoiceHeaders.AsQueryable()
                            from Branch in _context.Branches.AsQueryable().Where(x => x.Id == invoiceHeader.BranchId)
                            from cashier in _context.Cashiers.AsQueryable().Where(x => x.Id == invoiceHeader.CashierId)
                            select new InvoiceDataDto
                            {
                                InvoiceId = invoiceHeader.Id,
                                BranchName = Branch.BranchName,
                                CashierName = cashier.CashierName,
                                CustomerName = invoiceHeader.CustomerName,
                                InvoiceDate = invoiceHeader.Invoicedate
                            }).ToList();

            foreach (var invoice in invoices)
            {
                var invoiceDetails = _context.InvoiceDetails.Where(x => x.InvoiceHeaderId == invoice.InvoiceId).Select(x => new InvoiceDetailDto
                {
                    ItemName = x.ItemName,
                    Price = x.ItemPrice,
                    Quantity = x.ItemCount,
                    TotalValue = Math.Round(x.ItemPrice * x.ItemCount, 2)
                }).ToList();
                invoice.InvoiceDetails = invoiceDetails;
            }
            var model = new InvoiceMainDataDto
            {
                InvoiceData = invoices.ToList(),
                AddNewItemDto = GetNewItemData()
            };
            return View(model);
        }


        [HttpPost]
        public IActionResult Save(InvoiceDataDto model)
        {
            try
            {
                var invoiceHeader = new InvoiceHeader
                {
                    CustomerName = model.CustomerName,
                    Invoicedate = DateTime.Now,
                    CashierId = model.CashierId,
                    BranchId = model.BranchId
                };
                _context.InvoiceHeaders.Add(invoiceHeader);
                _context.SaveChanges();
            }
            catch (Exception e)
            {

            }
            return RedirectToAction("Index","Invoices");
        }

        private AddNewItemDto GetNewItemData()
        {
            var branches = _context.Branches.Select(x => new SelectListItem
            {
                Text = x.BranchName,
                Value = x.Id.ToString()
            }).ToList();
            var cashiers = _context.Cashiers.Select(x => new SelectListItem
            {
                Text = x.CashierName,
                Value = x.Id.ToString()
            }).ToList();
            var model = new AddNewItemDto
            {
                Branches = branches,
                Cashiers = cashiers,
                Price = 0,
                Quantity = 0
            };
            return model;
        }
    }
}
