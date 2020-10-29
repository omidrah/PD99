using DomainClass.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [OverrideAuthorization]
    public partial class PackegesController : Controller
    {
        private readonly IPackageService _packages;
        private readonly IDoorehService _dooreh;
        private readonly IKelassService _kelass;
        private readonly IPackageProductService _packageProduct;
        private readonly IProductService _products;
        private readonly IDoorehKelassService _doorehKelass;
        private readonly IDourseService _douros;
        private readonly IApplicationUserManager _user;
        private readonly IImageService _Images;
        private readonly ITagService _tags;

        public PackegesController(IPackageService packages, IDoorehService dooreh, IKelassService kelass, 
            IPackageProductService packageProduct, IProductService products, IDoorehKelassService doorehKelass,
            IDourseService dourus,IApplicationUserManager user,IImageService images, ITagService tags)
        {
            _packages = packages;
            _dooreh = dooreh;
            _kelass = kelass;
            _products = products;
            _packageProduct = packageProduct;
            _doorehKelass = doorehKelass;
            _douros = dourus;
            _user = user;
            _Images = images;
            _tags = tags;

        }
        public virtual ActionResult ShowPackages()
        {
            ViewBag.Dooreh = _dooreh.GetAll();
            //var Classes = _kelass.GetAll();
            //List<KelassVM> kelsses = new List<KelassVM>();
            //foreach(var item  in Classes)
            //{
            //    kelsses.Add(new KelassVM
            //    {
            //        KelassId = item.KelassId,
            //        dorusTitle = item.Doruses.Title?? ""
            //        //Cost = a.Cost,
            //        //EndDate = a.EndDate,
            //        //DourseId = a.DourseId,
            //        //IsActive = a.IsActive,
            //        //HasVideo = a.HasVideo,
            //        //IsHozori = a.IsHozori,
            //        //IsOnline = a.IsOnline,
            //        //MahaleBargozari = a.MahaleBargozari.MahalTitle,
            //        //MahaleBargozariId = a.MahaleBargozariId,
            //        //MasterId = a.MasterId,
            //        //MasterName = "",
            //        //StartDate = a.StartDate


            //        // MasterName = a.Masters.Users.Name
            //    });
                 
            //}  
                
             
              
     
            //ViewBag.Kelass = kelsses.ToList();
            return PartialView(_packages.GetAll());
        }

        public virtual ActionResult LastPackages()
        {
            return PartialView(_packages.GetAll().OrderByDescending(s => s.CreateDate).Take(6));
        }
        [Route("ArchivePackage")]
        public virtual ActionResult ArchivePackage(int pageId = 1)
        {
            List<Packages> list = new List<Packages>();
            list.AddRange(_packages.GetAll().ToList());
            //int take = 6;
            //int skip = (pageId - 1) * take;
            // ViewBag.PageCount = list.Count() / take;
            return View(list.OrderByDescending(p => p.CreateDate).ToList());//Skip(skip).Take(take).ToList());
        }

        [Route("ArchiveOnline")]
        public virtual ActionResult ArchiveOnline(int pageId = 1)
        {
            return View(_dooreh.GetAll().Where(k => k.IsOnline == true).OrderByDescending(s => s.CreateDate));
        }

        [Route("ArchiveHozori")]
        public virtual ActionResult ArchiveHozori(int pageId = 1)
        {
            return View(_dooreh.GetAll().Where(k => k.IsHozori == true).OrderByDescending(s => s.CreateDate));
        }
        [Route("PackageContents")]
        public virtual ActionResult PackageContents(int packageId)
        {
            List<Product> list = new List<Product>();
            foreach (var item in _packageProduct.GetAll(packageId))
            {
                list.Add(_products.Find(item.ProductId));
            }
            ViewBag.PackageName = _packages.Find(packageId).PackageTitle;
            return View(list.ToList());
        }

        [Route("DoorehContents")]
        public virtual ActionResult DoorehContents(int doorehId)
        {
            var curDooreh = _dooreh.Find(doorehId);
            var klases = _kelass.GetAll().Where(k => k.IsActive == true && k.IsOnline == curDooreh.IsOnline && k.IsHozori == curDooreh.IsHozori).Select(a => new Kelas
            {
                KelassId = a.KelassId,
                KlassName = _douros.Find(a.DourseId).Title + "- استاد " + _user.FindById(a.MasterId).Name + " " + _user.FindById(a.MasterId).Family // + " " + a.StartDate.Value.ToLongDateString() + "  الی  " + a.EndDate.Value.ToLongDateString()
            });

            List<Kelass> list = new List<Kelass>();
            foreach (var item in _doorehKelass.GetAllKelass(doorehId))
            {
                list.Add(_kelass.Find(item.KelassId));
            }
            ViewBag.DoorehTitle = _dooreh.Find(doorehId).Title;
            return View(klases.ToList());
        }
        [Route("PackageDetails")]
        public virtual ActionResult PackageDetails(int id)
        {
            var package = _packages.Find(id);
            ViewBag.PackageGallery = _Images.GetAll().Where(ll => ll.ItemId == id && ll.Constant == "Package");
            List<Product> products = new List<Product>();
            foreach (var item in _packageProduct.GetAll(id))
            {
                products.Add(_products.Find(item.ProductId));
            }
            ViewBag.PackageProducts = products;
            if (package == null)
            {
                return HttpNotFound();
            }

            ViewBag.Tags = _tags.GetAllByItemId(id, "Package");
            return View(package);
        }
    }
}