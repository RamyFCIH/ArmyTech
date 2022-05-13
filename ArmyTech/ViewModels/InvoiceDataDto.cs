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
        [Required(ErrorMessage ="من فضلك ادخل اسم العميل")]
        public string CustomerName { get; set; }

        [Display(Name = "الكاشير")]
        public string CashierName { get; set; }

        [Display(Name = "الفرع")]
        public string BranchName { get; set; }

        [Required(ErrorMessage = "من فضلك اختر الكاشير")]
        public int CashierId { get; set; }

        [Required(ErrorMessage = "من فضلك اختر الفرع")]
        public int BranchId { get; set; }

        [Display(Name = "تاريخ الفاتورة")]
        public DateTime InvoiceDate { get; set; }
        public List<InvoiceDetailDto> InvoiceDetails { get; set; }

    }

    public class InvoiceMainDataDto
    {
        public IEnumerable<InvoiceDataDto> InvoiceData { get; set; }
        public AddNewItemDto AddNewItemDto { get; set; }
    }

    public class InvoiceDetailDto
    {
        public string ItemName { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double TotalValue { get; set; }

    }
}
