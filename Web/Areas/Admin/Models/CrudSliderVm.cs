using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Web.Areas.Admin.Models
{
    public class CrudSliderVm
    {        
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string imgTitle { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string imgName { get; set; }
        [Display(Name = "شرح")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [AllowHtml]
        [UIHint("RichText")]
        public string imgDesc { get; set; }
        [Display(Name = "اولویت نمایش")]

        public int? imgPriority { get; set; }
        [Display(Name = "فعال /غیر فعال")]

        public bool? imgIsActive { get; set; }
        [Display(Name = "عرض")]

        public int? imgWidth { get; set; }
        [Display(Name = "طول")]

        public int? imgHeight { get; set; }
        [Display(Name = "پسوند")]
        public string imgExtension { get; set; }
        [Display(Name = "مقصد پس از کلیک")]
        public string NavigateUrl { get; set; }
        [Display(Name = "عکس ")]
        public string imgPath { get; set; }
        [Display(Name = "عکس کوچک")]
        public string imgPathThumb { get; set; }
    }
}