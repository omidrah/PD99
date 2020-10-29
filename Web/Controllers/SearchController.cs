using DataLayer.Context;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainClass.Models;
using Web.Models;

namespace Web.Controllers
{
    [OverrideAuthorization]
    public partial class SearchController : Controller
    {
        private readonly IGalleryService _gallery;
        private readonly IMasterService _master;
        private readonly IProductGroupService _productGroup;
        private readonly IProductService _products;
        private readonly IUnitOfWork _uow;
        private readonly IImageService _Images;
        private readonly IPackageService _packages;
        private readonly IDoorehService _dooreh;
        private readonly ITagService _tags;


        public SearchController(IUnitOfWork uow, IGalleryService gallery, IMasterService master, IProductGroupService productGroup, IProductService product
            , IImageService images, IPackageService packages, IDoorehService dooreh, ITagService tags)
        {
            _gallery = gallery;
            _master = master;
            _uow = uow;
            _productGroup = productGroup;
            _products = product;
            _Images = images;
            _packages = packages;
            _dooreh = dooreh;
            _tags = tags;
        }
        // GET: Search
        public virtual ActionResult Index1(string q)
        {
            List<Product> list = new List<Product>();
            list.AddRange(_tags.GetAllByTagTitle(q).Where(p => p.tagConstant == "Product").Select(s => _products.Find(s.itemId)));
            list.AddRange(_products.GetAll().Where(p => p.ProductName.Contains(q) || p.ProductEnglishName.Contains(q) || p.Summery.Contains(q) || p.Description.Contains(q)));
            ViewBag.search = q;
            return View(list.Distinct());
        }

        public virtual ActionResult Index(string q)
        {
            ComponentViewModel list = new ComponentViewModel();

            List<Product> productList = new List<Product>();
            productList.AddRange(_tags.GetAllByTagTitle(q).Where(p => p.tagConstant == "Product").Select(s => _products.Find(s.itemId)));
            productList.AddRange(_products.GetAll().Where(p => p.ProductName.Contains(q)));// || p.ProductEnglishName.Contains(q) || p.Summery.Contains(q) || p.Description.Contains(q)));
            list.Products = productList.Distinct();

            List<Packages> packageList = new List<Packages>();
            packageList.AddRange(_tags.GetAllByTagTitle(q).Where(p => p.tagConstant == "Package").Select(s => _packages.Find(s.itemId)));
            packageList.AddRange(_packages.GetAll().Where(p => p.PackageTitle.Contains(q)));// || p.Summery.Contains(q) || p.PackageDescription.Contains(q)));
            list.Packages = packageList.Distinct();

            List<Dooreha> doorehList = new List<Dooreha>();
            doorehList.AddRange(_tags.GetAllByTagTitle(q).Where(p => p.tagConstant == "Dooreh").Select(s => _dooreh.Find(s.itemId)));
            doorehList.AddRange(_dooreh.GetAll().Where(d => d.Title.Contains(q)));// || d.Goal.Contains(q) || d.Appendix.Contains(q) || d.Content.Contains(q)));
            list.Dooreh = doorehList.Distinct();

            ViewBag.search = q;
            ViewBag.Products = productList;
            ViewBag.Packages = packageList;
            ViewBag.Dooreh = doorehList;

            return View();
        }
    }
}