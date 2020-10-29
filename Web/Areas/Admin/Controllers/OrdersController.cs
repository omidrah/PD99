using DataLayer.Context;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public partial class OrdersController : Controller
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

        public OrdersController(IUnitOfWork uow, IProductService product, IProductGroupService productGroup
            , IImageService images, IApplicationUserManager user, IPackageProductService packageProducts, IPackageService packages
            , ITagService tags, IProductFeaturesService productFeatures, IProductGroupFeaturesService productGroupFeatures
            , IFeaturesService features, IOrderService orders, IOrderStatusService orderStatus, IOrderDetailsService orderDetail)
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
        }
        // GET: Admin/Orders
        public virtual ActionResult OpenOrders()
        {
            // var alluser = _db.Users.Where(ll => ll.UserTypeID != 1 && ll.IsDeleted == false).ToList(); //only Users
            var result = new List<OrderViewModel>();

            foreach (var item in _orders.GetAll().Where(ll => ll.IsFinalized == true && ll.IsTransfered != true && ll.IsDeleted != true))
            {
                result.Add(
                     new OrderViewModel
                     {
                         OrderID = item.OrderID,
                         CustomersName = _user.FindById(item.UserID).Name + " " + _user.FindById(item.UserID).Family,
                         OrderDate = (DateTime)item.OrderDate,
                         OrderSum = item.Sum,

                         StatusDesc = "منتظر تهیه موجودی"
                         // StatusDesc = _db.Condition.Find(item.OrderStatusID).ConditionDescription,
                         // ExporterName = alluser.FirstOrDefault(aa => aa.UserId == item.CreatedBy).Persons.name + " " + alluser.FirstOrDefault(aa => aa.UserId == item.CreatedBy).Persons.family,
                         //  Authenticated = item.IsAuthenticated ?? false
                     }
                    );
            }
            return View(result.OrderByDescending(ll => ll.OrderDate));
        }
        public virtual ActionResult Index()
        {
            return View();
        }
        public virtual ActionResult NotFinalizedOrders()
        {
            // var alluser = _db.Users.Where(ll => ll.UserTypeID != 1 && ll.IsDeleted == false).ToList(); //only Users
            var result = new List<OrderViewModel>();

            foreach (var item in _orders.GetAll().Where(ll => ll.IsFinalized == false && ll.IsDeleted != true))
            {
                result.Add(
                     new OrderViewModel
                     {
                         OrderID = item.OrderID,
                         CustomersName = _user.FindById(item.UserID).Name + " " + _user.FindById(item.UserID).Family,
                         OrderDate = (DateTime)item.OrderDate,
                         OrderSum = item.Sum,
                         // StatusDesc = "منتظر تهیه موجودی"
                         // StatusDesc = _db.Condition.Find(item.OrderStatusID).ConditionDescription,
                         // ExporterName = alluser.FirstOrDefault(aa => aa.UserId == item.CreatedBy).Persons.name + " " + alluser.FirstOrDefault(aa => aa.UserId == item.CreatedBy).Persons.family,
                         //  Authenticated = item.IsAuthenticated ?? false
                     }
                    );
            }
            return View(result.OrderByDescending(ll => ll.OrderDate));
        }

        public virtual ActionResult FinalizedOrders()
        {
            // var alluser = _db.Users.Where(ll => ll.UserTypeID != 1 && ll.IsDeleted == false).ToList(); //only Users
            var result = new List<OrderViewModel>();

            foreach (var item in _orders.GetAll().Where(ll => ll.IsTransfered == true && ll.IsDeleted != true))
            {
                result.Add(
                     new OrderViewModel
                     {
                         OrderID = item.OrderID,
                         CustomersName = _user.FindById(item.UserID).Name + " " + _user.FindById(item.UserID).Family,
                         OrderDate = (DateTime)item.OrderDate,
                         OrderSum = item.Sum,
                         // StatusDesc = "منتظر تهیه موجودی"
                         // StatusDesc = _db.Condition.Find(item.OrderStatusID).ConditionDescription,
                         // ExporterName = alluser.FirstOrDefault(aa => aa.UserId == item.CreatedBy).Persons.name + " " + alluser.FirstOrDefault(aa => aa.UserId == item.CreatedBy).Persons.family,
                         //  Authenticated = item.IsAuthenticated ?? false
                     }
                    );
            }
            return View(result.OrderByDescending(ll => ll.OrderDate));
        }
        public virtual ActionResult OrderAdrdressPrint(int orderID)
        {
            var curOrd = _orders.Find(orderID);
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

        public virtual ActionResult SetOrderFinlized(int id)
        {
            try
            {
                _orders.Find(id).IsFinalized = true;
                // _products.Delete(id);
                _uow.SaveAllChanges();
                return Json(new { ret = "true", codeid = 1 });
            }
            catch
            {
                return Json(new { ret = "false", codeid = 2 });
            }
        }


        public virtual ActionResult Delete(int id)
        {
            try
            {
                var curOrder = _orders.Find(id).IsDeleted = true;
                //var orderDetails = _orderDetail.GetAll().Where(ll => ll.OrderID == id);
                //foreach (var item in orderDetails)
                //{
                //    _orderDetail.Remove(item);
                //}
                //_orders.Remove(curOrder);
                // _products.Delete(id);
                _uow.SaveAllChanges();
                return Json(new { ret = "true", codeid = 1 });
            }
            catch
            {
                return Json(new { ret = "false", codeid = 2 });
            }
        }

    }
}