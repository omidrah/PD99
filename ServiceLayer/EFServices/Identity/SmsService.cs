using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace ServiceLayer.EFServices
{
    public class SmsService : IIdentityMessageService
    {
        private readonly string _baseApi = "https://rest.payamak-panel.com/api/SendSMS/";
        //var payamakPanleUrl = "http://87.107.121.54/api/SendSms/SendSms";
        private static string GetApiPath(string baseApi, string relativePath)
        {
            return $"{baseApi}{relativePath}";
        }
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            //return Task.FromResult(0);
            //پیاده سازی ارسال اس ام اس با سیستم ارسال
            //payamak-panel
            string AccountSid = "pdanesh";
            string AuthToken = "22851161";
            string senderPhone = "2122881161";
            return Task.FromResult(SendSms(AccountSid, AuthToken, senderPhone, message.Destination, message.Body));
        }
        private string SendPostRequest(string address, string data)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers[HttpRequestHeader.Accept] = "application/json";

                    byte[] result = client.UploadData(address, "POST", Encoding.UTF8.GetBytes(data));
                    return Encoding.UTF8.GetString(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

        }
        public Result SendSms(string userName, string password, string from, string to, string text)
        {
            SendMessage sendMessage = new SendMessage
            {
                username = userName,
                password = password,
                From = from,
                To = to,
                Text = text,
                IsFlash = false
            };

            string res = SendPostRequest(GetApiPath(_baseApi, "SendSMS"), JsonConvert.SerializeObject(sendMessage));

            return JsonConvert.DeserializeObject<Result>(res);
        }
        public Result GetMyCredit(string userName, string password)
        {
            Account account = new Account
            {
                username = userName,
                password = password
            };
            string res = SendPostRequest(GetApiPath(_baseApi, "GetCredit"), JsonConvert.SerializeObject(account));

            return JsonConvert.DeserializeObject<Result>(res);

        }
        public Result GetMessageStatus(string userName, string password, long recId)
        {
            DeliverRequest deliverRequest = new DeliverRequest
            {
                username = userName,
                password = password,
                RecId = recId
            };
            string res = SendPostRequest(GetApiPath(_baseApi, "GetDeliveries2"), JsonConvert.SerializeObject(deliverRequest));

            return JsonConvert.DeserializeObject<Result>(res);
        }
    }
    public class Result
    {
        public string Value { get; set; }

        public int RetStatus { get; set; }

        public string StrRetStatus { get; set; }

    }
    public class SendMessage
    {
        public string username { get; set; }
        public string password { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Text { get; set; }
        public bool IsFlash { get; set; }
    }
    public class Account
    {
        public string username { get; set; }

        public string password { get; set; }
    }
    public class DeliverRequest : Account
    {
        public long RecId { get; set; }
    }

}
