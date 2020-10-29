using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceLayer.Models
{
    public class ProductImageViewModel
    {
        [Key]
        public int ImageID { get; set; }
        public int? ItemId { get; set; }
        public string ImagePath { get; set; }
        public string ImageThumbPath { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}