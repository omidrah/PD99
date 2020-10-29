using System.Net;
using System.Web.Mvc;
using ServiceLayer.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using DomainClass.Models;
using Web.Models;

namespace Web.Controllers
{
    [OverrideAuthorization]
    public partial class ProductsController : Controller
    {
        private readonly IProductService _product;
        private IProductGroupService _productGroup;
        private IProductFeaturesService _productFeatures;
        private IFeaturesService _features;
        private ITagService _tags;
        IImageService _Images;

        public ProductsController(IProductService products, IProductGroupService productGroup, IProductFeaturesService productFeatures
            , IFeaturesService features, ITagService tags, IImageService images)
        {
            _product = products;
            _productGroup = productGroup;
            _productFeatures = productFeatures;
            _features = features;
            _tags = tags;
            _Images = images;
        }


        // GET: Products
        public virtual ActionResult Index()
        {
            //var products = _db.Products.Include(p => p.ProductGroup);
           // return View(_product.GetAll());
            return View();
        }

        // GET: Products/Details/5
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _product.Find((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }

        public virtual ActionResult ShowGroups()
        {
            return PartialView(_productGroup.GetAll());
        }

        public virtual ActionResult LastProducts()
        {
            return PartialView(_product.Select().OrderByDescending(s => s.CreatedDate).Take(6));
        }
        //  [Route("ShowProduct/{id}")]
        public virtual ActionResult ShowProduct(int id)
        {
            var product = _product.Find(id);
            ViewBag.ProductGallery = _Images.GetAll().Where(ll => ll.ItemId == id && ll.Constant == "Product");
            ViewBag.ProductFeatures = _productFeatures.GetAllByProductId(id).Select(f=> new ShowProductFeatureViewModel()
            {
                FeatureTitle = f.Features.featuresDispaly,
                Values = _productFeatures.GetAllByFeatureId(f.FeatureId).Where(p=>p.ProductId == id).Select(g=>g.FeatureValue).ToList()
            });
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.Tags = _tags.GetAllByItemId(id, "Product");
            return View(product);
        }

        [Route("Archive")]
        public virtual ActionResult ArchiveProduct(int pageId = 1, string title = "", int minPrice = 0, int maxPrice = 0, List<int> selectedGroups = null)
        {
            ViewBag.Groups = _productGroup.GetAll().ToList();
            ViewBag.productTitle = title;
            ViewBag.minPrice = minPrice;
            ViewBag.maxPrice = maxPrice;
            ViewBag.pageId = pageId;
            ViewBag.selectGroup = selectedGroups;
            List<Product> list = new List<Product>();
            // list = _product.GetAll().ToList();
            if (selectedGroups != null && selectedGroups.Any())
            {
                foreach (int group in selectedGroups)
                {
                    list.AddRange(_product.GetAll().Where(g => g.ProductGroupID == group).ToList());
                }
                list = list.Distinct().ToList();
            }
            else
            {
                list.AddRange(_product.GetAll().ToList());
            }


            if (title != "")
            {
                list = list.Where(p => p.ProductName.Contains(title)).ToList();
            }
            if (minPrice > 0)
            {
                list = list.Where(p => p.Price >= minPrice).ToList();
            }
            if (maxPrice > 0)
            {
                list = list.Where(p => p.Price <= maxPrice).ToList();
            }

            //Pagging
            int take = 6;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = list.Count() / take;
            return View(list.OrderByDescending(p => p.CreatedDate).Skip(skip).Take(take).ToList());
        }
    }
}