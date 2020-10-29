using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DomainClass.Models
{
    public class Gallery
    {
        public int Id { get; set; }
        public string imgTitle { get; set; }
        public string imgName { get; set; }
        public string imgDesc { get; set; }
        public int? imgPriority { get; set; }
        public bool? imgIsActive { get; set; }
        public int? imgWidth { get; set; }
        public int? imgHeight { get; set; }
        public string imgExtension { get; set; }
        public string NavigateUrl { get; set; }
        public string imgPath { get; set; }
        public string imgPathThumb { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public int? Modifyby { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
    }
}