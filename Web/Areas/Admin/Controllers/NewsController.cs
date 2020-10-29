using DataLayer.Context;
using DomainClass.Models;
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

namespace Web.Areas.Admin.Controllers
{
    public partial class NewsController : Controller
    {
        IUnitOfWork _uow;//db
        INewsService _news;
        IPersonelService _personel;
        IApplicationUserManager _user;
        public NewsController(IUnitOfWork uow, INewsService news, IPersonelService personel,
         IApplicationUserManager user)
        {
            _uow = uow;
            _user = user;
            _personel = personel;
            _news = news;

        }
        // GET: Admin/News
        public virtual ActionResult Index()
        {
            return View(_news.GetAll().Where(ll => ll.IsDeleted == false && ll.ParentId != null).Select(
                aa => new NewsViewModel
                {
                    IsAuthenticated = aa.IsAuthenticated,
                    NewsBody = aa.NewsBody,
                    NewsTitle = aa.NewsTitle,
                    MasterPicPathThumb = aa.MasterPicPathThumb,
                    NewsId = aa.NewsId,
                    ParentId = (int)aa.ParentId
                }
                ));
        }


        private void PopulateNewsGroups()
        {
            ViewBag.ParentId = new SelectList(_news.GetAll().Where(ll => ll.IsDeleted == false && ll.ParentId == null), "NewsId", "NewsTitle");
        }
        public virtual ActionResult Create()
        {
            PopulateNewsGroups();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(NewsViewModel news, HttpPostedFileBase PicPath)
        {
            if (ModelState.IsValid)
            {

                int id;
                int.TryParse(User.Identity.Name, out id);

                var NewNews = new News
                {
                    IsAuthenticated = news.IsAuthenticated,
                    IsDeleted = false,
                    CreatedBy = id,
                    CreatedDate = DateTime.Now,
                    NewsBody = news.NewsBody,
                    HeadTitle = news.HeadTitle,
                    NewsTitle = news.NewsTitle,
                    ParentId = news.ParentId,

                };

                _news.Add(NewNews);
                #region Save Uploaded Image 

                if (PicPath != null)
                {
                    var filename = Path.GetFileName(PicPath.FileName);
                    var newFilename = Guid.NewGuid().ToString("N") + Path.GetExtension(filename);

                    var newFilenameUrl = "/Uploads/NewsImages/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    PicPath.SaveAs(physicalFilename);
                    var thumbnailUrl = "/Uploads/NewsImages/" + Utils.CreateThumbnail(physicalFilename);
                    NewNews.MasterPicPathThumb = thumbnailUrl;
                    NewNews.MasterPicPath = newFilenameUrl;

                }
                #endregion              
                _uow.SaveAllChanges();
                return RedirectToAction("Index");
            }
            PopulateNewsGroups();
            return View(news);
        }
        public virtual ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var news = _news.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            PopulateNewsGroups();
            return View(new NewsViewModel
            {
                IsAuthenticated = news.IsAuthenticated,
                MasterPicPathThumb = news.MasterPicPathThumb,
                NewsBody = news.NewsBody,
                NewsTitle = news.NewsTitle,
                HeadTitle = news.HeadTitle,
                ParentId = (int)news.ParentId,
                NewsId = news.NewsId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(NewsViewModel news, HttpPostedFileBase PicPath)
        {
            if (ModelState.IsValid && news != null)
            {
                int id;
                int.TryParse(User.Identity.Name, out id);

                var curNews = _news.Find(news.NewsId);

                curNews.ParentId = news.ParentId;
                curNews.ModifiedBy = id;
                curNews.ModifiedDate = DateTime.Now;
                curNews.NewsBody = news.NewsBody;
                curNews.NewsTitle = news.NewsTitle;
                curNews.HeadTitle = news.HeadTitle;
                curNews.IsAuthenticated = news.IsAuthenticated;

                #region Save Uploaded Image 

                if (PicPath != null)
                {
                    //remove preceed picture
                    if (System.IO.File.Exists(Server.MapPath("~/" + curNews.MasterPicPath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + curNews.MasterPicPath));
                    }
                    if (System.IO.File.Exists(Server.MapPath("~/" + curNews.MasterPicPathThumb)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + curNews.MasterPicPathThumb));
                    }

                    if (PicPath != null)
                    {
                        var filename = Path.GetFileName(PicPath.FileName);
                        var newFilename = Guid.NewGuid().ToString("N") + Path.GetExtension(filename);

                        var newFilenameUrl = "/Uploads/NewsImages/" + newFilename;
                        string physicalFilename = Server.MapPath(newFilenameUrl);
                        PicPath.SaveAs(physicalFilename);
                        var thumbnailUrl = "/Uploads/NewsImages/" + Utils.CreateThumbnail(physicalFilename);
                        curNews.MasterPicPathThumb = thumbnailUrl;
                        curNews.MasterPicPath = newFilenameUrl;

                    }

                }
                #endregion

                try
                {
                    _uow.SaveAllChanges();

                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                    return Json(new { ret = "false", codeid = 2 }, JsonRequestBehavior.AllowGet);
                }
            }
            PopulateNewsGroups();
            return View(news);
        }

        [HttpPost]
        public virtual ActionResult Delete(int id)
        {
            try
            {
                _news.Find(id).IsDeleted = true;
                _uow.SaveAllChanges();
                return Json(new { ret = "true", codeid = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { ret = "false", codeid = 2 }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}