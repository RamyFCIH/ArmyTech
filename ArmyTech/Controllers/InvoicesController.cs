using ArmyTech.Interfaces;
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

        private readonly IDatabaseTransaction _transaction;
        private readonly IInvoiceHeaderService _invoiceheaderService;
        private readonly IInvoiceDetailService _invoicedetailService;
        public InvoicesController( IDatabaseTransaction transaction, IInvoiceHeaderService invoiceHeaderService, IInvoiceDetailService invoiceDetailService)
        {
            _invoicedetailService = invoiceDetailService;
            _invoiceheaderService = invoiceHeaderService;
            _transaction = transaction;
        }
        public IActionResult Index()
        {
            var model = GetIndexModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _invoicedetailService.GetEditModel(id);
            return Json(new { data = model });
        }

        [HttpPost]
        public IActionResult Save(InvoiceDataDto model)
        {

            var successModel = new SuccessMessageDto();
            try
            {
                if (ModelState.IsValid)
                {
                    successModel = _invoicedetailService.SaveInvoice(model);
                    _transaction.Commit();
                }
                else {
                    return View("Index", GetIndexModel());
                }
            }
            catch (Exception e)
            {
                _transaction.Rollback();
            }
            return View("Index", GetIndexModel());
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var successModel = new SuccessMessageDto();
            try
            {
                successModel = _invoicedetailService.DeleteInvoice(id);
                _transaction.Commit();
            }
            catch (Exception e)
            {
                _transaction.Rollback();
                successModel.Success = false;
                successModel.Message = "حدث خطأ اثناء حذف الفاتورة";
            }
            return Json(new { success = successModel.Success, message = successModel.Message });
        }

        private InvoiceMainDataDto GetIndexModel() {
            var invoices = _invoiceheaderService.GetAllInvoices();
            var model = new InvoiceMainDataDto
            {
                InvoiceData = _invoicedetailService.GetInvoicesDetails(invoices),
                AddNewItemDto = _invoiceheaderService.GetNewInvoiceData()
            };
            return model;
        }

    }
}
