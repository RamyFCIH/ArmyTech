using ArmyTech.ViewModels;
using System.Collections.Generic;

namespace ArmyTech.Interfaces
{
    public interface IInvoiceHeaderService
    {
        List<InvoiceDataDto> GetAllInvoices();
        AddNewItemDto GetNewInvoiceData();

        SuccessMessageDto SaveInvoiceHeader(InvoiceDataDto model);
    }
}
