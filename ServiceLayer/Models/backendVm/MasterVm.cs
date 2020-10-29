using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace ServiceLayer.Models
{
    [DisplayName("استاد")]

    public class MasterVm
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
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }
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
        #region Master
        [Display(Name = "رتبه")]
        [StringLength(150, MinimumLength = 0, ErrorMessage = "رتبه باید حداکثر 150 کاراکتر باشد")]
        public string Rotbe { get; set; }
        [Display(Name = "هیئت علمی")]
        public bool? IsHeiatElmi { get; set; }

        [Display(Name = "تخصص")]
        public string Takhasos { get; set; } //اندوسکوپی، قلب ، ...
        [Display(Name = "بیوگرافی")]
        [UIHint("RichText")]
     //  [AllowHtml]
        public string Bio { get; set; } //زندگینامه
        #endregion
        public string Desc { get; set; }
        public string ImgThumbPath { get; set; }

        public string PicPath { get; set; }

    }
}

