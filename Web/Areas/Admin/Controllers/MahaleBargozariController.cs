
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
    public partial class MahaleBargozariController : Controller
    {
        IUnitOfWork _uow;//db
        IMahaleBargozariService _mahaleBargozari;
        IApplicationUserManager _user;
        public MahaleBargozariController(IUnitOfWork uow, IMahaleBargozariService mahaleBargozari, IApplicationUserManager user)
        {
            _uow = uow;
            _mahaleBargozari = mahaleBargozari;
            _user = user;
        }

        // GET: doruses
        public virtual ActionResult Index()
        {
            return View(_mahaleBargozari.GetAll().Select(item => new MahaleBargozariVM
            {

                 MahalTitle = item.MahalTitle,
                    
                Id = item.Id,
                IsActive = item.IsActive,
                IsDeleted = item.IsDeleted,
                CreateDate = item.CreateDate,
                
            }));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Create([DataSourceRequest] DataSourceRequest request, MahaleBargozariVM mahaleBargozariVM)
        {
            if (ModelState.IsValid && mahaleBargozariVM != null)
            {
                var newItem = new MahaleBargozari()
                {
                    IsActive = mahaleBargozariVM.IsActive == true ? true : false,
                    MahalTitle = mahaleBargozariVM.MahalTitle,
                    IsDeleted = false,
                    CreateDate = DateTime.Now,
                    CreateBy = _user.GetCurrentUserId()
                };
                _mahaleBargozari.Add(newItem);
                _uow.SaveAllChanges();
                mahaleBargozariVM.Id = newItem.Id;
            }

            return Json(new[] { mahaleBargozariVM }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Edit([DataSourceRequest] DataSourceRequest request, MahaleBargozariVM mahaleBargozariVM)
        {
            if (ModelState.IsValid && mahaleBargozariVM != null)
            {
                var curDorus = _mahaleBargozari.Find(mahaleBargozariVM.Id);
                curDorus.MahalTitle = mahaleBargozariVM.MahalTitle;
                curDorus.IsActive = mahaleBargozariVM.IsActive == true ? true : false;
                curDorus.ModifiedBy = _user.GetCurrentUserId();
                curDorus.ModifiedDate = DateTime.Now;
                _uow.SaveAllChanges();
            }
            return Json(new[] { mahaleBargozariVM }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Delete([DataSourceRequest] DataSourceRequest request,MahaleBargozariVM mahaleBargozariVM)
        {
            bool result;

            //if (!db.ProductPropeties.Any(ll => ll.productid == properti.propId))
            //{
            try
            {
                //_dourses.Remove(_dourses.Find(dorusVM.Id));
                var curItem = _mahaleBargozari.Find(mahaleBargozariVM.Id);
                curItem.IsDeleted = true;
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

