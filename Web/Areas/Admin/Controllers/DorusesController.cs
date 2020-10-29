
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


namespace Web.Areas.Admin.Controllers
{
    // [Authorize]
    public partial class DorusesController : Controller
    {
        IUnitOfWork _uow;//db
        IDourseService _dourses;
        IApplicationUserManager _user;
        public DorusesController(IUnitOfWork uow, IDourseService dourse, IApplicationUserManager user)
        {
            _uow = uow;
            _dourses = dourse;
            _user = user;
        }

        // GET: doruses
        public virtual ActionResult Index()
        {
            return View(_dourses.GetAll().Select(item => new DorusVM
            {

                ModatByHours = item.ModatByHours,
                Title = item.Title,
                Id = item.Id,
                IsActive = item.IsActive,
                IsDeleted = item.IsDeleted,
                CreateDate = item.CreateDate,
                CreatedBy = item.CreatedBy,
                ModifyBy = item.ModifyBy,
                ModifyDate = item.ModifyDate
            }));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Create([DataSourceRequest] DataSourceRequest request, DorusVM dorusVM)
        {
            if (_dourses.GetAll().Where(d => d.Title == dorusVM.Title).Any())
            {
                ModelState.AddModelError("Title", "نام درس تکراری است");
            }
            else
            {
                if (ModelState.IsValid && dorusVM != null)
                {

                    var newDorus = new Doruses()
                    {
                        IsActive = dorusVM.IsActive == true ? true : false,
                        Title = dorusVM.Title,
                        ModatByHours = dorusVM.ModatByHours,
                        IsDeleted = false,
                        CreateDate = DateTime.Now,
                        CreatedBy = _user.GetCurrentUserId()
                    };
                    _dourses.Add(newDorus);
                    _uow.SaveAllChanges();
                    dorusVM.Id = newDorus.Id;
                }

            }

            return Json(new[] { dorusVM }.ToDataSourceResult(request, ModelState));
        }

        public virtual ActionResult CheckDorousTitleExists(string Title)

        {

            bool TitleExist = false;
            try
            {
                var TitleExits = from temprec in _dourses.GetAll()

                                 where temprec.Title.Equals(Title.Trim())

                                 select temprec;

                if (TitleExits.Count() > 0)
                {
                    TitleExist = true;
                }
                else
                {
                    TitleExist = false;
                }
                return Json(!TitleExist, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Edit([DataSourceRequest] DataSourceRequest request, DorusVM dorusVM)
        {
            if (ModelState.IsValid && dorusVM != null)
            {
                var curDorus = _dourses.Find(dorusVM.Id);
                curDorus.Title = dorusVM.Title;
                curDorus.IsActive = dorusVM.IsActive == true ? true : false;
                curDorus.ModatByHours = dorusVM.ModatByHours;
                curDorus.ModifyBy = _user.GetCurrentUserId();
                curDorus.ModifyDate = DateTime.Now;
                _uow.SaveAllChanges();
            }
            return Json(new[] { dorusVM }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Delete([DataSourceRequest] DataSourceRequest request, DorusVM dorusVM)
        {
            bool result;

            //if (!db.ProductPropeties.Any(ll => ll.productid == properti.propId))
            //{
            try
            {
                //_dourses.Remove(_dourses.Find(dorusVM.Id));
                var curDorus = _dourses.Find(dorusVM.Id);
                curDorus.IsDeleted = true;
                _uow.SaveAllChanges();
                result = true;
            }
            catch
            {

                result = false;
            }

            //}
            //else
            //{
            //    result = false;
            //}
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

