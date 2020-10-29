using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceLayer.Models.FrontEnd
{
    public class ShoppingCartViewModel
    {
        [Key]
        public int ProductID { get; set; }
        
        [Display(Name = "نام محصول")]
        public string ProductTitle { get; set; }
        
        [Display(Name = "قیمت محصول")]
        [DisplayFormat(DataFormatString = "{0:#,0 ریال}")]
        public int ProductPrice { get; set; }

        [Display(Name = "تعداد")]
        [DisplayFormat(DataFormatString = "{0:#,0 عدد}")]
        public int ProductCount { get; set; }

        [Display(Name = "جمع")]
        [DisplayFormat(DataFormatString = "{0:#,0 ریال}")]
        public int RowTotal { get; set; }
    }
}