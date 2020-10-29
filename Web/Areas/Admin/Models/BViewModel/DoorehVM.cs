using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Models
{
    [DisplayName("دوره")]
    //  [DisplayPluralName("دوره ها")] 
    public class DoorehVM
    {
        #region Dooreh
        //  [ScaffoldColumn(false)]
        public int Id { get; set; }
        //  [ScaffoldColumn(false)]
        public int? Pid { get; set; }
        [Display(Name = "نوع دوره")]
        public string DoorehType { get; set; }

        [Display(Name = "عنوان دوره")]
        // [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        // [StringLength(50, MinimumLength = 0, ErrorMessage = "نام باید حداکثر 250 کاراکتر باشد")]
        public string DoorehTitle { get; set; }
        [Display(Name = "تاریخ شروع دوره")]
        [DisplayFormat(DataFormatString = "{0: dddd, dd MMMM yyyy}")]
        public DateTime? DoorehStartDate { get; set; }
        [Display(Name = "تاریخ پایان دوره")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime? DoorehEndDate { get; set; }
        [Display(Name = "آنلاین")]
        public bool? HasOnline { get; set; }
        [Display(Name = "حضوری")]
        public bool? HasHozori { get; set; }
        [StringLength(350)]
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
        [Display(Name = "محتوای دوره")]
        // [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(500, MinimumLength = 0, ErrorMessage = "محتوا باید حداکثر 500 کاراکتر باشد")]
        // محتوای آموزشی دوره
        public string Content { get; set; }
        [Display(Name = "کلمات کلیدی")]
        public string tags { get; set; }

        [Display(Name = "هدف دوره")]
        // [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(1000, MinimumLength = 0, ErrorMessage = "هدف دوره باید حداکثر 1000 کاراکتر باشد")]

        // هدف از برکزاری دوره
        public string Goal { get; set; }

        [Display(Name = "پیشنیاز دوره")]
        // [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(1000, MinimumLength = 0, ErrorMessage = "پیشنیاز باید حداکثر 1000 کاراکتر باشد")]

        // پیش نیازهای دوره
        public string Pishniaz { get; set; }
        [Display(Name = "فهرست دوره")]
        // [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(2000, MinimumLength = 0, ErrorMessage = "فهرست باید حداکثر 2000 کاراکتر باشد")]

        // فهرست دوره
        public string Appendix { get; set; }
        [Display(Name = "روزهای برگزاری دوره")]
        // [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(1000, MinimumLength = 0, ErrorMessage = "طول باید حداکثر 1000 کاراکتر باشد")]

        // روزهای برگزاری دوره: شنبه و دوشنبه
        public string BargozariDay { get; set; }
        [Display(Name = "قیمت دوره")]
        // [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        // قیمت دوره
        public int? Cost { get; set; }
        [Display(Name = "درصد تخفیف")]
        [Range(0, 100, ErrorMessage = "عدد وارد شده باید بین 0 تا 100 باشد")]

        public byte? OffPercent { get; set; }
        [Display(Name = "ساعات برگزاری دوره")]
        [StringLength(250, MinimumLength = 0, ErrorMessage = "نام باید حداکثر 250 کاراکتر باشد")]
        //[StringLength(250)]
        // ساعت برگزاری دوره
        public string BargozariTime { get; set; }
        [Display(Name = "لغات کلیدی")]
        // [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(300, MinimumLength = 0, ErrorMessage = "نام باید حداکثر 300 کاراکتر باشد")]
        public string KeyWord { get; set; }

        public int KelassId { get; set; }
        public int KelassDoorehId { get; set; }
        #endregion
        #region DoorehDorus
        [Display(Name = "عنوان درس")]
        public int DoorehDorusId { get; set; }
        [Display(Name = "عنوان درس")]
        public string dorusTitle { get; set; }
        public int DourseId { get; set; }
        //   public int DoorehaId { get; set; }
        [Display(Name = "نام استاد")]
        public string MasterName { get; set; }
        [Display(Name = "نام استاد")]
        public int MasterId { get; set; }
        [Display(Name = "تاریخ شروع کلاس")]
        [DisplayFormat(DataFormatString = "{0: dddd, dd MMMM yyyy}")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "تاریخ پایان کلاس")]
        [DisplayFormat(DataFormatString = "{0: dddd, dd MMMM yyyy}")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "محل برگزای")]
        public string MahaleBargozari { get; set; }
        [Display(Name = "محل برگزاری")]
        public int? MahaleBargozariId { get; set; }
        public bool? HasVideo { get; set; }
        [Display(Name = "آنلاین")]
        public bool? IsOnline { get; set; }
        [Display(Name = "حضوری")]
        public bool? IsHozori { get; set; }
        #endregion
        [Display(Name = "فعال")]
        public bool? IsActive { get; set; }

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