using DataLayer.Context;
using DomainClass.Models;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [AllowAnonymous]
    public partial class ShopCartController4 : Controller
    {
        IUnitOfWork _uow;//db
        IProductService _products;
        IApplicationUserManager _user;
        ICustomerService _customer;
        IProductGroupService _productGroup;
        IImageService _Images;
        IPackageService _packages;
        IPackageProductService _packageProduct;
        ITagService _tags;
        IProductFeaturesService _productFeatures;
        IProductGroupFeaturesService _productGroupFeatures;
        IPaymentUniqueNumberService _payment;
        IFeaturesService _features;
        IOrderService _orders;
        IOrderStatusService _orderStatus;
        IOrderDetailsService _orderDetails;
        IDoorehService _dooreh;

        public ShopCartController4(IUnitOfWork uow, IProductService product, IProductGroupService productGroup, IPaymentUniqueNumberService payment
            , IImageService images, IApplicationUserManager user, IPackageProductService packageProducts, IPackageService packages
            , ITagService tags, IProductFeaturesService productFeatures, IProductGroupFeaturesService productGroupFeatures
            , IFeaturesService features, IOrderService orders, IOrderStatusService orderStatus, IOrderDetailsService orderDetails
            , IDoorehService dooreh, ICustomerService customer)
        {
            _uow = uow;
            _products = product;
            _user = user;
            _productGroup = productGroup;
            _Images = images;
            _packageProduct = packageProducts;
            _packages = packages;
            _tags = tags;
            _features = features;
            _productFeatures = productFeatures;
            _productGroupFeatures = productGroupFeatures;
            _orders = orders;
            _orderDetails = orderDetails;
            _orderStatus = orderStatus;
            _dooreh = dooreh;
            _payment = payment;
            _customer = customer;
        }
        // GET: ShopCart
        [OverrideAuthorization]
        public virtual ActionResult ShowCart()
        {
            List<ShopCartItemViewModel> list = new List<ShopCartItemViewModel>();

            if (Session["ShopCart"] != null)
            {
                List<ShopCartItem> listShop = (List<ShopCartItem>)Session["ShopCart"];

                foreach (var item in listShop)
                {
                    if (item.ItemType == "Product")
                    {
                        var product = _products.Where(p => p.ProductId == item.ItemId).Select(p => new
                        {
                            p.PicPath,
                            p.ProductName,
                            p.ProductGroup.ProductGroupTitle
                        }).Single();
                        list.Add(new ShopCartItemViewModel()
                        {
                            Count = item.Count,
                            ItemId = item.ItemId,
                            Title = product.ProductGroupTitle + " - " + product.ProductName,
                            ImageName = product.PicPath

                        });
                    }

                    if (item.ItemType == "Package")
                    {
                        var package = _packages.GetAll().Where(p => p.Id == item.ItemId).Select(p => new
                        {
                            p.ImageName,
                            p.PackageTitle
                        }).Single();
                        list.Add(new ShopCartItemViewModel()
                        {
                            Count = item.Count,
                            ItemId = item.ItemId,
                            Title = "بسته " + package.PackageTitle,
                            ImageName = package.ImageName

                        });
                    }

                    if (item.ItemType == "Dooreh")
                    {
                        var dooreh = _dooreh.GetAll().Where(p => p.Id == item.ItemId).Select(p => new
                        {
                            p.ImageName,
                            p.Title
                        }).Single();
                        list.Add(new ShopCartItemViewModel()
                        {
                            Count = item.Count,
                            ItemId = item.ItemId,
                            Title = "دوره " + dooreh.Title,
                            ImageName = dooreh.ImageName

                        });
                    }
                }
            }
            return PartialView(list);
        }

        [OverrideAuthorization]
        public virtual ActionResult Index()
        {
            return View();
        }

        List<ShowOrderViewModel> getListOrder()
        {
            bool hasOff = false;
            if (User.Identity.IsAuthenticated)
            {
                var userId = _user.GetCurrentUserId();
                Customer customer = _customer.Find(userId);
                if (customer.CustomerTypeId == 4)
                {
                    hasOff = true;
                }

            }

            List<ShowOrderViewModel> list = new List<ShowOrderViewModel>();

            if (hasOff == true)
            {
                if (Session["ShopCart"] != null)
                {
                    List<ShopCartItem> listShop = Session["ShopCart"] as List<ShopCartItem>;

                    foreach (var item in listShop)
                    {
                        if (item.ItemType == "Product")
                        {
                            var product = _products.GetAll().Where(p => p.ProductId == item.ItemId).Select(p => new
                            {
                                p.PicPath,
                                p.ProductName,
                                p.ProductGroup.ProductGroupTitle,
                                OffPercent = p.OffPercent ?? 0,
                                p.Price
                            }).Single();

                            list.Add(new ShowOrderViewModel()
                            {
                                Count = item.Count,
                                ItemId = item.ItemId,
                                Price = product.Price ?? 0,
                                ImageName = product.PicPath,
                                Title = product.ProductGroupTitle + " - " + product.ProductName,
                                ItemType = item.ItemType,
                                OffPercent =product.OffPercent,
                                Sum = item.Count * product.Price - item.Count * product.Price * product.OffPercent / 100 ?? 0
                            });
                        }
                        if (item.ItemType == "Package")
                        {
                            var package = _packages.GetAll().Where(p => p.Id == item.ItemId).Select(p => new
                            {
                                p.ImageName,
                                p.PackageTitle,
                                p.Cost,
                                OffPercent = p.OffPercent ?? 0 
                            }).Single();
                            list.Add(new ShowOrderViewModel()
                            {
                                Count = item.Count,
                                ItemId = item.ItemId,
                                Price = package.Cost ?? 0,
                                ImageName = package.ImageName,
                                Title = "بسته " + package.PackageTitle,
                                ItemType = item.ItemType,
                                 OffPercent= package.OffPercent,
                                Sum = item.Count * package.Cost - item.Count * package.Cost * package.OffPercent / 100 ?? 0
                            });
                        }

                        if (item.ItemType == "Dooreh")
                        {
                            var dooreh = _dooreh.GetAll().Where(p => p.Id == item.ItemId).Select(p => new
                            {
                                p.ImageName,
                                p.Title,
                                p.Cost,
                                OffPercent = p.OffPercent ?? 0
                            }).Single();
                            list.Add(new ShowOrderViewModel()
                            {
                                Count = item.Count,
                                ItemId = item.ItemId,
                                Price = dooreh.Cost ?? 0,
                                ImageName = dooreh.ImageName,
                                Title = "دوره " + dooreh.Title,
                                ItemType = item.ItemType,
                                OffPercent = dooreh.OffPercent,
                                Sum = item.Count * dooreh.Cost - item.Count * dooreh.Cost * dooreh.OffPercent / 100 ?? 0
                            });
                        }
                    }
                }
            }
            else
            {
                if (Session["ShopCart"] != null)
                {
                    List<ShopCartItem> listShop = Session["ShopCart"] as List<ShopCartItem>;

                    foreach (var item in listShop)
                    {
                        if (item.ItemType == "Product")
                        {
                            var product = _products.GetAll().Where(p => p.ProductId == item.ItemId).Select(p => new
                            {
                                p.PicPath,
                                p.ProductName,
                                p.ProductGroup.ProductGroupTitle,
                                p.OffPercent,
                                p.Price
                            }).Single();

                            list.Add(new ShowOrderViewModel()
                            {
                                Count = item.Count,
                                ItemId = item.ItemId,
                                Price = product.Price ?? 0,
                                ImageName = product.PicPath,
                                Title = product.ProductGroupTitle + " - " + product.ProductName,
                                ItemType = item.ItemType,
                                Sum = item.Count * product.Price ?? 0
                            });
                        }
                        if (item.ItemType == "Package")
                        {
                            var package = _packages.GetAll().Where(p => p.Id == item.ItemId).Select(p => new
                            {
                                p.ImageName,
                                p.PackageTitle,
                                p.Cost
                            }).Single();
                            list.Add(new ShowOrderViewModel()
                            {
                                Count = item.Count,
                                ItemId = item.ItemId,
                                Price = package.Cost ?? 0,
                                ImageName = package.ImageName,
                                Title = "بسته " + package.PackageTitle,
                                ItemType = item.ItemType,
                                Sum = item.Count * package.Cost ?? 0
                            });
                        }

                        if (item.ItemType == "Dooreh")
                        {
                            var dooreh = _dooreh.GetAll().Where(p => p.Id == item.ItemId).Select(p => new
                            {
                                p.ImageName,
                                p.Title,
                                p.Cost
                            }).Single();
                            list.Add(new ShowOrderViewModel()
                            {
                                Count = item.Count,
                                ItemId = item.ItemId,
                                Price = dooreh.Cost ?? 0,
                                ImageName = dooreh.ImageName,
                                Title = "دوره " + dooreh.Title,
                                ItemType = item.ItemType,
                                Sum = item.Count * dooreh.Cost ?? 0
                            });
                        }
                    }
                }
            }


            return list;
        }
        [OverrideAuthorization]
        public virtual ActionResult Order()
        {
            return PartialView(getListOrder());
        }
        [OverrideAuthorization]
        public virtual ActionResult CommandOrder(int id, string itemType, int count)
        {
            List<ShopCartItem> listShop = Session["ShopCart"] as List<ShopCartItem>;
            int index = listShop.FindIndex(p => p.ItemId == id && p.ItemType == itemType);
            if (count == 0)
            {
                listShop.RemoveAt(index);
            }
            else
            {
                listShop[index].Count = count;
            }
            Session["ShopCart"] = listShop;

            return PartialView("Order", getListOrder());
        }

        [Authorize]
        public virtual ActionResult Payment()
        {
            var listDetails = getListOrder();

            int userId = _user.GetCurrentUserId();
            Order order = new Order()
            {
                UserID = userId,
                OrderDate = DateTime.Now,
                IsFinalized = false,
                Sum = listDetails.Sum(o => o.Sum),
                OrderStatusId = 0

            };
            _orders.Add(order);



            //  int totalprice = 1000;
            foreach (var item in listDetails)
            {
                if (item.ItemType == "Product" || item.ItemType == "Dooreh")
                {
                    _orderDetails.Add(new OrderDetail()
                    {
                        OrderedCount = item.Count,
                        OrderID = order.OrderID,
                        Price = item.Price,
                        ItemId = item.ItemId,
                        ItemType = item.ItemType,
                        OrderDetailParenID = null,
                        ShowForCusomer = true,
                        IsTransfered = false

                    });
                    //  totalprice += item.Price;
                }
                if (item.ItemType == "Package")
                {
                    _orderDetails.Add(new OrderDetail()
                    {
                        OrderedCount = item.Count,
                        OrderID = order.OrderID,
                        Price = item.Price,
                        ItemId = item.ItemId,
                        ItemType = item.ItemType,
                        OrderDetailParenID = null,
                        ShowForCusomer = true,
                        IsTransfered = false

                    });
                    var packageProduct = _packageProduct.GetAll(item.ItemId);
                    foreach (var product in packageProduct)
                    {
                        _orderDetails.Add(new OrderDetail()
                        {
                            OrderedCount = item.Count,
                            OrderID = order.OrderID,
                            Price = product.Products.Price ?? 0,
                            ItemId = product.ProductId,
                            ItemType = "Product",
                            OrderDetailParenID = item.ItemId,
                            ShowForCusomer = false,
                            IsTransfered = false

                        });
                    }
                    //  totalprice += item.Price;
                }
            }

            _uow.SaveAllChanges();
            PaymentUniqueNumber po = new PaymentUniqueNumber { OrderID = order.OrderID };
            _payment.Add(po);
            _uow.SaveAllChanges();
            //TODO : Online Payment
            System.Net.ServicePointManager.Expect100Continue = false;
            ZarinPal.PaymentGatewayImplementationServicePortTypeClient zp = new ZarinPal.PaymentGatewayImplementationServicePortTypeClient();
            string Authority;
            int amount = order.Sum;
            string description = " درگاه زرین پال در پارسیان دانش";
            string email = "info@parsian-danesh.ir";
            string mobile = "09123715763";
            string callbackUrl = "http://wwww.parsian-danesh.ir/ShopCart/Verify/" + po.PaymentUniqueID;
            int Status = zp.PaymentRequest("4cdd0752-bc9e-4d32-8658-05cf3d3c8425", amount, description, email, mobile, callbackUrl, out Authority);

            if (Status == 100)
            {
                Response.Redirect("https://zarinpal.com/pg/StartPay/" + Authority);
                //Response.Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + Authority);
            }
            else
            {
                ViewBag.Error = "Error : " + Status;
            }
            return View();
        }

        public virtual ActionResult Verify(int id)
        {
            var curPaymentUniquNumber = _payment.Find(id);
            if (curPaymentUniquNumber != null)
            {
                if (Request.QueryString["Status"] != "" && Request.QueryString["Status"] != null && Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null)
                {
                    if (Request.QueryString["Status"].ToString().Equals("OK"))
                    {
                        int Amount = 100;// order.Sum;
                        long RefID;
                        System.Net.ServicePointManager.Expect100Continue = false;
                        ZarinPal.PaymentGatewayImplementationServicePortTypeClient zp = new ZarinPal.PaymentGatewayImplementationServicePortTypeClient();

                        int Status = zp.PaymentVerification("4cdd0752-bc9e-4d32-8658-05cf3d3c8425", Request.QueryString["Authority"].ToString(), Amount, out RefID);

                        if (Status == 100)
                        {
                            var order = _orders.Find(curPaymentUniquNumber.OrderID);
                            order.IsFinalized = true;
                            _uow.SaveAllChanges();
                            ViewBag.IsSuccess = true;
                            //ViewBag.RefId = RefID;
                            ViewBag.RefId = "کد پیگیری: " + RefID + " - کد سفارش: " + order.OrderID;
                            Session["ShopCart"] = null;
                            // Response.Write("Success!! RefId: " + RefID);
                        }
                        else
                        {
                            ViewBag.Status = GetMessage(Status);
                        }

                    }
                    else
                    {
                        Response.Write("Error! Authority: " + Request.QueryString["Authority"].ToString() + " Status: " + Request.QueryString["Status"].ToString());
                    }
                }
                else
                {
                    Response.Write("کد مرجع: " + Request.QueryString["Authority"] + " - وضعیت:" + Request.QueryString["Status"]);
                }
                return View();
            }
            Response.Write("شماره سفارش موردنظر نامعتبر می باشد");
            return View();
        }
        public string GetMessage(int status)
        {
            switch (status)
            {
                case -1:
                    return "اطلاعات ارسال شده  ناقص است.";
                case -2:
                    return "IP و یا مرچنت کد پذیرنده صحیح نیست.";
                case -3:
                    return "با توجه به محدودیت های شاپرک امکان پرداخت با رقم درخواست شده میسر نمی باشد.";
                case -4:
                    return "سطح تایید پذیرنده پایین تر از سطح نقره ای است.";
                case -11:
                    return "درخواست مورد نظر یافت نشد.";
                case -12:
                    return "امکان ویرایش درخواست میسر نمی باشد.";
                case -21:
                    return "هیچ نوع عملیات مالی برای این تراکنش یافت نشد.";
                case -22:
                    return "تراکنش ناموفق می باشد.";
                case -33:
                    return "رقم تراکنش با رقم پرداخت شده مطابقت ندارد.";
                case -34:
                    return "سقف تقسیم تراکنش از لحاظ تعداد یا رقم عبور نموده است.";
                case -40:
                    return "اجازه دسترسی به متد مربوطه وجود ندارد.";
                case -41:
                    return "اطلاعات ارسال شده مربوط به AdditionalData غیر معتبر می باشد.";
                case -42:
                    return "مدت زمان معتبر طول عمر شناسه پرداخت باید بین 30 دقیقه تا 45 روز می باشد.";
                case -54:
                    return "درخواست مورد نظر آرشیو شده است.";
                case 100:
                    return "عملیات با موفقیت انجام گردیده است.";
                case 101:
                    return "عملیات پرداخت موفق بوده و قبلاً PaymentVerification تراکنش انجام شده است.";
                default:
                    return "کد تعریف نشده.";
            }
        }
    }
}