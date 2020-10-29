using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
//using System.Web.Mvc;

namespace Web.Models
{
    [DisplayName("پرسنل")]

    public class EditPersonelVM
    {

        #region user
        public int UserId { get; set; }
        [Display(Name = "نام")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "نام باید حداکثر 50 کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "نام باید حداکثر 50 کاراکتر باشد")]
        public string Family { get; set; }

        [Display(Name = "کدملی")]
        [StringLength(10, MinimumLength = 0, ErrorMessage = "نام باید حداکثر 250 کاراکتر باشد")]
        public string NationCode { get; set; }
        [Display(Name = "موبایل")]
        [StringLength(11, MinimumLength = 0, ErrorMessage = "{0}باید حداکثر 11 کاراکتر باشد")]
        public string Mobile { get; set; }
        [Display(Name = "تلفن")]
        [StringLength(11, MinimumLength = 0, ErrorMessage = "تلفن باید حداکثر 250 کاراکتر باشد")]
        public string Tel { get; set; }
        [Display(Name = "آدرس")]
        [StringLength(650, MinimumLength = 0, ErrorMessage = "آدرس باید حداکثر 650 کاراکتر باشد")]
        public string Address { get; set; }
        [Display(Name = "نام کاربری")]
        [StringLength(150, MinimumLength = 0, ErrorMessage = "نام باید حداکثر 150 کاراکتر باشد")]
        public string Username { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress]
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
        [Display(Name = "تاریخ تولد")]
        public DateTime? BirthDate { get; set; }
        public int? UserTypeId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsLocked { get; set; }
        public DateTime? LastDateLogin { get; set; }
        public byte? UserStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        #endregion
        #region personel
        [Display(Name = "شماره پرسنلی")]
        public string PersonliNo { get; set; }
        [Display(Name = "سن")]

        public int? Age { get; set; }
        [Display(Name = "بیمه")]

        public bool? HasInsurance { get; set; }
        [Display(Name = "رسمی")]

        public bool? IsRasmi { get; set; }

        [Display(Name = "تاریخ حقوق")]
        public DateTime? SalaryDate { get; set; }

        //[StringLength(150)]
        [Display(Name = "مدرک تحصیلی")]
        public string MadrakTashili { get; set; }

        [Display(Name = "نقش کاربر")]
        //public int RoleId { get; set; }
        //public string RoleType { get; set; }
        public IEnumerable<SelectListItem> RolesList { get; set; }
        #endregion
        public string ImgThumbPath { get; set; }

        public string PicPath { get; set; }

    }

    [DisplayName("پرسنل")]

    public class EditPersonelVMWithoutPass
    {

        #region user
        public int UserId { get; set; }
        [Display(Name = "نام")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "نام باید حداکثر 50 کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "نام باید حداکثر 50 کاراکتر باشد")]
        public string Family { get; set; }

        [Display(Name = "کدملی")]
        [StringLength(10, MinimumLength = 0, ErrorMessage = "نام باید حداکثر 250 کاراکتر باشد")]
        public string NationCode { get; set; }
        [Display(Name = "موبایل")]
        [StringLength(11, MinimumLength = 0, ErrorMessage = "{0}باید حداکثر 11 کاراکتر باشد")]
        public string Mobile { get; set; }
        [Display(Name = "تلفن")]
        [StringLength(11, MinimumLength = 0, ErrorMessage = "تلفن باید حداکثر 250 کاراکتر باشد")]
        public string Tel { get; set; }
        [Display(Name = "آدرس")]
        [StringLength(650, MinimumLength = 0, ErrorMessage = "آدرس باید حداکثر 650 کاراکتر باشد")]
        public string Address { get; set; }
        [Display(Name = "نام کاربری")]
        [StringLength(150, MinimumLength = 0, ErrorMessage = "نام باید حداکثر 150 کاراکتر باشد")]
        public string Username { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "تاریخ تولد")]
        public DateTime? BirthDate { get; set; }
        public int? UserTypeId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsLocked { get; set; }
        public DateTime? LastDateLogin { get; set; }
        public byte? UserStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        #endregion
        #region personel
        [Display(Name = "شماره پرسنلی")]
        public string PersonliNo { get; set; }
        [Display(Name = "سن")]

        public int? Age { get; set; }
        [Display(Name = "بیمه")]

        public bool? HasInsurance { get; set; }
        [Display(Name = "رسمی")]

        public bool? IsRasmi { get; set; }

        [Display(Name = "تاریخ حقوق")]
        public DateTime? SalaryDate { get; set; }

        //[StringLength(150)]
        [Display(Name = "مدرک تحصیلی")]
        public string MadrakTashili { get; set; }

        [Display(Name = "نقش کاربر")]
        //public int RoleId { get; set; }
        //public string RoleType { get; set; }
        public IEnumerable<SelectListItem> RolesList { get; set; }
        #endregion
        public string ImgThumbPath { get; set; }

        public string PicPath { get; set; }

    }

    public class ChangePersonelPasswordViewModel
    {

        public int UserId { get; set; }
        [Display(Name = "نام")]
        //[StringLength(50, MinimumLength = 0, ErrorMessage = "نام باید حداکثر 50 کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
      //  [StringLength(50, MinimumLength = 0, ErrorMessage = "نام باید حداکثر 50 کاراکتر باشد")]
        public string Family { get; set; }

        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [EmailAddress]
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
       

    }
}

