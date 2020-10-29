using DataLayer.Context;
using DomainClass.Models;
using InsertShowImage;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KooyWebApp_MVC.Classes;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using Web.Classes;

namespace Web.Areas.Admin.Controllers
{
    public partial class ProductsController : Controller
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
        public ProductsController(IUnitOfWork uow, IProductService product, IProductGroupService productGroup
            , IImageService images, IApplicationUserManager user, IPackageProductService packageProducts, IPackageService packages
            , ITagService tags, IProductFeaturesService productFeatures, IProductGroupFeaturesService productGroupFeatures
            , IFeaturesService features)
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
        }
        // GET: Admin/Products
        // #region Package
        public virtual ActionResult Index()
        {
            var list = _products.Select().ToList().Select(item => new ProductVM
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                ProductGroupID = item.ProductGroupID,
                Price = item.Price,
                ProductMojodi = item.ProductMojodi,
                NoghteSefaresh = item.NoghteSefaresh,
                OffPercent = item.OffPercent,
                IsActive = item.IsActive,
                Weight = item.Weight,
                ProductGroupTitle = _productGroup.Find(item.ProductGroupID).ProductGroupTitle,
                PicPath = item.PicPath
            });
            return View(list.OrderBy(ll => ll.ProductName));

        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            ViewBag.ProductGroupID = new SelectList(_productGroup.GetAll(), "ProductGroupId", "ProductGroupTitle");
            ViewBag.Packages = _packages.GetAll();
            return View();
        }

        // POST: Admin/Products1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(ProductVM productVM, HttpPostedFileBase PicPath)
        {
            //if (PicPath == null)
            //{
            //    ModelState.AddModelError("PicPath", "حدقل یک عکس برای منبع انتخاب کنید");
            //}
            if (ModelState.IsValid && productVM != null)
            {
                var newProduct = new Product()
                {
                    Price = productVM.Price,
                    OffPercent = productVM.OffPercent,
                    IsActive = productVM.IsActive,
                    ProductMojodi = productVM.ProductMojodi,
                    ProductName = productVM.ProductName,
                    NoghteSefaresh = productVM.NoghteSefaresh,
                    ProductGroupID = productVM.ProductGroupID,
                    Summery = productVM.Summery,
                    Weight = productVM.Weight,
                    ProductEnglishName = productVM.ProductEnglishName,
                    Createdby = _user.GetCurrentUserId(),
                    CreatedDate = DateTime.Now,
                    Description = productVM.Description
                };
                // _uow.SaveAllChanges();
                #region Save Uploaded Image 

                //if (PicPath != null && PicPath.Count() > 0)
                //{
                //    foreach (var item in PicPath)
                //    {
                //        var filename = Path.GetFileName(item.FileName);
                //        var newFilename = Guid.NewGuid().ToString()
                //                                 .Replace("-", string.Empty)
                //                                 + Path.GetExtension(filename);

                //        var newFilenameUrl = "/Uploads/ImgProducts/" + newFilename;
                //        string physicalFilename = Server.MapPath(newFilenameUrl);
                //        item.SaveAs(physicalFilename);
                //        var thumbnailUrl = "/Uploads/ImgProducts/" + Utils.CreateThumbnail(physicalFilename, 500, 375);
                //        _Images.Add(
                //            new Image
                //            {
                //                ImagePath = newFilenameUrl,
                //                ImageThumbPath = thumbnailUrl,
                //                ItemId = newProduct.ProductId,
                //                CreatedDate = DateTime.Now,
                //                Createdby = _user.GetCurrentUserId(),
                //                Constant = "Product"

                //            });
                //        newProduct.PicPath = thumbnailUrl;
                //    }
                //    _products.Add(newProduct);

                //}

                newProduct.PicPath = "images.jpg";
                if (PicPath != null && PicPath.IsImage())
                {
                    newProduct.PicPath = Guid.NewGuid().ToString() + Path.GetExtension(PicPath.FileName);
                    PicPath.SaveAs(Server.MapPath("/Uploads/ImgProducts/" + newProduct.PicPath));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Uploads/ImgProducts/" + newProduct.PicPath),
                        Server.MapPath("/Uploads/ImgProducts/Thumb/" + newProduct.PicPath));
                }
                #endregion
                _products.Add(newProduct);
                if (productVM.SelectedPackages != null && productVM.SelectedPackages.Any())
                {
                    foreach (var item in productVM.SelectedPackages)
                    {
                        _packageProduct.Add(new PackageProduct
                        {
                            ProductId = newProduct.ProductId,
                            PackageId = item
                        });
                    }
                }

                if (!string.IsNullOrEmpty(productVM.tags))
                {
                    string[] tag = productVM.tags.Split('-');
                    foreach (string t in tag)
                    {
                        _tags.Add(new Tags()
                        {
                            itemId = newProduct.ProductId,
                            tagTitle = t.Trim(),
                            tagConstant = "Product"
                        });
                    }
                }
                _uow.SaveAllChanges();
                TempData["res"] = "<script>showNotification('رکورد جدید افزوده شد', 'success','center')</script>";
                return RedirectToAction("Index");
            }

            ViewBag.ProductGroupID = new SelectList(_productGroup.GetAll(), "ProductGroupId", "ProductGroupTitle", productVM.ProductGroupID);
            ViewBag.Packages = _packages.GetAll();
            return View(productVM);
        }


        public virtual ActionResult Edit(int id)
        {
            var curRec = _products.Find(id);
            ViewBag.ProductGroupID = new SelectList(_productGroup.GetAll(), "ProductGroupId", "ProductGroupTitle", curRec.ProductGroupID);
            ViewBag.Packages = _packages.GetAll();
            List<int> list = _packageProduct.GetAllPackage(id).Select(m => m.PackageId).ToList();
            ViewBag.Tags = string.Join("-", _tags.GetAllByItemId(id, "Product").Select(t => t.tagTitle).ToList());

            return View(new ProductVM
            {
                ProductName = curRec.ProductName,
                ProductEnglishName = curRec.ProductEnglishName,
                Description = curRec.Description,
                Summery = curRec.Summery,
                ProductGroupID = curRec.ProductGroupID,
                Price = curRec.Price,
                OffPercent = curRec.OffPercent,
                NoghteSefaresh = curRec.NoghteSefaresh,
                ProductId = curRec.ProductId,
                ProductMojodi = curRec.ProductMojodi,
                SelectedPackages = list,
                PicPath = curRec.PicPath,
                Weight = curRec.Weight,
                IsActive = curRec.IsActive

            }
                );
        }
        [HttpPost]
        public virtual ActionResult Edit(ProductVM vmProduct, HttpPostedFileBase imageProduct)
        {
            if (ModelState.IsValid && vmProduct != null)
            {
                var curProduct = _products.Find(vmProduct.ProductId);
                curProduct.ProductName = vmProduct.ProductName;
                curProduct.IsActive = vmProduct.IsActive;
                curProduct.ProductEnglishName = vmProduct.ProductEnglishName;
                curProduct.ProductGroupID = vmProduct.ProductGroupID;
                curProduct.Summery = vmProduct.Summery;
                curProduct.ProductGroupID = vmProduct.ProductGroupID;
                curProduct.ProductMojodi = vmProduct.ProductMojodi;
                curProduct.Description = vmProduct.Description;
                curProduct.Price = vmProduct.Price;
                curProduct.OffPercent = vmProduct.OffPercent;
                curProduct.NoghteSefaresh = vmProduct.NoghteSefaresh;
                curProduct.Modifiedby = _user.GetCurrentUserId();
                curProduct.Weight = vmProduct.Weight;
                curProduct.ModifiedDate = DateTime.Now;


                #region Save Uploaded Image 

                //if (PicPath != null && PicPath.Count() > 0)
                //{
                //    foreach (var item in PicPath)
                //    {
                //        var filename = Path.GetFileName(item.FileName);
                //        var newFilename = Guid.NewGuid().ToString()
                //                                 .Replace("-", string.Empty)
                //                                 + Path.GetExtension(filename);

                //        var newFilenameUrl = "/Uploads/ImgProducts/" + newFilename;
                //        string physicalFilename = Server.MapPath(newFilenameUrl);
                //        item.SaveAs(physicalFilename);
                //        var thumbnailUrl = "/Uploads/ImgProducts/" + Utils.CreateThumbnail(physicalFilename, 500, 375);
                //        _Images.Add(
                //            new Image
                //            {
                //                ImagePath = newFilenameUrl,
                //                ImageThumbPath = thumbnailUrl,
                //                ItemId = curProduct.ProductId,
                //                CreatedDate = DateTime.Now,
                //                //ModifiedDate = DateTime.Now,
                //                Createdby = _user.GetCurrentUserId(),
                //                Constant = "Product"
                //                //ModifiedBy = id,
                //                //  IsDeleted = false
                //            });
                //    }
                //}
                // curProduct.PicPath = "images.jpg";
                if (imageProduct != null && imageProduct.IsImage())
                {
                    if (curProduct.PicPath != "images.jpg")
                    {
                        System.IO.File.Delete(Server.MapPath("/Uploads/ImgProducts/Thumb/" + curProduct.PicPath));
                        System.IO.File.Delete(Server.MapPath("/Uploads/ImgProducts/" + curProduct.PicPath));
                    }

                    curProduct.PicPath = Guid.NewGuid().ToString() + Path.GetExtension(imageProduct.FileName);
                    imageProduct.SaveAs(Server.MapPath("/Uploads/ImgProducts/" + curProduct.PicPath));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Uploads/ImgProducts/" + curProduct.PicPath),
                        Server.MapPath("/Uploads/ImgProducts/Thumb/" + curProduct.PicPath));
                }
                #endregion

                foreach (var item in _packageProduct.GetAllPackage(vmProduct.ProductId))
                {
                    _packageProduct.Remove(item);
                }
                if (vmProduct.SelectedPackages != null)
                {
                    foreach (int selectedGroup in vmProduct.SelectedPackages)
                    {
                        _packageProduct.Add(new PackageProduct()
                        {
                            ProductId = curProduct.ProductId,
                            PackageId = selectedGroup
                        });
                    }
                }

                var tags = _tags.GetAllByItemId(vmProduct.ProductId, "Product");
                foreach (var tag in tags)
                {
                    _tags.Remove(tag);
                }
                if (!string.IsNullOrEmpty(vmProduct.tags))
                {
                    string[] tag = vmProduct.tags.Split('-');
                    foreach (string t in tag)
                    {
                        _tags.Add(new Tags()
                        {
                            itemId = vmProduct.ProductId,
                            tagTitle = t.Trim(),
                            tagConstant = "Product"
                        });
                    }
                }

                _uow.SaveAllChanges();
                TempData["res"] = "<script>showNotification('رکورد جدید بروز شد', 'success','center')</script>";
                return RedirectToAction("Index");
            }
            TempData["res"] = "<script>showNotification('مقادیر را درست وارد کنید', 'success','center')</script>";
            return View(vmProduct);

        }

        #region ProductPicture

        public virtual ActionResult Gallery(int id)
        {
            ViewBag.Galleries = _Images.GetAll().Where(p => p.ItemId == id && p.Constant == "Product").ToList();
            return View(new Image()
            {
                ItemId = id,
                Constant = "Product"
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
                    galleries.Constant = "Product";
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
        //public virtual ActionResult ShowPicture(int? ItemId)
        //{
        //    if (ItemId == null)
        //    {
        //        return RedirectToAction("index", "Products");
        //    }
        //    ViewBag.ItemId = ItemId;
        //    return View(GetImagesProduct(ItemId));
        //}
        //public virtual ActionResult ImagesProduct_Read([DataSourceRequest] DataSourceRequest request, int? ItemId)
        //{
        //    ViewBag.ItemId = ItemId;
        //    return Json(GetImagesProduct(ItemId).ToDataSourceResult(request));
        //}
        //private IEnumerable<ProductImageViewModel> GetImagesProduct(int? ItemId)
        //{
        //    if (ItemId == null) { return null; }
        //    var pictureImages = _Images.GetAll().Where(ll => ll.ItemId == ItemId);
        //    if (pictureImages.Count() > 0)
        //    {
        //        return pictureImages.Select(aa => new ProductImageViewModel
        //        {

        //            ItemId = aa.ItemId,
        //            ImagePath = aa.ImagePath,
        //            ImageThumbPath = aa.ImageThumbPath,
        //            IsActive = aa.IsActive,
        //            ImageID = aa.ImageID
        //        });
        //    }
        //    else
        //    {
        //        return null;
        //    };
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public virtual ActionResult DeletePicture([DataSourceRequest] DataSourceRequest request, ProductImageViewModel pImage)
        //{
        //    try
        //    {
        //        var curImg = _Images.Find(pImage.ImageID);
        //        var packageId = (int)curImg.ItemId;

        //        if (System.IO.File.Exists(Server.MapPath("~/" + curImg.ImagePath)))
        //        {
        //            System.IO.File.Delete(Server.MapPath("~/" + curImg.ImagePath));
        //        }
        //        if (System.IO.File.Exists(Server.MapPath("~/" + curImg.ImageThumbPath)))
        //        {
        //            System.IO.File.Delete(Server.MapPath("~/" + curImg.ImageThumbPath));
        //        }
        //        _Images.Remove(curImg);
        //        _uow.SaveAllChanges();
        //        return Json(new { ret = "true", codeid = 1, packageId = packageId }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}",
        //                                        validationError.PropertyName,
        //                                        validationError.ErrorMessage);
        //                return Json(new { ret = "false", codeid = 2 }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        return Json(new { ret = "false", codeid = 2 }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        #endregion

        #region Features
        public virtual ActionResult ProductFeatures(int id)
        {
            ViewBag.Features = _productFeatures.GetAllByProductId(id).ToList();
            ViewBag.FeatureId = new SelectList(_features.GetAll(), "FeatureId", "featuresDispaly");
            return View(new ProductFeaturesVM()
            {
                ProductId = id
            }
            );
        }

        [HttpPost]
        public virtual ActionResult ProductFeatures(ProductFeaturesVM productFeaturesVm)
        {
            var item = new ProductFeatures()
            {
                ProductId = productFeaturesVm.ProductId,
                FeatureId = productFeaturesVm.FeatureId,
                FeatureValue = productFeaturesVm.FeatureValue,
                IsActive = true,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                CreatedBy = _user.GetCurrentUserId()
            };
            if (ModelState.IsValid)
            {
                _productFeatures.Add(item);
                _uow.SaveAllChanges();
            }
            return RedirectToAction("ProductFeatures", new { id = productFeaturesVm.ProductId });
        }


        public void DeleteFeature(int id)
        {
            var feature = _productFeatures.Find(id);
            _productFeatures.Remove(feature);
            _uow.SaveAllChanges();
        }
        #endregion
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
                //_products.Find(id).IsDeleted = true;
                _products.Delete(id);
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
