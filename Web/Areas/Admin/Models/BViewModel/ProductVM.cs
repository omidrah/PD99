using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace ServiceLayer.Models
{
    [DisplayName("محصول")]
    public class ProductVM
    {
        public int ProductId { get; set; }
        [Display(Name = "کد محصول")]
        public string ProductCode { get; set; }

        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ProductName { get; set; }
        [Display(Name = "نام لاتین")]
        public string ProductEnglishName { get; set; }

        [Display(Name = "توضیحات")]
        [AllowHtml]
        [UIHint("RichText")]
        public string Summery { get; set; }

        [Display(Name = "شرح مختصر")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "گروه محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ProductGroupID { get; set; }

        [Display(Name = "گروه محصول")]
        public string ProductGroupTitle { get; set; }
        [Display(Name = "نقطه سفارش")]
        public int? NoghteSefaresh { get; set; }
        [Display(Name = "موجودی")]
        public int? ProductMojodi { get; set; }
        public int? ProductDorehId { get; set; }
        [Display(Name = "نمایش/عدم نمایش")]
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        [Display(Name = "تصویر محصول")]
       // [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PicPath { get; set; }
        [Display(Name = "قیمت")]
        public int? Price { get; set; }
        [Display(Name = "درصد تخفیف")]
        [Range(0, 100,ErrorMessage ="عدد وارد شده باید بین 0 تا 100 باشد")]
       
        public byte? OffPercent { get; set; }
        [Display(Name = "وزن به گرم")]
        public int? Weight { get; set; }
        public List<int> SelectedPackages { get; set; }

        public string tags { get; set; }
        public int? Createdby { get; set; }
        public int? Modifiedby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}