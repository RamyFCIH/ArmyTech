using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArmyTech.ViewModels
{

 
    public class InvoiceDataDto
    {
        [Display(Name = "كود الفاتورة")]
        public long InvoiceId { get; set; }


        [Display(Name = "العميل")]
        public string CustomerName { get; set; }


        [Display(Name = "الكاشير")]
        public string CashierName { get; set; }

        [Display(Name = "الفرع")]
        public string BranchName { get; set; }


        [Display(Name = "تاريخ الفاتورة")]
        public DateTime InvoiceDate { get; set; }

    }

    public class InvoiceMainDataDto
    {
        public IEnumerable<InvoiceDataDto> InvoiceData { get; set; }
        public AddNewItemDto AddNewItemDto { get; set; }
    }
}
