using ArmyTech.Interfaces;
using ArmyTech.Models;
using ArmyTech.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ArmyTech.Services
{
    public class InvoiceHeaderService : IInvoiceHeaderService
    {
        private readonly ArmyTechTaskContext _context;
        public InvoiceHeaderService(ArmyTechTaskContext context)
        {
            _context = context;
        }

        public SuccessMessageDto SaveInvoiceHeader(InvoiceDataDto model)
        {
            var succcessModel = new SuccessMessageDto();
            if (model.InvoiceId <= 0)
            {
                var invoiceHeader = new InvoiceHeader
                {
                    CustomerName = model.CustomerName,
                    Invoicedate = DateTime.Now,
                    CashierId = model.CashierId,
                    BranchId = model.BranchId
                };
                _context.InvoiceHeaders.Add(invoiceHeader);
            }
            else
            {
                var invoiceHeaderInDb = _context.InvoiceHeaders.FirstOrDefault(x => x.Id == model.InvoiceId);
                if (invoiceHeaderInDb != null)
                {
                    invoiceHeaderInDb.BranchId = model.BranchId;
                    invoiceHeaderInDb.CashierId = model.CashierId;
                    invoiceHeaderInDb.CustomerName = model.CustomerName;
                    _context.InvoiceHeaders.Update(invoiceHeaderInDb);
                }
            }
            succcessModel.Success = true;
            _context.SaveChanges();
            return succcessModel;
        }

        public List<InvoiceDataDto> GetAllInvoices()
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
            return invoices;
        }

        public AddNewItemDto GetNewInvoiceData()
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
                Cashiers = cashiers
            };
            return model;
        }
    }
}
