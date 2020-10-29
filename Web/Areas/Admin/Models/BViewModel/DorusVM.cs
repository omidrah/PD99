using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ServiceLayer.Models
{
    [DisplayName("درس")]

    public class DorusVM
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Display(Name = "نام درس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "نام باید حداکثر 250 کاراکتر باشد")]
        [Remote("CheckDorousTitleExists", "Doruses", ErrorMessage = "عنوان درس تکراری است")]
        public string Title { get; set; }
        [Display(Name = "مدت به ساعت")]
        public int? ModatByHours { get; set; }
        [Display(Name = "فعال")]
        public bool? IsActive { get; set; }
        [ScaffoldColumn(false)]
        public bool? IsDeleted { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? ModifyDate { get; set; }
        [ScaffoldColumn(false)]
        public int? CreatedBy { get; set; }
        [ScaffoldColumn(false)]
        public int? ModifyBy { get; set; }

    }
}