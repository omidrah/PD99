using DataLayer.Context;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceLayer.Models;
using Kendo.Mvc.UI;
using DomainClass.Models;
using Kendo.Mvc.Extensions;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{
    public partial class SettingsController : Controller
    {

        IUnitOfWork _uow;//db
        ISettingsService _settings;
        IApplicationUserManager _user;
        public SettingsController(IUnitOfWork uow, ISettingsService settings, IApplicationUserManager user)
        {
            _uow = uow;
            _settings = settings;
            _user = user;
        }

        // GET: doruses
        public virtual ActionResult Index()
        {
            return View(_settings.GetAll().Select(item => new SettingsVM
            {

                Id = item.Id,
                PostBasePrice = item.PostBasePrice,
                PostPricePerUnit = item.PostPricePerUnit,
                IsDeleted = item.IsDeleted,
                CreateDate = item.CreateDate,
                CreatedBy = item.CreatedBy,
                ModifiedBy = item.ModifiedBy,
                ModifiedDate = item.ModifiedDate
            }));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Create([DataSourceRequest] DataSourceRequest request, SettingsVM settingsVM)
        {
            if (ModelState.IsValid && settingsVM != null)
            {
                var newSetting = new Settings()
                {

                    PostBasePrice = settingsVM.PostBasePrice,
                    PostPricePerUnit = settingsVM.PostPricePerUnit,
                    IsDeleted = false,
                    CreateDate = DateTime.Now,
                    CreatedBy = _user.GetCurrentUserId()
                };
                _settings.Add(newSetting);
                _uow.SaveAllChanges();
                settingsVM.Id = newSetting.Id;
            }

            return Json(new[] { settingsVM }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Edit([DataSourceRequest] DataSourceRequest request, SettingsVM settingsVM)
        {
            if (ModelState.IsValid && settingsVM != null)
            {
                var curSetting = _settings.Find(settingsVM.Id);
                curSetting.PostPricePerUnit = settingsVM.PostPricePerUnit;
                curSetting.PostBasePrice = settingsVM.PostBasePrice;
                curSetting.ModifiedBy = _user.GetCurrentUserId();
                curSetting.ModifiedDate = DateTime.Now;
                _uow.SaveAllChanges();
            }
            return Json(new[] { settingsVM }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Delete([DataSourceRequest] DataSourceRequest request, SettingsVM settingsVM)
        {
            bool result;

            //if (!db.ProductPropeties.Any(ll => ll.productid == properti.propId))
            //{
            try
            {
                //_dourses.Remove(_dourses.Find(dorusVM.Id));
                var curSettings = _settings.Find(settingsVM.Id);
                curSettings.IsDeleted = true;
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