using DomainClass.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Models.FrontEnd
{
    public class OrdersViewModel
    {
        [Key]
        [Display(Name = "شماره فاکتور")]
        public int OrderID { get; set; }
        public int UserID { get; set; }
        [Display(Name="تاریخ فاکتور")]
        public System.DateTime OrderDate { get; set; }
        [Display(Name = "پرداخت شده")]
        public bool IsFinalized { get; set; }

        [Display(Name = "مبلغ کل فاکتور")]
        public int OrderTotal { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}