using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ShopCartItem
    {
        public int ItemId { get; set; }

        public string ItemType { get; set; }
        public int Count { get; set; }
    }


    public class ShopCartItemViewModel
    {
        public int ItemId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string ItemType { get; set; }
        public int Count { get; set; }
    }

    public class ShowOrderViewModel
    {
        public int ItemId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string ItemType { get; set; }
        public int Count { get; set; }
        public int Sum { get; set; }
        public int Price { get; set; }
        public int OffPrice { get; set; }
        public byte? OffPercent { get; set; }
        public int Weight { get; set; }
        public int TransferCost { get; set; }
    }

    public class OrderShipingViewModel
    {
        public string DisplayName { get; set; }
        public string Address { get; set; }
        public int SumTotal { get; set; }
        public int TransferCost { get; set; }

    }
}