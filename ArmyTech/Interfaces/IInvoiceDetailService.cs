using ArmyTech.ViewModels;
using System.Collections.Generic;

namespace ArmyTech.Interfaces
{
    public interface IInvoiceDetailService
    {
        List<InvoiceDataDto> GetInvoicesDetails(List<InvoiceDataDto> invoices);
        InvoiceDataDto GetEditModel(int invoiceId);
        SuccessMessageDto SaveInvoice(InvoiceDataDto model);
        SuccessMessageDto DeleteInvoice(long invoiceId);
    }
}
