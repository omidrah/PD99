using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceLayer.Models
{
    public class OrderDetailViewModel
    {
        public int OrderDetailID { get; set; }
        public int? OrderDetailParenID { get; set; }

        public bool ShowForCusomer { get; set; }
        [DisplayName("شماره سفارش")]
        [Required]
        public int? OrderID { get; set; }

        [DisplayName("نام کالا")]
        public int? ItemId { get; set; }

        [DisplayName("نام کالا")]
        public string ItemName { get; set; }

        [DisplayName("تعداد")]
        [Required]
        public int? OrderedCount { get; set; }
        [DisplayName("وضعیت محصول")]
        public bool IsTransfered { get; set; }
        [DisplayName("درصد تخفیف ")]
        public int? ProductOffPersent { get; set; }


        [DisplayName("قیمت")]
        public int Price { get; set; }

        public string ItemType { get; set; }

    }
    public class GetAllproductNotTransfer
    {
        public int OrderDetailID { get; set; }
        [DisplayName("شماره سفارش")]
        [Required]
        public int? OrderID { get; set; }

        [DisplayName("نام مشتری")]
        public int? ItemId { get; set; }

        [DisplayName("نام مشتری")]
        public string ItemName { get; set; }

        [DisplayName("تعداد")]
        [Required]
        public int? OrderedCount { get; set; }
        [DisplayName("وضعیت محصول")]
        public bool IsTransfered { get; set; }

    }
}