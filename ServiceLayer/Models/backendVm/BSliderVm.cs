using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Models
{
    public class BSliderVm
    {
        public int Id { get; set; }
        public string ImgPath { get; set; }
        [Display(Name = "عنوان ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]        
        public string imgTitle { get; set; }

        [Display(Name = "اولویت نمایش ")]        
        public string imgPriority { get; set; }
        [Display(Name = " فعال/غیرفعال")]        
        public bool imgIsActive { get; set; }
    }

  
}