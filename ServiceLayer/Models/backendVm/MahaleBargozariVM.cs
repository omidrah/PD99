using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ServiceLayer.Models
{
    [DisplayName("محل یرگزاری کلاس")]

    public class MahaleBargozariVM
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        

        [Display(Name = "محل برگزاری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(250, MinimumLength = 0, ErrorMessage = "محل برکزاری باید حداکثر 250 کاراکتر باشد")]
        public string MahalTitle { get; set; }
        [Display(Name = "فعال")]
        public bool? IsActive { get; set; }
        [ScaffoldColumn(false)]
        public bool? IsDeleted { get; set; }
        [ScaffoldColumn(false)]
        public int? CreateBy { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; }
        [ScaffoldColumn(false)]
        public int? ModifiedBy { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? ModifiedDate { get; set; }
    }
}