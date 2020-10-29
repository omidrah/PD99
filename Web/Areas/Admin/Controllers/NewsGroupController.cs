using DataLayer.Context;
using DomainClass.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Authorize]
    public partial class NewsGroupController : Controller
    {
        IUnitOfWork _uow;//db
        INewsService _news;
        IPersonelService _personel;
        IApplicationUserManager _user;
        public NewsGroupController(IUnitOfWork uow, INewsService news, IPersonelService personel,
         IApplicationUserManager user)
        {
            _uow = uow;
            _user = user;
            _personel = personel;
            _news = news;

        }
        public virtual ActionResult Index()
        {
            PopulateDDl();
            var NewsGroups = _news.GetAll().Where(ll => ll.ParentId == null && ll.IsDeleted == false).Select(ll => new
            {
                NewsTitle = ll.NewsTitle,
                NewsId = ll.NewsId,
                MasterPicPath = ll.MasterPicPath,
                GroupmanagerId = ll.GroupmanagerId
            });
            var NewsGroupsModir = (IEnumerable<UserddlViewModel>)ViewData["Usersddl"];
            var JoisAboveCol = NewsGroups.Join(NewsGroupsModir, inp => inp.GroupmanagerId, outt => outt.UserId,
                    (a, b) => new NewsGroupVIewModel
                    {
                        NewsTitle = a.NewsTitle,
                        NewsId = a.NewsId,
                        GroupmanagerId = b.UserId
                    });
            return View(JoisAboveCol);
        }

        private void PopulateDDl()
        {
            ViewData["Usersddl"] = _personel.GetAll().//.Where(ll => ll.IsDeleted == false && (ll.Role.RoleName == "Admin" || ll.Role.RoleName == "AdminNews")).
                Select(aa => new UserddlViewModel
                {
                    UserId = aa.UserId,
                    UserName = aa.Users.Name + " " + aa.Users.Family
                }).OrderBy(aa => aa.UserName);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Create([DataSourceRequest] DataSourceRequest request, NewsGroupVIewModel NewsGR)
        {
            if (ModelState.IsValid && NewsGR != null)
            {
                int curUsrId; int.TryParse(User.Identity.Name, out curUsrId);
                var newrect = new News
                {
                    IsDeleted = false,
                    CreatedBy = curUsrId,
                    CreatedDate = DateTime.Now,
                    IsAuthenticated = true,
                    ParentId = null,
                    NewsTitle = NewsGR.NewsTitle,
                    NewsBody = "NewsGroups",
                    MasterPicPath = NewsGR.MasterPicPath,
                    GroupmanagerId = NewsGR.GroupmanagerId
                };

                _news.Add(newrect);
                _uow.SaveAllChanges();
                NewsGR.NewsId = newrect.NewsId;
            }
            return Json(new[] { NewsGR }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Edit([DataSourceRequest] DataSourceRequest request, NewsGroupVIewModel NewsGrR)
        {
            if (ModelState.IsValid && NewsGrR != null)
            {
                int curUsrId; int.TryParse(User.Identity.Name, out curUsrId);
                var curREC = _news.Find(NewsGrR.NewsId);
                curREC.NewsTitle = NewsGrR.NewsTitle;
                curREC.ParentId = null;
                curREC.ModifiedBy = curUsrId;
                curREC.ModifiedDate = DateTime.Now;
                curREC.MasterPicPath = NewsGrR.MasterPicPath;
                curREC.GroupmanagerId = NewsGrR.GroupmanagerId;
                _uow.SaveAllChanges();

            }
            return Json(new[] { NewsGrR }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Delete([DataSourceRequest] DataSourceRequest request, NewsGroupVIewModel NewsGR)
        {
            bool result;
            if (!_news.GetAll().Any(ll => ll.ParentId == NewsGR.NewsId)) //groups has child
            {
                result = true;
                _news.Find(NewsGR.NewsId).IsDeleted = true;
                _uow.SaveAllChanges();
            }
            else
            {
                result = false;
            }
            return Json(new[] { result }.ToDataSourceResult(request, ModelState));
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