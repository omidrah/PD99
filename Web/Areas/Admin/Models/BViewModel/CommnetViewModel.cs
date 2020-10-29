using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceLayer.Models
{
    public class CommentViewModel
    {
       
        public int ID { get; set; }
        public int NewsID { get; set; }
        public int ParentID { get; set; }
        public string Title { get; set; }
        [Display(Name = "نام")]
        public string Name { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "متن نظر")]
        public string Text { get; set; }
        public string IP { get; set; }
        public System.DateTime? Date { get; set; }
        [Display(Name = "تایید شده")]
        public bool Confirm { get; set; }
        public bool IsDeleted { get; set; }
        public bool Read { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }

       
    }
}