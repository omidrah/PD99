using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceLayer.Models
{
    public class NewsGroupVIewModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int NewsId { get; set; }

        [Display(Name = "مدیرگروه")]
        public int GroupmanagerId { get; set; }

        [Display(Name = "عنوان گروه خبری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string NewsTitle { get; set; }

        [Display(Name = "عکس اصلی")]
        public string MasterPicPath { get; set; }
    }
}