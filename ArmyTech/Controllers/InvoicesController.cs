using ArmyTech.Models;
using ArmyTech.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
            var data = from invoiceHeader in _context.InvoiceHeaders.AsQueryable()
                       from invoiceDetail in _context.InvoiceDetails.AsQueryable().Where(x => x.InvoiceHeaderId == invoiceHeader.Id)
                       from Branch in _context.Branches.AsQueryable().Where(x => x.Id == invoiceHeader.BranchId)
                       from cashier in _context.Cashiers.AsQueryable().Where(x => x.Id == invoiceHeader.CashierId)
                       select new InvoiceDataDto
                       {
                           InvoiceId = invoiceHeader.Id,
                           BranchName = Branch.BranchName,
                           CashierName = cashier.CashierName,
                           CustomerName = invoiceHeader.CustomerName,
                           InvoiceDate = invoiceHeader.Invoicedate
                       };
            var model = new InvoiceMainDataDto
            {
                InvoiceData = data.ToList(),
                AddNewItemDto = GetNewItemData()
            };
            return View(model);
        }


        private AddNewItemDto GetNewItemData()
        {
            var branches = _context.Branches.Select(x => new BranchDto
            {
                BranchId = x.Id,
                BranchName = x.BranchName
            }).ToList();
            var cashiers = _context.Cashiers.Select(x => new CashierDto
            {
                CashierId = x.Id,
                CashierName = x.CashierName
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
