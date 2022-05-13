using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArmyTech.ViewModels
{
    public class AddNewItemDto
    {
        public long InvoiceId { get; set; }

        // integers

        [Display(Name ="الكمية")]
        public int Quantity { get; set; }

        [Display(Name = "الفرع")]
        public int BranchId { get; set; }

        [Display(Name = "الكاشير")]
        public int CashierId { get; set; }
      
  

        [Display(Name = "السعر")]
        public decimal Price { get; set; }

        // strings

        [Display(Name = "العميل")]
        public string CustomerName { get; set; }

        [Display(Name = "الصنف")]
        public string ItemName { get; set; }

        public string CashierName { get; set; }
        public string BranchName { get; set; }

        // Lists
        public List<SelectListItem> Branches { get; set; }
        public List<SelectListItem> Cashiers { get; set; }
    }
}
