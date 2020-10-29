using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace ServiceLayer.Models
{
    public class ProductGroupFeaturesVM
    {
        [ScaffoldColumn(false)]
        public int ProductGroupFeatuesId { get; set; }
        [Display(Name = "نام گروه")]
        public int ProductGroupId { get; set; }
        [Display(Name = "نام ویژگی")]


        public int FeaturesId { get; set; }

       // public IEnumerable<SelectListItem> RolesList { get; set; }
        [Display(Name = "فعال")]
        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }
    }
}
