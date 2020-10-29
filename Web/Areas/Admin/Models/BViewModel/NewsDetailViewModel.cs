using DomainClass.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceLayer.Models
{
    public class NewsDetailViewModel
    {
        public NewsViewModel NewsViewModel { get; set; }
        public IEnumerable<Comments> CommentViewModel { get; set; }
    }
}