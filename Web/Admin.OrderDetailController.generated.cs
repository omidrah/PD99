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
namespace Web.Areas.Admin.Controllers
{
    public partial class OrderDetailController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected OrderDetailController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Index()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Transfer()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Transfer);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Print()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Print);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Print2()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Print2);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public OrderDetailController Actions { get { return MVC.Admin.OrderDetail; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Admin";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "OrderDetail";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "OrderDetail";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string Transfer = "Transfer";
            public readonly string Print = "Print";
            public readonly string Print2 = "Print2";
            public readonly string GetAllOrderDetialByProductIdNotTransfered = "GetAllOrderDetialByProductIdNotTransfered";
            public readonly string SearchProduct = "SearchProduct";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string Transfer = "Transfer";
            public const string Print = "Print";
            public const string Print2 = "Print2";
            public const string GetAllOrderDetialByProductIdNotTransfered = "GetAllOrderDetialByProductIdNotTransfered";
            public const string SearchProduct = "SearchProduct";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Transfer s_params_Transfer = new ActionParamsClass_Transfer();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Transfer TransferParams { get { return s_params_Transfer; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Transfer
        {
            public readonly string orderDetailID = "orderDetailID";
        }
        static readonly ActionParamsClass_Print s_params_Print = new ActionParamsClass_Print();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Print PrintParams { get { return s_params_Print; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Print
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Print2 s_params_Print2 = new ActionParamsClass_Print2();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Print2 Print2Params { get { return s_params_Print2; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Print2
        {
            public readonly string cols = "cols";
        }
        static readonly ActionParamsClass_GetAllOrderDetialByProductIdNotTransfered s_params_GetAllOrderDetialByProductIdNotTransfered = new ActionParamsClass_GetAllOrderDetialByProductIdNotTransfered();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetAllOrderDetialByProductIdNotTransfered GetAllOrderDetialByProductIdNotTransferedParams { get { return s_params_GetAllOrderDetialByProductIdNotTransfered; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetAllOrderDetialByProductIdNotTransfered
        {
            public readonly string productId = "productId";
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
                public readonly string GetAllOrderDetialByProductIdNotTransfered = "GetAllOrderDetialByProductIdNotTransfered";
                public readonly string Index = "Index";
                public readonly string Print = "Print";
                public readonly string SearchProduct = "SearchProduct";
            }
            public readonly string GetAllOrderDetialByProductIdNotTransfered = "~/Areas/Admin/Views/OrderDetail/GetAllOrderDetialByProductIdNotTransfered.cshtml";
            public readonly string Index = "~/Areas/Admin/Views/OrderDetail/Index.cshtml";
            public readonly string Print = "~/Areas/Admin/Views/OrderDetail/Print.cshtml";
            public readonly string SearchProduct = "~/Areas/Admin/Views/OrderDetail/SearchProduct.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_OrderDetailController : Web.Areas.Admin.Controllers.OrderDetailController
    {
        public T4MVC_OrderDetailController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            IndexOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void TransferOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int orderDetailID);

        [NonAction]
        public override System.Web.Mvc.ActionResult Transfer(int orderDetailID)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Transfer);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "orderDetailID", orderDetailID);
            TransferOverride(callInfo, orderDetailID);
            return callInfo;
        }

        [NonAction]
        partial void PrintOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Print(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Print);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            PrintOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void Print2Override(T4MVC_System_Web_Mvc_ActionResult callInfo, Web.Areas.Admin.Controllers.tmpVm[] cols);

        [NonAction]
        public override System.Web.Mvc.ActionResult Print2(Web.Areas.Admin.Controllers.tmpVm[] cols)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Print2);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "cols", cols);
            Print2Override(callInfo, cols);
            return callInfo;
        }

        [NonAction]
        partial void GetAllOrderDetialByProductIdNotTransferedOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int productId);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetAllOrderDetialByProductIdNotTransfered(int productId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetAllOrderDetialByProductIdNotTransfered);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "productId", productId);
            GetAllOrderDetialByProductIdNotTransferedOverride(callInfo, productId);
            return callInfo;
        }

        [NonAction]
        partial void SearchProductOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult SearchProduct()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SearchProduct);
            SearchProductOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
