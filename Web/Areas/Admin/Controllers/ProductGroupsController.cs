using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer.Context;
using DomainClass.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace Web.Areas.Admin.Controllers
{
    public partial class ProductGroupsController : Controller
    {

        IUnitOfWork _uow;//db
        IProductGroupService _productGroup;
        IApplicationUserManager _user;
        IProductGroupFeaturesService _productGroupFeatures;
        IFeaturesService _features;
        IProductService _products;
        public ProductGroupsController(IUnitOfWork uow, IProductGroupService productGroup, IApplicationUserManager user
            , IProductGroupFeaturesService productGroupFeatures, IFeaturesService featuresService, IProductService products)
        {
            _uow = uow;
            _productGroup = productGroup;
            _user = user;
            _productGroupFeatures = productGroupFeatures;
            _features = featuresService;
            _products = products;
        }

        // GET: doruses
        public virtual ActionResult Index()
        {
            return View(_productGroup.GetAll().Select(item => new ProductGroupVM
            {
                ProductGroupTitle = item.ProductGroupTitle,
                ProductGroupId = item.ProductGroupId,
                IsActive = item.IsActive,
                CreatedBy = item.CreatedBy,
                CreatedDate = item.CreatedDate,
                ModifiedBy = item.ModifiedBy,
                ModifiedDate = item.ModifiedDate,
                PicPath = item.PicPath,
                PicPathThumbnail = item.PicPathThumbnail
            }));
        }

        public virtual ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public virtual ActionResult Create(ProductGroupVM productGroupVM)
        {
            if (ModelState.IsValid && productGroupVM != null)
            {
                var newproductGroup = new ProductGroup()
                {
                    ProductGroupTitle = productGroupVM.ProductGroupTitle,
                    IsActive = productGroupVM.IsActive == true ? true : false,
                    CreatedDate = DateTime.Now,
                    CreatedBy = _user.GetCurrentUserId()
                };
                _productGroup.Add(newproductGroup);
                _uow.SaveAllChanges();
                productGroupVM.ProductGroupId = newproductGroup.ProductGroupId;
            }
            return RedirectToAction("Index");
        }

        public virtual ActionResult Edit(int id)
        {
            var productGroup = _productGroup.Find(id);
            return View(new ProductGroupVM()
            {
                ProductGroupId = productGroup.ProductGroupId,
                ProductGroupTitle = productGroup.ProductGroupTitle,
                IsActive = productGroup.IsActive
            });
        }
        [HttpPost]
        public virtual ActionResult Edit(ProductGroupVM productGroupVM)
        {
            var productGroup = _productGroup.Find(productGroupVM.ProductGroupId);
            if (ModelState.IsValid && productGroupVM != null)
            {
                productGroup.IsActive = productGroupVM.IsActive;
                productGroup.ProductGroupTitle = productGroupVM.ProductGroupTitle;
                productGroup.ModifiedDate = DateTime.Now;
                productGroup.ModifiedBy = _user.GetCurrentUserId();
                _uow.SaveAllChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public virtual ActionResult Delete(int id)
        {
            try
            {
               var item = _productGroup.Find(id);
                _productGroup.Remove(item);
                _uow.SaveAllChanges();
                return Json(new { ret = "true", codeid = 1 });
            }
            catch
            {
                return Json(new { ret = "false", codeid = 2 });
            }
        }



        public virtual ActionResult Addfeatures(int id)
        {
            ViewBag.productGroupFeaturesList = _productGroupFeatures.GetAllByProductGroupId(id).Select(x => x.FeaturesId).ToList();
            ViewBag.FeaturesList = _features.GetAll();
            ViewBag.ProductGroupTitle = _productGroup.Find(id).ProductGroupTitle;
            return View(new ProductGroupFeaturesVM { ProductGroupId = id });
        }

        [HttpPost]
        public virtual ActionResult Addfeatures(ProductGroupFeaturesVM productGroupFeaturesVM, List<int> SelectedFeatures)
        {
            foreach (var item in _productGroupFeatures.GetAllByProductGroupId(productGroupFeaturesVM.ProductGroupId))
            {
                _productGroupFeatures.Remove(item);
            }

            if (SelectedFeatures != null)
            {
                foreach (int feature in SelectedFeatures)
                {
                    _productGroupFeatures.Add(new ProductGroupFeatures()
                    {
                        ProductGroupId = productGroupFeaturesVM.ProductGroupId,
                        IsActive = true,
                        FeaturesId = feature,
                        CreatedDate = DateTime.Now,
                        CreatedBy = _user.GetCurrentUserId()
                    });
                }

                ViewBag.productGroupFeaturesList = _productGroupFeatures.GetAllByProductGroupId(productGroupFeaturesVM.ProductGroupId).Select(x => x.FeaturesId).ToList();
                ViewBag.FeaturesList = _features.GetAll();
                TempData["res"] = "<script>showNotification('رکورد جدید بروز شد', 'success','center')</script>";

            }
            _uow.SaveAllChanges();
            TempData["res"] = "<script>showNotification('رکورد جدید بروز شد', 'success','center')</script>";
            return RedirectToAction("Index");
        }



        public virtual ActionResult AddOff(int id)
        {
            ViewBag.ProductGroupId = new SelectList(_productGroup.GetAll(), "ProductGroupId", "ProductGroupTitle", id);
            return View();
        }

        [HttpPost]
        public virtual ActionResult AddOff(ProductGroupVM productGroupVM)
        {
            productGroupVM.ProductGroupTitle = _productGroup.Find(productGroupVM.ProductGroupId).ProductGroupTitle;
            int productCount = 0;
            List<Product> productList = _products.GetAll().Where(p => p.ProductGroupID == productGroupVM.ProductGroupId).ToList();
            foreach (var item in productList)
            {
                Product product = _products.Find(item.ProductId);
                product.OffPercent = productGroupVM.OffPercent;
                productCount++;
            }

            _uow.SaveAllChanges();
            ViewBag.ProductGroupId = new SelectList(_productGroup.GetAll(), "ProductGroupId", "ProductGroupTitle", productGroupVM.ProductGroupId);
            productGroupVM.OffPercent = 0;
            ViewBag.ProductCount = productCount;
            return View(productGroupVM);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uow.Dispose();
            }
            base.Dispose(disposing);

        }


    }
}
