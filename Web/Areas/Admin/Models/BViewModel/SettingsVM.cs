using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SettingsVM
    {
        [ScaffoldColumn(false)]
        public byte Id { get; set; }

        [Display(Name = "هزینه پایه پستی")]
        public int PostBasePrice { get; set; }
        [Display(Name = "هزینه پستی به ازای یک کیلوگرم")]
        
        public int PostPricePerUnit { get; set; }
        [ScaffoldColumn(false)]
        public bool? IsDeleted { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? ModifiedDate { get; set; }
        [ScaffoldColumn(false)]
        public int? CreatedBy { get; set; }
        [ScaffoldColumn(false)]
        public int? ModifiedBy { get; set; }

    }
}