using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models 
{
   public class ProductFeaturesVM
    {
        [ScaffoldColumn(false)]
        public int ProductFeatureValueID { get; set; }
        [Display(Name = "ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int FeatureId { get; set; }
        [Display(Name = "محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ProductId { get; set; }
        [Display(Name = "مقدار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FeatureValue { get; set; }
        [Display(Name = "فعال")]
       
        public bool? IsActive { get; set; }
          [ScaffoldColumn(false)]
        public bool? IsDeleted { get; set; }
        [ScaffoldColumn(false)]
        public int? CreatedBy { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? CreatedDate { get; set; }
        [ScaffoldColumn(false)]
        public int? ModifiedBy { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? ModifiedDate { get; set; }


    }
}
