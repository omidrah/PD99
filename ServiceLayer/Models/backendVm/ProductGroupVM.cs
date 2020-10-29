using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ServiceLayer.Models
{
    [DisplayName("گروه محصولات")]
    public class ProductGroupVM
    {
        [ScaffoldColumn(false)]
        [Display(Name = "گروه محصول")]
        public int ProductGroupId { get; set; }

        [Display(Name = "گروه محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "محل برکزاری باید حداکثر 250 کاراکتر باشد")]
        public string ProductGroupTitle { get; set; }
        [Display(Name = "درصد تخفیف")]
        [Range(0, 100, ErrorMessage = "عدد وارد شده باید بین 0 تا 100 باشد")]

        public byte? OffPercent { get; set; }
        [Display(Name = "فعال")]
        public bool? IsActive { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "گروه محصول")]
      //  [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(350, MinimumLength = 0, ErrorMessage = "محل برکزاری باید حداکثر 250 کاراکتر باشد")]
        public string PicPath { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "گروه محصول")]
       // [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(350, MinimumLength = 0, ErrorMessage = "محل برکزاری باید حداکثر 250 کاراکتر باشد")]
        public string PicPathThumbnail { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? CreatedDate { get; set; }
        [ScaffoldColumn(false)]
        public int? CreatedBy { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? ModifiedDate { get; set; }
        [ScaffoldColumn(false)]
        public int? ModifiedBy { get; set; }

    }
}