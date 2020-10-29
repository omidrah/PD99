using System.Web.Mvc;
using DataLayer.Context;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System.Collections.Generic;

namespace Web.Controllers
{
    [OverrideAuthorization]
    public partial class HomeController : Controller
    {
        private readonly IGalleryService _gallery;
        private readonly IMasterService _master;
        private readonly IProductGroupService _productGroup;
        private readonly IProductService _products;
        private readonly IUnitOfWork _uow;
        private readonly IImageService _Images;

        public HomeController(IUnitOfWork uow, IGalleryService gallery, IMasterService master, IProductGroupService productGroup, IProductService product
            , IImageService images)
        {
            _gallery = gallery;
            _master = master;
            _uow = uow;
            _productGroup = productGroup;
            _products = product;
            _Images = images;
        }

        public virtual ActionResult Index()
        {
            var result = _products.GetAll();
            List<ProductVM> productVM = new List<ProductVM>(); //= new Enumerable<ProductVM>();
            //foreach (var item in result)
            //{
            //    ProductVM pVM = new ProductVM()
            //    {
            //        ProductId = item.ProductId,
            //        IsActive = item.IsActive,
            //        Price = item.Price,
            //        //  ProductCode = item.pro
            //        ProductName = item.ProductName,
            //        NoghteSefaresh = item.NoghteSefaresh,
            //        ProductMojodi = item.ProductMojodi,
            //        Summery = item.Summery,
            //        ProductEnglishName = item.ProductEnglishName,
            //        Description = item.Description
            //       // PicPath = _Images.FindByItemId(item.ProductId).ImagePath ?? ""


            //    };


            //    productVM.Add(pVM);
            //}

            return View(productVM);
        }

        public virtual ActionResult NotFound()
        {
            return View();
        }

        [ChildActionOnly]
        public virtual ActionResult FillSlider()
        {
            return PartialView(MVC.Home.Views._Slider, _gallery.GetAllActive());
        }

        [ChildActionOnly]
        public virtual ActionResult FillResource()
        {
            var result = _productGroup.GetAllActive();
            return PartialView(MVC.Home.Views._Resource, result);
            //return PartialView();
        }

        [ChildActionOnly]
        //  [HttpPost]
        public virtual ActionResult FillResourceByType(int resourceType = 1)
        {
            var result = _products.SelectByType(resourceType);
            List<ProductVM> productVM = new List<ProductVM>(); //= new Enumerable<ProductVM>();
            foreach (var item in result)
            {
                ProductVM pVM = new ProductVM()
                {
                    ProductId = item.ProductId,
                    IsActive = item.IsActive,
                    Price = item.Price,
                    //  ProductCode = item.pro
                    ProductName = item.ProductName,
                    NoghteSefaresh = item.NoghteSefaresh,
                    ProductMojodi = item.ProductMojodi,
                    Summery = item.Summery,
                    ProductEnglishName = item.ProductEnglishName,
                    Description = item.Description,
                    PicPath = _Images.FindByItemId(item.ProductId).ImagePath ?? ""
                };
                productVM.Add(pVM);
            }
            return PartialView(MVC.Home.Views._ResourceByType, productVM);
            //return PartialView();
        }

        [ChildActionOnly]
        public virtual ActionResult FillAsatid()
        {
            return PartialView(MVC.Home.Views._Asatid, _master.InfoAll());
        }

        public virtual ActionResult AboutUs()
        {
            return View();
        }
    }
}