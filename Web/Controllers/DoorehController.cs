using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    [OverrideAuthorization]
    public partial class DoorehController : Controller
    {
        // GET: Dooreh
        private readonly IDoorehService _dooreh;
        public DoorehController(IDoorehService dooreh)
        {
            _dooreh = dooreh;
        }
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult LastOnlineDooreh()
        {
            return PartialView(_dooreh.GetAll().Where(k=>k.IsOnline ==true).OrderByDescending(s => s.CreateDate).Take(3));
        }
        public virtual ActionResult LastHozoriDooreh()
        {
            return PartialView(_dooreh.GetAll().Where(k=>k.IsHozori ==true).OrderByDescending(s => s.CreateDate).Take(3));
        }
    }
}