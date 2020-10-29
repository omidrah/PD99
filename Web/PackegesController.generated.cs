// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Web.Controllers
{
    public partial class PackegesController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected PackegesController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult PackageContents()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PackageContents);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult DoorehContents()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DoorehContents);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult PackageDetails()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PackageDetails);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public PackegesController Actions { get { return MVC.Packeges; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Packeges";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Packeges";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string ShowPackages = "ShowPackages";
            public readonly string LastPackages = "LastPackages";
            public readonly string ArchivePackage = "ArchivePackage";
            public readonly string ArchiveOnline = "ArchiveOnline";
            public readonly string ArchiveHozori = "ArchiveHozori";
            public readonly string PackageContents = "PackageContents";
            public readonly string DoorehContents = "DoorehContents";
            public readonly string PackageDetails = "PackageDetails";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string ShowPackages = "ShowPackages";
            public const string LastPackages = "LastPackages";
            public const string ArchivePackage = "ArchivePackage";
            public const string ArchiveOnline = "ArchiveOnline";
            public const string ArchiveHozori = "ArchiveHozori";
            public const string PackageContents = "PackageContents";
            public const string DoorehContents = "DoorehContents";
            public const string PackageDetails = "PackageDetails";
        }


        static readonly ActionParamsClass_ArchivePackage s_params_ArchivePackage = new ActionParamsClass_ArchivePackage();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ArchivePackage ArchivePackageParams { get { return s_params_ArchivePackage; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ArchivePackage
        {
            public readonly string pageId = "pageId";
        }
        static readonly ActionParamsClass_ArchiveOnline s_params_ArchiveOnline = new ActionParamsClass_ArchiveOnline();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ArchiveOnline ArchiveOnlineParams { get { return s_params_ArchiveOnline; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ArchiveOnline
        {
            public readonly string pageId = "pageId";
        }
        static readonly ActionParamsClass_ArchiveHozori s_params_ArchiveHozori = new ActionParamsClass_ArchiveHozori();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ArchiveHozori ArchiveHozoriParams { get { return s_params_ArchiveHozori; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ArchiveHozori
        {
            public readonly string pageId = "pageId";
        }
        static readonly ActionParamsClass_PackageContents s_params_PackageContents = new ActionParamsClass_PackageContents();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_PackageContents PackageContentsParams { get { return s_params_PackageContents; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_PackageContents
        {
            public readonly string packageId = "packageId";
        }
        static readonly ActionParamsClass_DoorehContents s_params_DoorehContents = new ActionParamsClass_DoorehContents();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DoorehContents DoorehContentsParams { get { return s_params_DoorehContents; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DoorehContents
        {
            public readonly string doorehId = "doorehId";
        }
        static readonly ActionParamsClass_PackageDetails s_params_PackageDetails = new ActionParamsClass_PackageDetails();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_PackageDetails PackageDetailsParams { get { return s_params_PackageDetails; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_PackageDetails
        {
            public readonly string id = "id";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string ArchiveHozori = "ArchiveHozori";
                public readonly string ArchiveOnline = "ArchiveOnline";
                public readonly string ArchivePackage = "ArchivePackage";
                public readonly string DoorehContents = "DoorehContents";
                public readonly string LastPackages = "LastPackages";
                public readonly string PackageContents = "PackageContents";
                public readonly string PackageDetails = "PackageDetails";
                public readonly string ShowPackages = "ShowPackages";
            }
            public readonly string ArchiveHozori = "~/Views/Packeges/ArchiveHozori.cshtml";
            public readonly string ArchiveOnline = "~/Views/Packeges/ArchiveOnline.cshtml";
            public readonly string ArchivePackage = "~/Views/Packeges/ArchivePackage.cshtml";
            public readonly string DoorehContents = "~/Views/Packeges/DoorehContents.cshtml";
            public readonly string LastPackages = "~/Views/Packeges/LastPackages.cshtml";
            public readonly string PackageContents = "~/Views/Packeges/PackageContents.cshtml";
            public readonly string PackageDetails = "~/Views/Packeges/PackageDetails.cshtml";
            public readonly string ShowPackages = "~/Views/Packeges/ShowPackages.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_PackegesController : Web.Controllers.PackegesController
    {
        public T4MVC_PackegesController() : base(Dummy.Instance) { }

        [NonAction]
        partial void ShowPackagesOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ShowPackages()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ShowPackages);
            ShowPackagesOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void LastPackagesOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult LastPackages()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.LastPackages);
            LastPackagesOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ArchivePackageOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int pageId);

        [NonAction]
        public override System.Web.Mvc.ActionResult ArchivePackage(int pageId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ArchivePackage);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "pageId", pageId);
            ArchivePackageOverride(callInfo, pageId);
            return callInfo;
        }

        [NonAction]
        partial void ArchiveOnlineOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int pageId);

        [NonAction]
        public override System.Web.Mvc.ActionResult ArchiveOnline(int pageId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ArchiveOnline);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "pageId", pageId);
            ArchiveOnlineOverride(callInfo, pageId);
            return callInfo;
        }

        [NonAction]
        partial void ArchiveHozoriOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int pageId);

        [NonAction]
        public override System.Web.Mvc.ActionResult ArchiveHozori(int pageId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ArchiveHozori);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "pageId", pageId);
            ArchiveHozoriOverride(callInfo, pageId);
            return callInfo;
        }

        [NonAction]
        partial void PackageContentsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int packageId);

        [NonAction]
        public override System.Web.Mvc.ActionResult PackageContents(int packageId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PackageContents);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "packageId", packageId);
            PackageContentsOverride(callInfo, packageId);
            return callInfo;
        }

        [NonAction]
        partial void DoorehContentsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int doorehId);

        [NonAction]
        public override System.Web.Mvc.ActionResult DoorehContents(int doorehId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DoorehContents);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "doorehId", doorehId);
            DoorehContentsOverride(callInfo, doorehId);
            return callInfo;
        }

        [NonAction]
        partial void PackageDetailsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult PackageDetails(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PackageDetails);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            PackageDetailsOverride(callInfo, id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114