using ArmyTech.Interfaces;
using ArmyTech.Models;
using ArmyTech.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArmyTech.Services
{
    public class InvoiceDetailService : IInvoiceDetailService
    {
        private readonly ArmyTechTaskContext _context;
        private readonly IInvoiceHeaderService _invoiceHeaderService;
        public InvoiceDetailService(ArmyTechTaskContext context, IInvoiceHeaderService invoiceHeaderService)
        {
            _invoiceHeaderService = invoiceHeaderService;
            _context = context;
        }

      

        public InvoiceDataDto GetEditModel(int invoiceId)
        {
            var model = new InvoiceDataDto();
            var invoiceHeader = _context.InvoiceHeaders.FirstOrDefault(x => x.Id == invoiceId);
            if (invoiceHeader != null)
            {
                var invoiceDetails = _context.InvoiceDetails.Where(x => x.InvoiceHeaderId == invoiceHeader.Id).Select(x => new InvoiceDetailDto
                {
                    ItemName = x.ItemName,
                    Price = x.ItemPrice,
                    Quantity = x.ItemCount,
                    TotalValue = Math.Round((x.ItemPrice * x.ItemCount))
                }).ToList();
                if (invoiceDetails.Any())
                {
                    model.InvoiceId = invoiceHeader.Id;
                    model.InvoiceDetails = invoiceDetails;
                    model.BranchId = invoiceHeader.BranchId;
                    model.CashierId = invoiceHeader.CashierId.GetValueOrDefault();
                    model.CustomerName = invoiceHeader.CustomerName;
                }
            }
            return model;
        }

        public List<InvoiceDataDto> GetInvoicesDetails(List<InvoiceDataDto> invoices)
        {
            var invoiceDetails = new List<InvoiceDetailDto>();
            foreach (var invoice in invoices)
            {
                invoiceDetails = _context.InvoiceDetails.Where(x => x.InvoiceHeaderId == invoice.InvoiceId).Select(x => new InvoiceDetailDto
                {
                    ItemName = x.ItemName,
                    Price = x.ItemPrice,
                    Quantity = x.ItemCount,
                    TotalValue = Math.Round(x.ItemPrice * x.ItemCount, 2)
                }).ToList();
                invoice.InvoiceDetails = invoiceDetails;
            }
            return invoices;
        }

        public SuccessMessageDto SaveInvoice(InvoiceDataDto model)
        {
            var response = new SuccessMessageDto();
            var headerResponse = _invoiceHeaderService.SaveInvoiceHeader(model);
            var invoiceDetailList = new List<InvoiceDetail>();
            if (headerResponse.Success)
            {
                if (model.InvoiceId > 0)
                {
                    foreach (var item in model.InvoiceDetails)
                    {
                        var invoiceDetail = new InvoiceDetail();
                        invoiceDetail.InvoiceHeaderId = _context.InvoiceHeaders.Select(x => x.Id).Max();
                        invoiceDetail.ItemName = item.ItemName;
                        invoiceDetail.ItemPrice = item.Price;
                        invoiceDetail.ItemCount = item.Quantity;
                        invoiceDetailList.Add(invoiceDetail);
                    }
                    DeleteInvoiceDetails(model.InvoiceId);
                }
                else
                {
                    foreach (var item in model.InvoiceDetails)
                    {
                        var invoiceDetail = new InvoiceDetail();
                        invoiceDetail.InvoiceHeaderId = _context.InvoiceHeaders.Select(x => x.Id).Max();
                        invoiceDetail.ItemName = item.ItemName;
                        invoiceDetail.ItemPrice = item.Price;
                        invoiceDetail.ItemCount = item.Quantity;
                        invoiceDetailList.Add(invoiceDetail);
                    }
                }
                _context.InvoiceDetails.AddRange(invoiceDetailList);
                _context.SaveChanges();
                response.Success = true;

            }
            else
            {
                response.Success = false;
            }
            return response;
        }

        public SuccessMessageDto DeleteInvoice(long invoiceId)
        {
            var successModel = new SuccessMessageDto();
            var invoice = _context.InvoiceHeaders.Where(x => x.Id == invoiceId).FirstOrDefault();
            if (invoice != null)
            {
                var invoiceDetails = _context.InvoiceDetails.Where(x => x.InvoiceHeaderId == invoice.Id).ToList();
                if (invoiceDetails.Any())
                {
                    _context.InvoiceDetails.RemoveRange(invoiceDetails);
                    _context.SaveChanges();
                    _context.InvoiceHeaders.Remove(invoice);
                    _context.SaveChanges();
                    successModel.Success = true;
                    successModel.Message = "تم حذف الفاتورة بنجاح";
                }
                else
                {
                    successModel.Success = false;
                    successModel.Message = "حدث خطأ اثناء حذف الفاتورة";
                }
            }
            else {
                successModel.Success = false;
                successModel.Message = "حدث خطأ اثناء حذف الفاتورة";
            }
            return successModel;
        }

        private SuccessMessageDto DeleteInvoiceDetails(long invoiceId)
        {
            var successModel = new SuccessMessageDto();
            var invoiceDetailsInDb = _context.InvoiceDetails.Where(x => x.InvoiceHeaderId == invoiceId).ToList();
            if (invoiceDetailsInDb.Any())
            {
                _context.InvoiceDetails.RemoveRange(invoiceDetailsInDb);
                _context.SaveChanges();
                successModel.Success = true;
            }
            else
            {
                successModel.Success = false;
            }
            return successModel;
        }
    }
}
