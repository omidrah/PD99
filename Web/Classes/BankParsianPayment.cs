using System;
using System.Configuration;
using System.Web;
using System.Web.UI;

namespace Web
{
    public class BankParsianPayment
    {
        #region IPaymentInterface Members


        public bool ProcessPayment(int Amount, int UniqueOrderID)
        {
            long authority = 0;
            byte status = 0;

            string MerchantID = ConfigurationManager.AppSettings["BankParsianMerchantID"];

            string returnPageURL = GetReturnPageUrl(UniqueOrderID);

            //var service = new EShopService();

            ////service.Url = "https://www.pec24.com/pecpaymentgateway/EShopService.asmx";
            
            //service.PinPaymentRequest(MerchantID, Amount, UniqueOrderID, returnPageURL, ref authority, ref status);

            //if (status == 0)
            //{
            //    HttpContext.Current.Response.Redirect("https://www.pec24.com/pecpaymentgateway/default.aspx?au=" + authority,
            //                                 true);
            //}

            return true;
        }

        public PaymentResponse ProcessResponse(string authorityStr, string response, int Amount, int UniqueOrderID)
        {
            string merchantID = ConfigurationManager.AppSettings["BankParsianMerchantID"];

            string statusmessage = string.Empty;
            PaymentResponse paymentResponse = new PaymentResponse();
            

            try
            {
                //var service = new EShopService();
                //service.Url = "https://www.pec24.com/pecpaymentgateway/EShopService.asmx";



                // if response is succesful, eShops have to check it again
                if (response == "0" && authorityStr != null)
                {
                    long authority = long.Parse(authorityStr);

                    byte status = 0;
                  //  service.PinPaymentEnquiry(merchantID, authority, ref status);

                    // if this method failed, eShop has to call the following method
                    // service.Reversal(authority, ref status);
                    // to be sure about payment reversal


                    paymentResponse.ResponseMessage = "موفقیت آمیز";
                    paymentResponse.ResponseCode = status;
                    paymentResponse.Successful = true;
                    paymentResponse.Transaction_ID = authorityStr;
                }
                else
                {
                    paymentResponse.ResponseMessage = "خطا در پرداخت";
                    paymentResponse.ResponseCode = int.Parse(response);
                    paymentResponse.Successful = false;
                    paymentResponse.Transaction_ID = authorityStr;
                }
            } // Response.Write("آخییییش، به خیر گذشت... ارور نداد");
            catch (Exception err)
            {
                paymentResponse.ResponseMessage = err.Message;
                paymentResponse.ResponseCode = int.Parse(response);
                paymentResponse.Successful = false;
                paymentResponse.Transaction_ID = authorityStr;

            }
            return paymentResponse;
        }

        #endregion

        private string GetReturnPageUrl(int UniqueOrderID)
        {
            string domainName =
                HttpContext.Current.Request.Url.AbsoluteUri.Remove(
                    HttpContext.Current.Request.Url.AbsoluteUri.IndexOf(HttpContext.Current.Request.Url.AbsolutePath));
            string pageURL = "/ShoppingCart/ProcessOnlinePayment" + "?OID=" + UniqueOrderID;
            return domainName + pageURL;
        }

        private string BankResponseCodeDictionary(int ResponseCode)
        {
            string result = "ناشناخته";
            switch (ResponseCode)
            {
                case (int) PgwStatus.MerchantAuthenticationFailed:
                    {
                        result = "IP پذیرنده نامعتبر است";
                        break;
                    }
                case (int) PgwStatus.Successful:
                    {
                        result = "موفقیت آمیز";
                        break;
                    }
                case (int) PgwStatus.Timeout:
                    {
                        result = "زمان ارتباط بیش از حد طولانی شده است";
                        break;
                    }
            }
            return result;
        }
    }

    public enum PgwStatus : int
    {
        #region Common Statuses (0-9)

        /// <summary>
        /// transactions have been done successfully!
        /// </summary>
        Successful = 0,

        /// <summary>
        /// when status is dropped into an unknown state, this situations have
        /// to be known and are removed.
        /// </summary>
        Unknown = 1,

        /// <summary>
        /// when timeout error occured, often when the authority timeout occured
        /// </summary>
        Timeout = 2,

        #endregion

        #region Financial Errors (10-19)

        /// <summary>
        /// when the card number is invalid
        /// </summary>
        InvalidCard = 10,

        /// <summary>
        /// when expiration date is invalid, or card has been expired
        /// </summary>
        ExpiredCard = 11,

        /// <summary>
        /// when pin code is incorrect
        /// </summary>
        IncorrectPin = 12,

        /// <summary>
        /// when amount is not in the valid ranges, or customer balance is not sufficant
        /// </summary>
        InvalidAmount = 13,

        /// <summary>
        /// when amount is upper than merchant's max transaction amount 
        /// </summary>
        InvalidMerchantMaxTransAmount = 14,

        #endregion

        #region Security Errors (20-29)

        AccessViolation = 20,

        InvalidAuthority = 21,

        MerchantAuthenticationFailed = 22,

        #endregion

        #region Logical Statuses (30-49)

        SaleIsAlreadyDoneSuccessfully = 30,

        SaleIsVoidedSuccessfully = 31,

        SaleIsReversaledSuccessfully = 32,

        /// <summary>
        /// customer tried passing his/her information some times for invalid 
        /// counts.
        /// </summary>
        ValidFailureCountPassed = 33,

        InvalidMerchantOrder = 34,

        Inconsistency = 35,

        /// <summary>
        /// transaction with the given order id already has been voided successfully
        /// </summary>
        SaleIsAlreadyVoidedSuccessfully = 36,

        /// <summary>
        /// transaction with the given order id already has been reversaled successfully
        /// </summary>
        SaleIsAlreadyReversaledSuccessfully = 37,

        RefundAmountIsUpperThanOrderAmount = 38,
        RefundAmountIsUpperThanCountOfOrdersAmount = 39,

        #endregion

        #region Sale Tracing Statuses (50-59)

        /// <summary>
        /// transaction registered in database and is going to perform
        /// </summary>
        Pending = 50,

        OrderReceived = 51,

        //CustomerPaymentRequest = 42,
        InProgress = 52,

        EnquiriedByMerchant = 53,

        #endregion

        #region Bank Switch related errors (60-79)

        /// <summary>
        /// receive response from bank switch failed!
        /// </summary>
        ReceiveError = 60,

        /// <summary>
        /// sending request to bank switch failed
        /// </summary>
        SendError = 61,

        MerchantNotLogin = 62,

        FormatError = 63,
        InvalidCardReader = 64,
        InvalidProductCodes = 65,
        IssuerOrSwitchInoperative = 66,
        ReconcileError = 67,
        RecordNotFound = 68,
        ReEnterTransaction = 69,
        Referral = 70,
        SESystemMlfunction = 71,
        SN = 72,
        TraceNumberNotFound = 73,
        TransNotPermitted2Term = 74,
        BadTerminalId = 75,
        BankNotSupportedBySwitch = 76,
        BatchNumberNotFound = 77,
        DuplicateTransmission = 78,
        TransNotOK = 79,
        UnNoneError = 80,

        #endregion

        #region Application Errors (90-99)

        /// <summary>
        /// when an exception is raised through performing the a process, must
        /// be discovered it position and be removed
        /// </summary>
        ExceptionRaised = 90,

        /// <summary>
        /// database transactions failed!
        /// </summary>
        DatabaseError = 91,

        #endregion

    }
}
