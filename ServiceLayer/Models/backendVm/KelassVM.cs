using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
   public class KelassVM
    {
        public int KelassId { get; set; }
        [Display(Name = "عنوان درس")]
        public string dorusTitle { get; set; }
        [Display(Name = "عنوان درس")]
        public int DourseId { get; set; }
        [Display(Name = "نام استاد")]
        public string MasterName { get; set; }
        [Display(Name = "نام استاد")]
        public int MasterId { get; set; }
        [Display(Name = "تاریخ شروع کلاس")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "تاریخ پایان کلاس")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "محل برگزای")]
        public string MahaleBargozari { get; set; }
        [Display(Name = "محل برگزای")]
        public int? MahaleBargozariId { get; set; }
        [Display(Name = "نوع کلاس")]
        public string KelassType { get; set; }
        [Display(Name = "قیمت")]
        public int? Cost { get; set; }
        public bool? HasVideo { get; set; }
        [Display(Name = "آنلاین")]
        public bool? IsOnline { get; set; }
        [Display(Name = "حضوری")]
        public bool? IsHozori { get; set; }

        [Display(Name = "فعال")]
        public bool? IsActive { get; set; }
    }
}
