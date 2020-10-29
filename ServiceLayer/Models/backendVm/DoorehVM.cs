using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Models
{
    [DisplayName("دوره")]
  //  [DisplayPluralName("دوره ها")]
    public class DoorehVM1
    {
        #region Dooreh
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [ScaffoldColumn(false)]
        public int? Pid { get; set; }

        [Display(Name = "عنوان دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        // [StringLength(50, MinimumLength = 0, ErrorMessage = "نام باید حداکثر 250 کاراکتر باشد")]
        public string DoorehTitle { get; set; }
        [Display(Name = "تاریخ شروع دوره")]
        public DateTime? DoorehStartDate { get; set; }
        [Display(Name = "تاریخ پایان دوره")]
        public DateTime? DoorehEndDate { get; set; }
        #endregion
        #region DoorehDorus
        //public int DoorehDorusId { get; set; }
        //[Display(Name = "عنوان درس")]
        //public string  dorusTitle { get; set; }
        //public int DourseId { get; set; }
        //public int DoorehaId { get; set; }
        //[Display(Name = "نام استاد")]
        //public string MasterName { get; set; }
        //public int MasterId { get; set; }
        //[Display(Name = "تاریخ شروع کلاس")]
        //public DateTime? StartDate { get; set; }
        //[Display(Name = "تاریخ پایان کلاس")]
        //public DateTime? EndDate { get; set; }
        //[Display(Name = "محل برگزای")]
        //public string MahaleBargozari { get; set; }
        //public int? MahaleBargozariId { get; set; }
        //public bool? HasVideo { get; set; }
        #endregion
        [Display(Name = "فعال")]
        public bool? IsActive { get; set; }
        [Display(Name = "آنلاین")]
        public bool? IsOnline { get; set; }
        [Display(Name = "حضوری")]
        public bool? IsHozori { get; set; }

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