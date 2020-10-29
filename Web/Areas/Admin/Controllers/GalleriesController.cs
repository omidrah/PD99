using System.Net;
using System.Web.Mvc;
using System.Web;
using System.IO;
using System;
using DataLayer.Context;
using ServiceLayer.Interfaces;
using DomainClass.Models;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    public partial class GalleriesController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IGalleryService _gallery;
        public GalleriesController(IUnitOfWork uow, IGalleryService gallery)
        {
            _uow = uow;
            _gallery = gallery;
        }
        
        // GET: AdminRoot/Galleries
        public virtual ActionResult Index()
        {
            return View(_gallery.BGetAllActive());
        }
        public virtual ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(CrudSliderVm gallery, HttpPostedFileBase PicPath)
        {
            if (ModelState.IsValid)
            {
                int id;
                int.TryParse(User.Identity.Name, out id);
                var newGal = new Gallery
                {
                    imgIsActive = gallery.imgIsActive,
                    CreatedBy = id,
                    CreatedDate = DateTime.Now,
                    imgDesc = gallery.imgDesc,
                    imgExtension = gallery.imgExtension,
                    imgHeight = gallery.imgHeight,
                    imgName = gallery.imgName,
                    imgPriority = gallery.imgPriority,
                    imgTitle = gallery.imgTitle,
                    NavigateUrl = gallery.NavigateUrl,
                    imgWidth = gallery.imgWidth
                };
                _gallery.Add(newGal);                
                #region Save Uploaded Image 
                if (PicPath != null)
                {
                    var filename = Path.GetFileName(PicPath.FileName);
                    var newFilename = Guid.NewGuid().ToString("N")+ Path.GetExtension(filename);
                    var newFilenameUrl = "/Uploads/SlidersImage/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    PicPath.SaveAs(physicalFilename);
                    var thumbnailUrl = Utils.CreateThumbnail(physicalFilename);
                    newGal.imgPathThumb = thumbnailUrl;
                    newGal.imgPath = newFilenameUrl;
                }
                #endregion              
                _uow.SaveAllChanges();
                return RedirectToAction("Index");
            }
            return View(gallery);
        }

        // GET: AdminRoot/Galleries/Edit/5
        public virtual ActionResult Edit(int? id)
        {
            if (id == null || id<=0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = _gallery.FindById((int)id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(new CrudSliderVm { Id=gallery.Id, imgDesc  = gallery.imgDesc, imgIsActive = gallery.imgIsActive, imgWidth = gallery.imgWidth, imgTitle = gallery.imgTitle,

             imgName = gallery.imgName, NavigateUrl = gallery.NavigateUrl, imgHeight=gallery.imgHeight, imgPath=gallery.imgPath, imgPathThumb= gallery.imgPathThumb, imgPriority = gallery.imgPriority});
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(CrudSliderVm gallery, HttpPostedFileBase PicPath)
        {
            if (ModelState.IsValid)
            {
                int id;
                int.TryParse(User.Identity.Name, out id);
                var curgallery = _gallery.FindById(gallery.Id);
                #region Save Uploaded Image 

                if (PicPath != null)
                {
                    //remove preceed picture
                    if (System.IO.File.Exists(Server.MapPath("~/" + gallery.imgPath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + gallery.imgPath));
                    }
                    if (System.IO.File.Exists(Server.MapPath("~/" + gallery.imgPathThumb)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + gallery.imgPathThumb));
                    }

                    var filename = Path.GetFileName(PicPath.FileName);
                    var newFilename = Guid.NewGuid().ToString()
                                             .Replace("-", string.Empty)
                                             + Path.GetExtension(filename);

                    var newFilenameUrl = "/Uploads/SlidersImage/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    PicPath.SaveAs(physicalFilename);
                    var thumbnailUrl = Utils.CreateThumbnail(physicalFilename);

                    curgallery.imgPath = newFilenameUrl;
                    curgallery.imgPathThumb = thumbnailUrl;
                }
                #endregion                    
                curgallery.ModifiedDate = DateTime.Now;
                curgallery.Modifyby = id;

                curgallery.imgDesc = gallery.imgDesc;
                curgallery.imgIsActive = gallery.imgIsActive;
                curgallery.imgName = gallery.imgName;
                curgallery.imgTitle = gallery.imgTitle;
                curgallery.NavigateUrl = gallery.NavigateUrl;
                curgallery.imgHeight = gallery.imgHeight;
                curgallery.imgWidth = gallery.imgWidth;
                curgallery.imgPriority = gallery.imgPriority;

                _uow.MarkAsChanged(curgallery);
                _uow.SaveAllChanges();
                return RedirectToAction("Index");
            }
            return View(gallery);
        }

        [HttpPost]
        public virtual ActionResult Delete(int id)
        {
            try
            {                
                var result =  _gallery.Delete(_gallery.FindById(id));
                if (result == null)
                {
                    return Json(new { ret = "false", codeid = 3 }, JsonRequestBehavior.AllowGet);
                }
                _uow.SaveAllChanges();
                //remove preceed picture
                if (System.IO.File.Exists(Server.MapPath("~/" + result.imgPath )))
                {
                    System.IO.File.Delete(Server.MapPath("~/" + result.imgPath));
                }
                if (System.IO.File.Exists(Server.MapPath("~/" + result.imgPathThumb)))
                {
                    System.IO.File.Delete(Server.MapPath("~/" + result.imgPathThumb));
                }

                return Json(new { ret = "true", codeid = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { ret = "false", codeid = 2 }, JsonRequestBehavior.AllowGet);
            }

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
