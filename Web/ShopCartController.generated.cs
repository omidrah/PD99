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
    public partial class ShopCartController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ShopCartController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult CommandOrder()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CommandOrder);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Payment()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Payment);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Verify()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Verify);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ShopCartController Actions { get { return MVC.ShopCart; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "ShopCart";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "ShopCart";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string ShowCart = "ShowCart";
            public readonly string Index = "Index";
            public readonly string Order = "Order";
            public readonly string CommandOrder = "CommandOrder";
            public readonly string SelectShiping = "SelectShiping";
            public readonly string Payment = "Payment";
            public readonly string Verify = "Verify";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string ShowCart = "ShowCart";
            public const string Index = "Index";
            public const string Order = "Order";
            public const string CommandOrder = "CommandOrder";
            public const string SelectShiping = "SelectShiping";
            public const string Payment = "Payment";
            public const string Verify = "Verify";
        }


        static readonly ActionParamsClass_CommandOrder s_params_CommandOrder = new ActionParamsClass_CommandOrder();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CommandOrder CommandOrderParams { get { return s_params_CommandOrder; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CommandOrder
        {
            public readonly string id = "id";
            public readonly string itemType = "itemType";
            public readonly string count = "count";
        }
        static readonly ActionParamsClass_Payment s_params_Payment = new ActionParamsClass_Payment();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Payment PaymentParams { get { return s_params_Payment; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Payment
        {
            public readonly string ShippingType = "ShippingType";
        }
        static readonly ActionParamsClass_Verify s_params_Verify = new ActionParamsClass_Verify();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Verify VerifyParams { get { return s_params_Verify; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Verify
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
                public readonly string Index = "Index";
                public readonly string Order = "Order";
                public readonly string Payment = "Payment";
                public readonly string SelectShiping = "SelectShiping";
                public readonly string ShowCart = "ShowCart";
                public readonly string Verify = "Verify";
            }
            public readonly string Index = "~/Views/ShopCart/Index.cshtml";
            public readonly string Order = "~/Views/ShopCart/Order.cshtml";
            public readonly string Payment = "~/Views/ShopCart/Payment.cshtml";
            public readonly string SelectShiping = "~/Views/ShopCart/SelectShiping.cshtml";
            public readonly string ShowCart = "~/Views/ShopCart/ShowCart.cshtml";
            public readonly string Verify = "~/Views/ShopCart/Verify.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ShopCartController : Web.Controllers.ShopCartController
    {
        public T4MVC_ShopCartController() : base(Dummy.Instance) { }

        [NonAction]
        partial void ShowCartOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ShowCart()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ShowCart);
            ShowCartOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void OrderOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Order()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Order);
            OrderOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void CommandOrderOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id, string itemType, int count);

        [NonAction]
        public override System.Web.Mvc.ActionResult CommandOrder(int id, string itemType, int count)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CommandOrder);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "itemType", itemType);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "count", count);
            CommandOrderOverride(callInfo, id, itemType, count);
            return callInfo;
        }

        [NonAction]
        partial void SelectShipingOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult SelectShiping()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SelectShiping);
            SelectShipingOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void PaymentOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string ShippingType);

        [NonAction]
        public override System.Web.Mvc.ActionResult Payment(string ShippingType)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Payment);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "ShippingType", ShippingType);
            PaymentOverride(callInfo, ShippingType);
            return callInfo;
        }

        [NonAction]
        partial void VerifyOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Verify(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Verify);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            VerifyOverride(callInfo, id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
