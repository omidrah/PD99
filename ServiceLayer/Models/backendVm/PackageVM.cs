using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Models
{
    [DisplayName("بسته")] 
    public class PackageVM
    {
        public int Id { get; set; }
        [Display(Name = "نام بسته اصلی")]
        public int? ParentId { get; set; }
        [Display(Name = "نام بسته")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PackageTitle { get; set; }
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
        [Display(Name = "وضعیت")]
        public byte? Status { get; set; }
        [Display(Name = "نوع بسته")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool PackageType { get; set; }
        [Display(Name = "فعال")]
        public bool? IsActive { get; set; }
        [Display(Name = "شرح کوتاه")]
        public string PackageDescription { get; set; }
        [Display(Name = "توضیحات")]
        public string Summery { get; set; }

        [Display(Name = "کلمات کلیدی")]
        public string KeyWord { get; set; }
        [Display(Name = "قیمت")]
        public int? Cost { get; set; }
        [Display(Name = "وزن به گرم")]
        public int? Weight { get; set; }
        [Display(Name = "درصد تخفیف")]
        [Range(0, 100, ErrorMessage = "عدد وارد شده باید بین 0 تا 100 باشد")]

        public byte? OffPercent { get; set; }
        [Display(Name = "کلمات کلیدی")]
        public string tags { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }

    }
}