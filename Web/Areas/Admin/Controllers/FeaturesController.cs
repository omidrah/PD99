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

namespace Web.Areas.Admin.Controllers
{
    public partial class FeaturesController : Controller
    {

        IUnitOfWork _uow;//db
        IFeaturesService _features;
        IApplicationUserManager _user;
        public FeaturesController(IUnitOfWork uow, IFeaturesService features, IApplicationUserManager user)
        {
            _uow = uow;
            _features = features;
            _user = user;
        }

        // GET: doruses
        public virtual ActionResult Index()
        {
            return View(_features.GetAll().Select(item => new FeaturesVm
            {

                FeatureId = item.FeatureId,
                featuresDispaly = item.featuresDispaly,
                FeaturesName = item.FeaturesName,
                IsActive = item.IsActive,
                IsDeleted = item.IsDeleted,
                CreateDate = item.CreateDate,
                CreatedBy = item.CreatedBy,
                ModifyBy = item.ModifyBy,
                ModifyDate = item.ModifyDate
            }));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Create([DataSourceRequest] DataSourceRequest request, FeaturesVm featuresVM)
        {
            if (ModelState.IsValid && featuresVM != null)
            {
                var newFeature = new Features()
                {
                    IsActive = featuresVM.IsActive == true ? true : false,
                    featuresDispaly = featuresVM.featuresDispaly,
                    FeaturesName = featuresVM.FeaturesName,
                    IsDeleted = false,
                    CreateDate = DateTime.Now,
                    CreatedBy = _user.GetCurrentUserId()
                };
                _features.Add(newFeature);
                _uow.SaveAllChanges();
                featuresVM.FeatureId = newFeature.FeatureId;
            }

            return Json(new[] { featuresVM }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Edit([DataSourceRequest] DataSourceRequest request, FeaturesVm featuresVM)
        {
            if (ModelState.IsValid && featuresVM != null)
            {
                var curFeature = _features.Find(featuresVM.FeatureId);
                curFeature.FeaturesName = featuresVM.FeaturesName;
                curFeature.featuresDispaly = featuresVM.featuresDispaly;
                curFeature.IsActive = featuresVM.IsActive == true ? true : false;
                curFeature.ModifyBy = _user.GetCurrentUserId();
                curFeature.ModifyDate = DateTime.Now;
                _uow.SaveAllChanges();
            }
            return Json(new[] { featuresVM }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Delete([DataSourceRequest] DataSourceRequest request, FeaturesVm featuresVM)
        {
            bool result;

            //if (!db.ProductPropeties.Any(ll => ll.productid == properti.propId))
            //{
            try
            {
                //_dourses.Remove(_dourses.Find(dorusVM.Id));
                var curFeature = _features.Find(featuresVM.FeatureId);
                curFeature.IsDeleted = true;
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