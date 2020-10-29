using DataLayer.Context;
using Kendo.Mvc.UI;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using DomainClass.Models;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using System.Diagnostics;
using Kendo.Mvc.Extensions;
using System.Web;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace Web.Areas.Admin.Controllers
{
    public partial class CustomersController : Controller
    {
        IUnitOfWork _uow;//db
        ICustomerService _customers;
        ICustomerTypeServic _customerType;
        IApplicationUserManager _userManager;
       // IUser _user;
        IOrderService _orders;

        public CustomersController(IUnitOfWork uow, ICustomerService customers, IApplicationUserManager user, ICustomerTypeServic customerType
            , IOrderService order)
        {
            _uow = uow;
            _customers = customers;
            _customerType = customerType;
            _userManager = user;
          //  _user = user1;
            _orders = order;

        }
        public virtual ActionResult Index()
        {
            PopulateCustomerType();
            var d = _customers.GetAll();

            //foreach (var item in d)
            //{
            //    item.FullName = item.Name + " " + item.Family;
            //    item.CreateDate = _userManager.FindById(item.UserId).CreatedDate;
            //}
            return View(d);
        }

        public virtual ActionResult ConfirmDocument()
        {
            PopulateCustomerType();
            var d = _customers.GetAll();

            foreach (var item in d)
            {
                item.FullName = item.Name + " " + item.Family;
                item.CreateDate = _userManager.FindById(item.UserId).CreatedDate;
            }
            return View(d.Where(ll=>ll.IsMadrakChecked == false));
        }
        //[AcceptVerbs(HttpVerbs.Get)]
        //public ActionResult Customer_Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    return Json(_customers.GetAll().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual async Task<ActionResult> Create([DataSourceRequest] DataSourceRequest request, CustomerVm customer)
        {

            var user = new User();
            if (ModelState.IsValid && customer != null)
            {
                int curUserid;
                int.TryParse(User.Identity.Name, out curUserid);
                user = new User
                {
                    UserName = customer.Email,
                    Email = customer.Email,
                    Name = customer.Name,
                    Family = customer.Family,
                    NationCode = customer.nationCode,
                    Address = customer.Address,
                    PostalCode = customer.PostalCode,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    PhoneNumber = customer.Mobile,
                    Mobile = customer.Mobile,
                    Tel = customer.Tel
                };
                var ParvandPrefix = "A";
                if (customer.customerTypeId == 0)//اگر کاربر ست نکرده بود
                {
                    customer.customerTypeId = 1;//ازاد
                    customer.customerTypeTitle = "آزاد";
                }
                switch (customer.customerTypeTitle)
                {
                    case "آزاد":
                        ParvandPrefix = "A";
                        break;
                    case "بسیج":
                        ParvandPrefix = "B";
                        break;
                    case "شاهد":
                        ParvandPrefix = "SH";
                        break;

                    case "دانشجو":
                        ParvandPrefix = "S";
                        break;
                }
                user.Customers = new Customer
                {
                    Users = user,
                    CustomerTypeId = customer.customerTypeId,
                    PanvandeNo = ParvandPrefix + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString(),
                };
                user.IsDeleted = false;
                var result = await _userManager.CreateAsync(user, customer.Password);
                if (result.Succeeded)
                {
                    List<HttpPostedFileBase> files = HttpContext.Session["picPath"] as List<HttpPostedFileBase>;
                    if (files != null)
                    {
                        HttpPostedFileBase newFile = files[0];
                        var filename = Path.GetFileName(newFile.FileName);
                        var newFilename = Guid.NewGuid().ToString("N") + Path.GetExtension(filename);
                        var newFilenameUrl = "~/Uploads/ImgCustomer/" + newFilename;
                        var physicalFilename = Server.MapPath(newFilenameUrl);
                        newFile.SaveAs(physicalFilename);
                        var thumbnailUrl = Utils.CreateThumbnail(physicalFilename);
                        user.ImgPath = newFilenameUrl;

                        //save Doc images بسیج ، آزاد، شاهد ، ایثارگر
                        List<HttpPostedFileBase> Docfiles = HttpContext.Session["picDoc"] as List<HttpPostedFileBase>;
                        if (Docfiles != null)
                        {
                            newFile = Docfiles[0];
                            //باهمان اسمی که عکس  کاربر را ذخیره کردیم ،  مدارک  وی  را هم ذخیره می کنیم
                            newFilenameUrl = "~/Uploads/MadarekCustomer/" + newFilename;
                            physicalFilename = Server.MapPath(newFilenameUrl);
                            newFile.SaveAs(physicalFilename);
                        }
                    }
                    // Add user to Role Customer if not already added
                    var rolesForUser = await _userManager.GetRolesAsync(user.Id);
                    if (!rolesForUser.Contains("Customer"))
                    {
                        var addToRoleResult = await _userManager.AddToRoleAsync(user.Id, "Customer");
                        if (!addToRoleResult.Succeeded)
                        {
                            AddErrors(addToRoleResult);
                        }
                    }

                }
            }
            return Json(new[] { customer }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual async Task<ActionResult> Edit([DataSourceRequest] DataSourceRequest request, CustomerVm customer)
        {
            if (customer != null)
            {
                var curUser = await _userManager.FindByIdAsync(customer.UserId);
                customer.customerTypeTitle = _customerType.GetAll().Where(ll => ll.Id == customer.customerTypeId).Single().Title;
                customer.Password = "123456";
                if (curUser != null)
                {
                    curUser.Name = customer.Name;
                    curUser.Family = customer.Family;
                    curUser.NationCode = customer.nationCode;
                    curUser.Mobile = curUser.PhoneNumber = customer.Mobile;
                    curUser.Address = customer.Address;
                    curUser.PostalCode = customer.PostalCode;
                    curUser.Email = customer.Email;
                    curUser.Tel = customer.Tel;
                    curUser.ModifiedDate = DateTime.Now;
                    curUser.ModifiedBy = _userManager.GetCurrentUserId();
                    curUser.Customers.CustomerTypeId = customer.customerTypeId;
                    curUser.Customers.IsMadrakTaeed = customer.IsMadrakTaeed;

                    List<HttpPostedFileBase> files = HttpContext.Session["picPath"] as List<HttpPostedFileBase>;
                    if (files != null)
                    {
                        //remove pre pic 
                        //عکس قبلی را پاک کنیم
                        HttpPostedFileBase newFile = files[0];
                        var filename = Path.GetFileName(newFile.FileName);
                        var newFilename = Guid.NewGuid().ToString("N") + Path.GetExtension(filename);
                        var newFilenameUrl = "~/Uploads/ImgCustomer/" + newFilename;
                        var physicalFilename = Server.MapPath(newFilenameUrl);
                        newFile.SaveAs(physicalFilename);
                        var thumbnailUrl = Utils.CreateThumbnail(physicalFilename);
                        curUser.ImgPath = newFilenameUrl;

                        //save Doc images بسیج ، آزاد، شاهد ، ایثارگر
                        List<HttpPostedFileBase> Docfiles = HttpContext.Session["picDoc"] as List<HttpPostedFileBase>;
                        if (Docfiles != null)
                        {
                            newFile = Docfiles[0];
                            //باهمان اسمی که عکس  کاربر را ذخیره کردیم ،  مدارک  وی  را هم ذخیره می کنیم
                            newFilenameUrl = "~/Uploads/MadarekCustomer/" + newFilename;
                            physicalFilename = Server.MapPath(newFilenameUrl);
                            newFile.SaveAs(physicalFilename);
                        }
                    }
                    var d = await _userManager.UpdateAsync(curUser);
                    if (!d.Succeeded)
                    {
                        AddErrors(d);
                    }
                }
            }

            return Json(new[] { customer }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual async Task<ActionResult> EditConfirmCode([DataSourceRequest] DataSourceRequest request, CustomerVm customer)
        {
            if (customer != null)
            {
                var curUser = await _userManager.FindByIdAsync(customer.UserId);
                customer.customerTypeTitle = _customerType.GetAll().Where(ll => ll.Id == customer.customerTypeId).Single().Title;
                customer.Password = "123456";
                if (curUser != null)
                {
                    curUser.Name = customer.Name;
                    curUser.Family = customer.Family;
                    curUser.NationCode = customer.nationCode;
                    curUser.Mobile = curUser.PhoneNumber = customer.Mobile;
                    curUser.Address = customer.Address;
                    curUser.PostalCode = customer.PostalCode;
                    curUser.Email = customer.Email;
                    curUser.Tel = customer.Tel;
                    curUser.ModifiedDate = DateTime.Now;
                    curUser.ModifiedBy = _userManager.GetCurrentUserId();
                    curUser.Customers.CustomerTypeId = customer.customerTypeId;
                    curUser.Customers.IsMadrakChecked = true;
                    curUser.Customers.IsMadrakTaeed = customer.IsMadrakTaeed;

                    List<HttpPostedFileBase> files = HttpContext.Session["picPath"] as List<HttpPostedFileBase>;
                    if (files != null)
                    {
                        //remove pre pic 
                        //عکس قبلی را پاک کنیم
                        HttpPostedFileBase newFile = files[0];
                        var filename = Path.GetFileName(newFile.FileName);
                        var newFilename = Guid.NewGuid().ToString("N") + Path.GetExtension(filename);
                        var newFilenameUrl = "~/Uploads/ImgCustomer/" + newFilename;
                        var physicalFilename = Server.MapPath(newFilenameUrl);
                        newFile.SaveAs(physicalFilename);
                        var thumbnailUrl = Utils.CreateThumbnail(physicalFilename);
                        curUser.ImgPath = newFilenameUrl;

                        //save Doc images بسیج ، آزاد، شاهد ، ایثارگر
                        List<HttpPostedFileBase> Docfiles = HttpContext.Session["picDoc"] as List<HttpPostedFileBase>;
                        if (Docfiles != null)
                        {
                            newFile = Docfiles[0];
                            //باهمان اسمی که عکس  کاربر را ذخیره کردیم ،  مدارک  وی  را هم ذخیره می کنیم
                            newFilenameUrl = "~/Uploads/MadarekCustomer/" + newFilename;
                            physicalFilename = Server.MapPath(newFilenameUrl);
                            newFile.SaveAs(physicalFilename);
                        }
                    }
                    var d = await _userManager.UpdateAsync(curUser);
                    if (!d.Succeeded)
                    {
                        AddErrors(d);
                    }
                }
            }

            return Json(new[] { customer }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual async Task<ActionResult> Destroy([DataSourceRequest] DataSourceRequest request, CustomerVm customer)
        {
            if (customer != null)
            {
                var curUser = await _userManager.FindByIdAsync(customer.UserId);
                if (curUser != null && !_orders.GetAll().Any( ll=>ll.UserID ==curUser.Id))
                {
                    curUser.IsDeleted = true;
                    var d = await _userManager.UpdateAsync(curUser);
                    if (!d.Succeeded)
                    {
                        AddErrors(d);
                    }
                }
                _uow.SaveAllChanges();
            }

            return Json(new[] { customer }.ToDataSourceResult(request, ModelState));
        }
        private void PopulateCustomerType()
        {

            var res = _customerType.GetAll()
                        .Select(c => new CustomerTypeVm
                        {
                            Id = c.Id,
                            Title = c.Title
                        })
                        .OrderBy(e => e.Title);
            ViewData["CustomerTypes"] = res;
            ViewData["defaultCustomerTypes"] = res.First();

        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public virtual JsonResult SavePicPathFile()
        {
            string filename = String.Empty;
            const string sessionKey = "picPath";
            if (HttpContext.Request.Files != null && HttpContext.Request.Files.Count > 0 && HttpContext.Session != null)
            {
                List<HttpPostedFileBase> files = HttpContext.Session[sessionKey] as List<HttpPostedFileBase>;
                foreach (string fileName in HttpContext.Request.Files)
                {
                    HttpPostedFileBase newFile = HttpContext.Request.Files[fileName];
                    if (files == null)
                    {
                        files = new List<HttpPostedFileBase> { newFile };
                    }
                    else
                    {
                        files.Add(newFile);
                    }
                    HttpContext.Session[sessionKey] = files;
                    if (newFile != null)
                        filename = Path.GetFileName(newFile.FileName);
                }
            }
            return Json(new { Type = "Upload", FileName = filename }, JsonRequestBehavior.AllowGet);
        }
        public virtual JsonResult DeletePicPathFile(string fileName)
        {
            const string sessionKey = "picPath";
            List<HttpPostedFile> files = HttpContext.Session?[sessionKey] as List<HttpPostedFile>;
            if (files != null && files.Count > 0)
            {
                //Don't rely on browser always doing the correct thing 
                files = files.Where(f => Path.GetFileName(f.FileName) != fileName).ToList();
                HttpContext.Session[sessionKey] = files;
            }
            return Json(new { Type = "Upload", FileName = fileName }, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult SavePicDocFile()
        {
            string filename = String.Empty;
            const string sessionKey = "picDoc";
            if (HttpContext.Request.Files != null && HttpContext.Request.Files.Count > 0 && HttpContext.Session != null)
            {
                List<HttpPostedFileBase> files = HttpContext.Session[sessionKey] as List<HttpPostedFileBase>;
                foreach (string fileName in HttpContext.Request.Files)
                {
                    HttpPostedFileBase newFile = HttpContext.Request.Files[fileName];
                    if (files == null)
                    {
                        files = new List<HttpPostedFileBase> { newFile };
                    }
                    else
                    {
                        files.Add(newFile);
                    }
                    HttpContext.Session[sessionKey] = files;
                    if (newFile != null)
                        filename = Path.GetFileName(newFile.FileName);
                }
            }
            return Json(new { Type = "Upload", FileName = filename }, JsonRequestBehavior.AllowGet);
        }
        public virtual JsonResult DeletePicDocFile(string fileName)
        {
            const string sessionKey = "picDoc";
            List<HttpPostedFile> files = HttpContext.Session?[sessionKey] as List<HttpPostedFile>;
            if (files != null && files.Count > 0)
            {
                //Don't rely on browser always doing the correct thing 
                files = files.Where(f => Path.GetFileName(f.FileName) != fileName).ToList();
                HttpContext.Session[sessionKey] = files;
            }
            return Json(new { Type = "Upload", FileName = fileName }, JsonRequestBehavior.AllowGet);
        }
    }
}