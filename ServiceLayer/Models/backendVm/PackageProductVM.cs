using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Models
{
    [DisplayName("بسته")]
  //  [DisplayPluralName("بسته")]
    public class PackageProductVM
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        [Display(Name = "نام بسته")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PackageTitle { get; set; }
        [Display(Name = "وضعیت")]
        public byte? Status { get; set; }
        [Display(Name = "نوع بسته")]
       // [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool PackageType { get; set; }
        [Display(Name = "نام منبع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ProductId { get; set; }
        [Display(Name = "نام منبع")]
     //   [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ProductName { get; set; }

        [Display(Name = "گروه منبع")]
       // [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ProductGroupID { get; set; }

        [Display(Name = "گروه منبع")]
        public string ProductGroupTitle { get; set; }


    }
}