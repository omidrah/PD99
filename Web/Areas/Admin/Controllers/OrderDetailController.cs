using DataLayer.Context;
using DomainClass.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public partial class OrderDetailController : Controller
    {
        IUnitOfWork _uow;//db
        IProductService _products;
        IApplicationUserManager _user;
        IProductGroupService _productGroup;
        IImageService _Images;
        IPackageService _packages;
        IPackageProductService _packageProduct;
        ITagService _tags;
        IProductFeaturesService _productFeatures;
        IProductGroupFeaturesService _productGroupFeatures;
        IFeaturesService _features;
        IOrderService _orders;
        IOrderStatusService _orderStatus;
        IOrderDetailsService _orderDetail;
        IDoorehService _dooreh;

        public OrderDetailController(IUnitOfWork uow, IProductService product, IProductGroupService productGroup
            , IImageService images, IApplicationUserManager user, IPackageProductService packageProducts, IPackageService packages
            , ITagService tags, IProductFeaturesService productFeatures, IProductGroupFeaturesService productGroupFeatures
            , IFeaturesService features, IOrderService orders, IOrderStatusService orderStatus, IOrderDetailsService orderDetail, IDoorehService dooreh)
        {
            _uow = uow;
            _products = product;
            _user = user;
            _productGroup = productGroup;
            _Images = images;
            _packageProduct = packageProducts;
            _packages = packages;
            _tags = tags;
            _features = features;
            _productFeatures = productFeatures;
            _productGroupFeatures = productGroupFeatures;
            _orderDetail = orderDetail;
            _orders = orders;
            _orderStatus = orderStatus;
            _dooreh = dooreh;
        }
        // GET: Admin/OrderDetail
        public virtual ActionResult Index(int id)
        {
            ViewBag.OrderID = id;
            var order = _orders.Find(id);
            ViewBag.CustomerName = _user.FindById(order.UserID).Name + " " + _user.FindById(order.UserID).Family;
            ViewBag.OrderDate = order.OrderDate;
            ViewBag.Percent = 0;
            ViewBag.ActionName = "OpenOrders";
            if (order.IsFinalized && !order.IsTransfered)
            {
                ViewBag.ActionName = "OpenOrders";
            }
            if (!order.IsFinalized)
            {
                ViewBag.ActionName = "NotFinalizedOrders";
            }
            if (order.IsFinalized && order.IsTransfered)
            {
                ViewBag.ActionName = "FinalizedOrders";
            }
            
            var orderDetailList = _orderDetail.GetAll().Where(LL => LL.OrderID == id).ToList();
            List<OrderDetailViewModel> list = new List<OrderDetailViewModel>();
            foreach (var item in orderDetailList)
            {
                if (item.ItemType == "Product")
                {
                    list.Add(new OrderDetailViewModel()
                    {
                        OrderID = item.OrderID,
                        OrderDetailID = item.OrderDetailID,
                        Price = item.Price,
                        ItemType = item.ItemType,
                        IsTransfered = item.IsTransfered,
                        ItemId = item.ItemId,
                        ItemName = _productGroup.Find(_products.Find(item.ItemId).ProductGroupID).ProductGroupTitle + " " + _products.Find(item.ItemId).ProductName,
                        OrderedCount = item.OrderedCount
                    });
                }
                if (item.ItemType == "Dooreh")
                {
                    list.Add(new OrderDetailViewModel()
                    {
                        OrderID = item.OrderID,
                        OrderDetailID = item.OrderDetailID,
                        Price = item.Price,
                        ItemType = item.ItemType,
                        IsTransfered = item.IsTransfered,
                        ItemId = item.ItemId,
                        ItemName = "دوره " + _dooreh.Find(item.ItemId).Title,
                        OrderedCount = item.OrderedCount
                    });
                }

                if (item.ItemType == "Package")
                {
                    list.Add(new OrderDetailViewModel()
                    {
                        OrderID = item.OrderID,
                        OrderDetailID = item.OrderDetailID,
                        Price = item.Price,
                        ItemType = item.ItemType,
                        IsTransfered = item.IsTransfered,
                        ItemId = item.ItemId,
                        ItemName = "بسته " + _packages.Find(item.ItemId).PackageTitle,
                        OrderedCount = item.OrderedCount
                    });
                }
            }

            return View(list);
        }

        public virtual ActionResult Transfer(int orderDetailID)
        {
            return View();
        }
        public virtual ActionResult Print(int id)
        {
            var curDetail = _orderDetail.Find(id);
            var curOrd = _orders.Find(curDetail.OrderID);
            var curUser = _user.FindByIdAsync(curOrd.UserID);
            if (string.IsNullOrEmpty(curUser.Result.Address))
            {
                return View("enterUserAddress");
            }
            var mdl = new printVm
            {
                Address = curUser.Result.Address,
                name = curUser.Result.Name,
                family = curUser.Result.Family,
                userId = curUser.Result.Id,
                Tel = curUser.Result.Tel,
                Mobile = curUser.Result.Mobile ?? curUser.Result.PhoneNumber ?? "وارد نشده است"
                //Postalcode = curUser.Result.postalcode
            };
            return View(mdl);
        }
        public virtual ActionResult Print2(tmpVm[] cols)
        {
            int ordId = 0;   
            
            foreach (var item in cols)
            {
                var curDetail = _orderDetail.Find(item.id);
                curDetail.IsTransfered = true;
                ordId = curDetail.OrderID;
            }
            _uow.SaveAllChanges();
            var AllTransfered = _uow.Set<OrderDetail>().Any(x => x.OrderID == ordId && x.IsTransfered == false);
            var curOrd = _orders.Find(ordId);
            if(!AllTransfered)
            {
                curOrd.IsTransfered = true;
            }
             
            var curUser = _user.FindByIdAsync(curOrd.UserID);
            if (string.IsNullOrEmpty(curUser.Result.Address))
            {
                return View("enterUserAddress");
            }
            var mdl = new printVm
            {
                Address = curUser.Result.Address,
                name = curUser.Result.Name,
                family = curUser.Result.Family,
                userId = curUser.Result.Id,
                Tel = curUser.Result.Tel,
                Mobile = curUser.Result.Mobile ?? curUser.Result.PhoneNumber ?? "وارد نشده است"
                //Postalcode = curUser.Result.postalcode
            };
            _uow.SaveAllChanges();            
             return Json(mdl); 
           
        }
        public virtual ActionResult GetAllOrderDetialByProductIdNotTransfered(int productId=0)
        {
            
                var dd = _uow.Set<OrderDetail>().
                    Where(x => x.IsTransfered == false && x.ItemType == "Product" && x.ItemId == productId && x.Orders.IsFinalized == true)
                    .Select(item =>
                        new GetAllproductNotTransfer()
                        {
                            OrderID = item.OrderID,
                            OrderDetailID = item.OrderDetailID,
                            IsTransfered = item.IsTransfered,
                            ItemId = item.Orders.Users.Id,
                            ItemName = item.Orders.Users.Name + " " + item.Orders.Users.Family ?? "",
                            OrderedCount = item.OrderedCount
                        });
                return View(dd);
            
        }
        public virtual ActionResult SearchProduct()
        {
            var productList = _products.Where(ll => ll.IsActive == true).Select(p => new
            {
                ProductId = p.ProductId,
                ProductName = p.ProductGroup.ProductGroupTitle + " - " + p.ProductName
            });
            ViewBag.ProductId = new SelectList(productList, "ProductId", "ProductName");
            return View();
        }
    }
    public class prId
    {
        public int ProductId { get; set; }
    }
    public class printVm
    {
        public int userId { get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Postalcode { get; set; }
    }
    public class tmpVm
    {
        public int id { get; set; }
        public string type { get; set; }
    }
}