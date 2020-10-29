using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceLayer.Models
{
    public class NewsViewModel
    {
        [Key]
        public int NewsId { get; set; }
        [Display(Name = "گروه خبر")]
        public int ParentId { get; set; }

        [Display(Name = "عنوان خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string NewsTitle { get; set; }
        [Display(Name = "سرتیتر خبر")]
      //  [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string HeadTitle { get; set; }

        [Display(Name = "متن خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [AllowHtml]
        [UIHint("RichText")]
        public string NewsBody { get; set; }

        [Display(Name = "تایید شده")]
        public bool? IsAuthenticated { get; set; }

        [Display(Name = "عکس اصلی")]
        public string MasterPicPathThumb { get; set; }
        [Display(Name = "تعداد بازدید")]
        public long Visit { get; set; }
        [Display(Name = "پسندیدن")]
        public int Like { get; set; }
        [Display(Name = "نپسندیدن")]
        public int Dislike { get; set; }
        [Display(Name = "تعداد کامنت ها")]
        public int CommentsCounts { get; set; }
        public Nullable<DateTime> PublishDate { get; set; }
    }
}