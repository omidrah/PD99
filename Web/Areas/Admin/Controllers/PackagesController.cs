
using DataLayer.Context;
using System;
using System.Linq;
using System.Net;
using DomainClass.Models;
using System.Web.Mvc;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Data.Entity.Validation;
using System.Diagnostics;
using KooyWebApp_MVC.Classes;
using InsertShowImage;

namespace Web.Areas.Admin.Controllers
{
    // [Authorize]
    public partial class PackagesController : Controller
    {
        IUnitOfWork _uow;//db
        IPackageService _packages;
        IPackageProductService _packageProduct;
        IProductService _products;
        IProductGroupService _productGroups;
        IApplicationUserManager _user;
        IImageService _Images;
        ITagService _tags;


        public PackagesController(IUnitOfWork uow, IPackageService packages, IPackageProductService packageProduct
            , IProductGroupService productGroup, IProductService products, IApplicationUserManager user, IImageService images
            , ITagService tags)
        {
            _uow = uow;
            _packages = packages;
            _packageProduct = packageProduct;
            _user = user;
            _products = products;
            _productGroups = productGroup;
            _Images = images;
            _tags = tags;
        }

        // GET: doruses
        #region Packages
        public virtual ActionResult Index()
        {
            return View(_packages.GetAll().Select(item => new PackageVM
            {

                PackageTitle = item.PackageTitle,
                Id = item.Id,
                PackageDescription = item.PackageDescription,
                Summery = item.Summery,
                Cost = item.Cost,
                KeyWord = item.KeyWord,
                OffPercent = item.OffPercent,
                ParentId = item.ParentId,
                IsActive = item.IsActive
            }).OrderBy(ll => ll.PackageTitle));
        }
        public virtual ActionResult ListPackages()
        {
            var packages_Groups = _packages.GetAll().Select(item => new PackageVM
            {

                PackageTitle = item.PackageTitle,
                Id = item.Id,
                PackageDescription = item.PackageDescription,
                Summery = item.Summery,
                Cost = item.Cost,
                KeyWord = item.KeyWord,
                ParentId = item.ParentId,
                IsActive = item.IsActive
            });
            return PartialView(packages_Groups);
        }

        public virtual ActionResult Hierarchy()
        {
            return View(_packages.GetAll().Select(item => new PackageVM
            {

                PackageTitle = item.PackageTitle,
                Id = item.Id,
                PackageDescription = item.PackageDescription,
                Summery = item.Summery,
                Cost = item.Cost,
                KeyWord = item.KeyWord,
                IsActive = item.IsActive
            }));
        }

        public virtual ActionResult PackageTree()
        {
            PackageTreeVM Model = new PackageTreeVM();
            Model.PackagesVM = _packages.GetAll().Select(item => new PackageVM
            {

                PackageTitle = item.PackageTitle,
                Id = item.Id,
                PackageDescription = item.PackageDescription,
                Summery = item.Summery,
                Cost = item.Cost,
                KeyWord = item.KeyWord,
                ParentId = item.ParentId,
                IsActive = item.IsActive
            });
            return View(Model);
        }

        public virtual ActionResult HierarchyBinding_Packages([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_packages.GetAll().ToDataSourceResult(request));
        }

        public virtual ActionResult HierarchyBinding_ChildPackages(int parentId, [DataSourceRequest] DataSourceRequest request)
        {
            return Json(_packages.GetAll()
                .Where(order => order.ParentId == parentId)
                .ToDataSourceResult(request));
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(_packages.GetAll(), "Id", "PackageTitle");
            return View();
        }

        [HttpPost]
        public virtual ActionResult Create(PackageVM packageVM, HttpPostedFileBase ImgPackage)
        {
            //if (PicPath == null)
            //{
            //    ModelState.AddModelError("PackageTitle", "حدقل یک عکس برای بسته انتخاب کنید");
            //}
            if (ModelState.IsValid && packageVM != null)
            {
                var newItem = new Packages()
                {
                    PackageTitle = packageVM.PackageTitle,
                    PackageType = true,
                    Status = 1,
                    KeyWord = packageVM.KeyWord,
                    IsDeleted = false,
                    IsActive = true,
                    Summery = packageVM.Summery,
                    PackageDescription = packageVM.PackageDescription,
                    ParentId = packageVM.ParentId,
                    Cost = packageVM.Cost,
                    Weight = packageVM.Weight,
                    OffPercent = packageVM.OffPercent ?? 0,
                    CreateDate = DateTime.Now,
                    CreatedBy = _user.GetCurrentUserId()
                };
                #region Save Uploaded Image 

                newItem.ImageName = "images.jpg";
                if (ImgPackage != null && ImgPackage.IsImage())
                {
                    newItem.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImgPackage.FileName);
                    ImgPackage.SaveAs(Server.MapPath("/Uploads/ImgProducts/" + newItem.ImageName));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Uploads/ImgProducts/" + newItem.ImageName),
                        Server.MapPath("/Uploads/ImgProducts/Thumb/" + newItem.ImageName));
                }
                #endregion
                _packages.Add(newItem);
                _uow.SaveAllChanges();
                if (!string.IsNullOrEmpty(packageVM.tags))
                {
                    string[] tag = packageVM.tags.Split('-');
                    foreach (string t in tag)
                    {
                        _tags.Add(new Tags()
                        {
                            itemId = newItem.Id,
                            tagTitle = t.Trim(),
                            tagConstant = "Package"
                        });
                    }
                }

                _uow.SaveAllChanges();
                TempData["res"] = "<script>showNotification('رکورد جدید افزوده شد', 'success','center')</script>";
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(_packages.GetAll(), "Id", "PackageTitle");
            return View(packageVM);
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            var curRec = _packages.Find(id);
            ViewBag.ParentId = new SelectList(_packages.GetAll(), "Id", "PackageTitle", curRec.ParentId);
            ViewBag.Tags = string.Join("-", _tags.GetAllByItemId(id, "Package").Select(t => t.tagTitle).ToList());
            return View(new PackageVM
            {
                Id = curRec.Id,
                PackageTitle = curRec.PackageTitle,
                KeyWord = curRec.KeyWord,
                Summery = curRec.Summery,
                PackageDescription = curRec.PackageDescription,
                Cost = curRec.Cost,
                OffPercent = curRec.OffPercent,
                ImageName = curRec.ImageName,
                Weight = curRec.Weight,
                IsActive = curRec.IsActive
            }
                );
        }
        [HttpPost]
        public virtual ActionResult Edit(PackageVM packageVM, HttpPostedFileBase ImgPackage)
        {
            if (ModelState.IsValid && packageVM != null)
            {
                var curPackage = _packages.Find(packageVM.Id);
                curPackage.PackageTitle = packageVM.PackageTitle;
                curPackage.IsActive = packageVM.IsActive;
                curPackage.PackageDescription = packageVM.PackageDescription;
                curPackage.KeyWord = packageVM.KeyWord;
                curPackage.Summery = packageVM.Summery;
                curPackage.ParentId = packageVM.ParentId;
                curPackage.Cost = packageVM.Cost;
                curPackage.OffPercent = packageVM.OffPercent ?? 0;
                curPackage.ModifyBy = _user.GetCurrentUserId();
                curPackage.ModifyDate = DateTime.Now;
                curPackage.Weight = packageVM.Weight;
                #region Save Uploaded Image 

                if (ImgPackage != null && ImgPackage.IsImage())
                {
                    if (curPackage.ImageName != "images.jpg")
                    {
                        System.IO.File.Delete(Server.MapPath("/Uploads/ImgProducts/Thumb/" + curPackage.ImageName));
                        System.IO.File.Delete(Server.MapPath("/Uploads/ImgProducts/" + curPackage.ImageName));
                    }

                    curPackage.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImgPackage.FileName);
                    ImgPackage.SaveAs(Server.MapPath("/Uploads/ImgProducts/" + curPackage.ImageName));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Uploads/ImgProducts/" + curPackage.ImageName),
                        Server.MapPath("/Uploads/ImgProducts/Thumb/" + curPackage.ImageName));
                }
                #endregion

                var tags = _tags.GetAllByItemId(packageVM.Id, "Package");
                foreach (var tag in tags)
                {
                    _tags.Remove(tag);
                }
                if (!string.IsNullOrEmpty(packageVM.tags))
                {
                    string[] tag = packageVM.tags.Split('-');
                    foreach (string t in tag)
                    {
                        _tags.Add(new Tags()
                        {
                            itemId = packageVM.Id,
                            tagTitle = t.Trim(),
                            tagConstant = "Package"
                        });
                    }
                }
                _uow.SaveAllChanges();
                TempData["res"] = "<script>showNotification('رکورد جدید بروز شد', 'success','center')</script>";
                return RedirectToAction("Index");
            }
            ViewBag.Tags = string.Join("-", _tags.GetAllByItemId(packageVM.Id, "Package").Select(t => t.tagTitle).ToList());
            TempData["res"] = "<script>showNotification('مقادیر را درست وارد کنید', 'success','center')</script>";
            return View(packageVM);
        }

        [HttpPost]
        public virtual ActionResult Delete(int id)
        {
            //if (db.PackageProducts.Any(ll => ll.ProductId == id))

            //{
            //    return Json(new { ret = "false", codeid = 3 });
            //}
            //db.Products.Remove(products);            
            try
            {
                _packages.Remove(_packages.Find(id));
                // var curDorus = _packages.Find(packageVM.Id);
                // curDorus.IsDeleted = true;
                _uow.SaveAllChanges();
                return Json(new { ret = "true", codeid = 1 });
            }
            catch
            {
                return Json(new { ret = "false", codeid = 2 });
            }
        }
        #endregion

        #region Package Detailes

        public virtual ActionResult ShowProp(int id)
        {
            var curPackage = _packages.Find(id);
            ViewBag.PackageId = id;
            ViewBag.PackageTitle = curPackage.PackageTitle;
            ViewBag.PackageStatus = curPackage.Status;
            ViewBag.PackageType = curPackage.PackageType;
            return View(_packageProduct.GetAll(id).Select(item => new PackageProductVM
            {

                ProductName = _products.Find(item.ProductId).ProductName,
                PackageTitle = _packages.Find(item.PackageId).PackageTitle,
                Id = item.Id,
                ProductGroupTitle = _productGroups.Find(_products.Find(item.ProductId).ProductGroupID).ProductGroupTitle,
                Status = _packages.Find(item.PackageId).Status

            }));
        }

        public virtual ActionResult AddDetiales(int packageId)
        {
            var productList = _products.Where(ll => ll.IsActive == true).Select(p => new
            {
                ProductId = p.ProductId,
                ProductName = p.ProductGroup.ProductGroupTitle + " - " + p.ProductName
            });
            ViewBag.ProductId = new SelectList(productList, "ProductId", "ProductName");
            ViewBag.PackageId = packageId;
            ViewBag.PackageTitle = _packages.Find(packageId).PackageTitle;
            return View();
        }
        [HttpPost]
        public virtual ActionResult AddDetiales(PackageProductVM vmPackageProduct)
        {
            var productList = _products.Where(ll => ll.IsActive == true).Select(p => new
            {
                ProductId = p.ProductId,
                ProductName = p.ProductGroup.ProductGroupTitle + " - " + p.ProductName
            });
            if (ModelState.IsValid && vmPackageProduct != null)
            {
                var packageProductList = _packageProduct.GetAll(vmPackageProduct.PackageId).ToList();
                if (packageProductList.Any(p => p.ProductId == vmPackageProduct.ProductId))
                {
                    ModelState.AddModelError("ProductId", "این محصول قبلا اضافه شده است");
                    TempData["res"] = "<script>showNotification('این محصول قبلا اضافه شده است', 'error','center')</script>";

                    ViewBag.ProductId = new SelectList(productList, "ProductId", "ProductName");
                    ViewBag.PackageId = vmPackageProduct.PackageId;
                    ViewBag.PackageTitle = _packages.Find(vmPackageProduct.PackageId).PackageTitle;
                    return View(vmPackageProduct);
                }
                else
                {
                    var newPackageProduct = new PackageProduct()
                    {
                        PackageId = vmPackageProduct.PackageId,
                        ProductId = vmPackageProduct.ProductId

                    };
                    _packageProduct.Add(newPackageProduct);
                    _uow.SaveAllChanges();
                    TempData["res"] = "<script>showNotification('رکورد جدید افزوده شد', 'success','center')</script>";
                    return RedirectToAction("ShowProp", new { id = (int)vmPackageProduct.PackageId });
                }
            }

            TempData["res"] = "<script>showNotification('مقادیر را درست وارد کنید', 'error','center')</script>";
            ViewBag.ProductId = new SelectList(productList, "ProductId", "ProductName");
            ViewBag.PackageId = vmPackageProduct.PackageId;
            ViewBag.PackageTitle = _packages.Find(vmPackageProduct.PackageId).PackageTitle;
            return View(vmPackageProduct);
        }

        [HttpPost]
        public virtual ActionResult DeleteProduct(int id)
        {
            try
            {
                var item = _packageProduct.Find(id);
                _packageProduct.Remove(_packageProduct.Find(id));
                _uow.SaveAllChanges();
                return Json(new { ret = "true", codeid = 1, packageId = item.PackageId });
            }
            catch
            {
                return Json(new { ret = "false", codeid = 2 });
            }

        }


        #endregion

        #region Picture

        public virtual ActionResult Gallery(int id)
        {
            ViewBag.Galleries = _Images.GetAll().Where(p => p.ItemId == id && p.Constant == "Package").ToList();
            return View(new Image()
            {
                ItemId = id,
                Constant = "Package"
            });
        }

        [HttpPost]
        public virtual ActionResult Gallery(Image galleries, HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                if (imgUp != null && imgUp.IsImage())
                {
                    galleries.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/Uploads/ImgProducts/" + galleries.ImageName));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Uploads/ImgProducts/" + galleries.ImageName),
                        Server.MapPath("/Uploads/ImgProducts/Thumb/" + galleries.ImageName));
                    galleries.Constant = "Package";
                    _Images.Add(galleries);
                    _uow.SaveAllChanges();
                }
            }

            return RedirectToAction("Gallery", new { id = galleries.ItemId });
        }

        public virtual ActionResult DeleteGallery(int id)
        {
            var gallery = _Images.Find(id);

            System.IO.File.Delete(Server.MapPath("/Uploads/ImgProducts/" + gallery.ImageName));
            System.IO.File.Delete(Server.MapPath("/Uploads/ImgProducts/Thumb/" + gallery.ImageName));

            _Images.Remove(gallery);
            _uow.SaveAllChanges();
            return RedirectToAction("Gallery", new { id = gallery.ItemId });
        }

        #endregion

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

