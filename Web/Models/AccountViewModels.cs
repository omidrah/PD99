using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }


    }
    public class ConfirmEmailModel
    {
        
        public string ActivationLink { get; set; }
        public string SiteTitle { get; set; }
        public string SiteDescription { get; set; }
    }
    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool? RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Provider { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "کد احراز هویت")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "مرا بخاطر بسپار؟")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام کاربری(ایمیل)")]
        [EmailAddress(ErrorMessage = "ایمیل با فرمت صحیح وارد نشده است")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "مرا بخاطر بسپار؟")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

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
        [MaxLength(11, ErrorMessage = "موبایل 11 رقمی است"), MinLength(11, ErrorMessage = "موبایل 11 رقمی است")]
        [Remote("IsMobileAvailable", "Account", ErrorMessage = "این موبایل قبلا ثبت شده است")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10,ErrorMessage ="کدملی 10 رقمی است"),MinLength(10,ErrorMessage ="کدملی 10 رقمی است")]
        [Display(Name = "کدملی")]
        [Remote("IsNationalCodeAvailable", "Account", ErrorMessage = "این کدملی قبلا ثبت شده است")]
        public string NationCode { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "کدپستی 10 رقمی است"), MinLength(10, ErrorMessage = "کدپستی 10 رقمی است")]
        [Display(Name = "کدپستی")]
        public string PostalCode { get; set; }

        [Display(Name = "عکس")]
        public string PicPath { get; set; }

        [Display(Name = "عکس مدارک")]
        public string PicDoc { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress]
        [Remote("IsEmailAvailable", "Account", ErrorMessage = "این ایمیل قبلا ثبت شده است")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "حداقل طول {0} باید 6 کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور ")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "عدم یکسان بودن رمز عبور و تکرار آن")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "نوع سهمیه")]
        public int CustomerTypeId { get; set; }
        public string CustomerType { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "حداقل طول {0}  باید {1} کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "عدم یکسان بودن رمز عبور وتکرار آن")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
    }
}
