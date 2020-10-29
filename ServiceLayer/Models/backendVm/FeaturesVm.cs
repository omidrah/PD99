using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class FeaturesVm
    {
        [ScaffoldColumn(false)]
        public int FeatureId { get; set; }

        
        [StringLength(250)]
        [Display(Name = "نام ویژگی به لاتین")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FeaturesName { get; set; }

        //[Required]
         [Display(Name = "عنوان نمایشی")]
        [StringLength(250)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string featuresDispaly { get; set; }
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
