using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Admin.Models
{
    public class UserProfile
    {
        public int userId { get; set; }

        [Display(Name = "نقش کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [UIHint("UsrRole")]
        public int RoleId { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress]
        [Remote("IsEmailAvailable", "Account", ErrorMessage = "این ایمیل قبلا ثبت شده است")]
       // [Display(Name = "ایمیل")]
        public string username { get; set; }

        [Display(Name = "نقش")]        
        public string RoleTitle { get; set; }

        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }        

        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]     
        public string name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]        
        public string family { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "کدملی 10 رقمی است"), MinLength(10, ErrorMessage = "کدملی 10 رقمی است")]
        [Display(Name = "کدملی")]
        [Remote("IsNationalCodeAvailable", "Account", ErrorMessage = "این کدملی قبلا ثبت شده است")]
        public string codeMelli { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "تلفن ثابت")]
        public string Tel { get; set; }
        [Display(Name = "کدپستی")]
        [MaxLength(10, ErrorMessage = "کدپستی حداکثر10 کاراکتر می باشد")]
        [MinLength(10, ErrorMessage = "کدپستی حداقل10 کاراکتر می باشد")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "تلفن همراه")]
        [MaxLength(11, ErrorMessage = "موبایل 11 رقمی است"), MinLength(11, ErrorMessage = "موبایل 11 رقمی است")]
        [Remote("IsMobileAvailable", "Account", ErrorMessage = "این موبایل قبلا ثبت شده است")]
        public string Mobile { get; set; }
        [Display(Name = "نام پدر")]
        public string FatherName { get; set; }

        [Display(Name = "تصویر کاربر")]
        public string picPath { get; set; }

        [Display(Name = "شهر")]
        public string City { get; set; }

    }

}