using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArmyTech.ViewModels
{
    public class AddNewItemDto
    {
        public long InvoiceId { get; set; }

        [Display(Name = "الفرع")]

        [Required(ErrorMessage = "من فضلك اختر الفرع")]
        public int BranchId { get; set; }

        [Display(Name = "الكاشير")]
        [Required(ErrorMessage = "من فضلك اختر الكاشير")]
        public int CashierId { get; set; }
      

        // strings

        [Display(Name = "العميل")]
        [Required(ErrorMessage = "من فضلك ادخل اسم العميل")]
        public string CustomerName { get; set; }

        public string CashierName { get; set; }
        public string BranchName { get; set; }

        // Lists
        public List<SelectListItem> Branches { get; set; }
        public List<SelectListItem> Cashiers { get; set; }
    }
}
