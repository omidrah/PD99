using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Areas.Admin.Models.BViewModel
{
    public class EditCustomerVm
    {
        [Key]
        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام خانوادگی")]
        public string Family { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "تلفن ثابت")]
        public string Tel { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "تلفن همراه")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "کدملی 10 رقمی است"), MinLength(10, ErrorMessage = "کدملی 10 رقمی است")]
        [Display(Name = "کدملی")]
        public string nationCode { get; set; }

        [Display(Name = "آواتار")]
        public string PicPath { get; set; }


        [Display(Name = "عکس مدارک")]
        public string PicDoc { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[StringLength(100, ErrorMessage = "حداقل طول {0} باید 6 کاراکتر باشد", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }


        [DisplayName("نوع مشتری")]
        public string customerTypeTitle { get; set; }
        //[ScaffoldColumn(false)]
        [Required(ErrorMessage = "لطفا{0} را مشخص کنید")]
        public byte customerTypeId { get; set; }
        [Required]
        [DisplayName("فعال/غیرفعال")]
        public bool IsActive { get; set; }

        [DisplayName("مدرک مورد تایید است؟")]
        public bool IsMadrakTaeed { get; set; }

        [DisplayName("تاریخ تولد")]
        public DateTime? birthDate { get; set; }
    }
}