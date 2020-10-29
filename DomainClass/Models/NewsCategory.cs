using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass.Models
{
    class NewsCategory
    {
        public int ID { get; set; }
        public int CatID { get; set; }
        public int NewsID { get; set; }
        public bool? IsDeleted { get; set; }

        //public virtual Tbl_Catgoris Tbl_Catgoris { get; set; }
        //public virtual Tbl_News Tbl_News { get; set; }
    }
}
