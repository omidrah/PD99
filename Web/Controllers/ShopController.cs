using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers
{
    [AllowAnonymous]
    public class ShopController : ApiController
    {
        // GET: api/Shop
        public int Get()
        {
            List<ShopCartItem> list = new List<ShopCartItem>();
            var sessions = HttpContext.Current.Session;
            if (sessions["ShopCart"] != null)
            {
                list = sessions["ShopCart"] as List<ShopCartItem>;
            }

            return list.Sum(l => l.Count);
        }

        // GET: api/Shop/5
        public int Get(int id, string itemType)
        {
            List<ShopCartItem> list = new List<ShopCartItem>();
            var sessions = HttpContext.Current.Session;
            if (sessions["ShopCart"] != null)
            {
                list = sessions["ShopCart"] as List<ShopCartItem>;
            }
            if (list.Any(p => p.ItemId == id && p.ItemType == itemType))
            {
                int index = list.FindIndex(p => p.ItemId == id && p.ItemType == itemType);
                list[index].Count += 1;
            }
            else
            {
                list.Add(new ShopCartItem()
                {
                    ItemId = id,
                    ItemType = itemType,
                    Count = 1
                });
            }

            sessions["ShopCart"] = list;

            return Get();
        }


    }
}
