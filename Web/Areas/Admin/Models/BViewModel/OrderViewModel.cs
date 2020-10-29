using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceLayer.Models
{
    public class OrderViewModel
    {
        [Key]
        [DisplayName("شماره فاکتور")]
        public int OrderID { get; set; }

        [DisplayName("سفارش دهنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int UserID { get; set; }

        [DisplayName("سفارش دهنده")]
        public string CustomersName { get; set; }

        [DisplayName("صادر کننده")]
        public int CreatedBy { get; set; }

        [DisplayName("صادر کننده")]
        public string ExporterName { get; set; }

        [DisplayName("تاریخ سفارش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime OrderDate { get; set; }

        [DisplayName("زمان سفارش")]
        [DataType(DataType.Time)]
        public DateTime OrderTime { get; set; }

        [DisplayName("شرح فاکتور")]
        public string OrderDescription { get; set; }

        [DisplayName("وضعیت فاکتور")]
        public int StatusID { get; set; }
        [DisplayName("وضعیت فاکتور")]
        public string StatusDesc { get; set; }

        [DisplayName("مبلغ فاکتور")]
        public int OrderSum { get; set; }
        [DisplayName("درصد تخفیف")]
        public int OrderOffPersent { get; set; }
        [DisplayName("کسر از موجودی انبار(تایید شده)")]
        public bool Authenticated { get; set; }
    }
}