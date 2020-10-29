using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Context;
using DomainClass.Models;
using System.Data.Entity;
using ServiceLayer.Interfaces;



namespace ServiceLayer.EFServices
{
    public class PaymentLogService : IPaymentLogService
    {
        IUnitOfWork _uow;
        IDbSet<PaymentLog> _paymentLog;

        public PaymentLogService(IUnitOfWork uow)
        {
            _uow = uow;
            _paymentLog = uow.Set<PaymentLog>();
        }

        public PaymentLog Add(PaymentLog item)
        {
            return _paymentLog.Add(item);
        }

        public void Remove(PaymentLog item)
        {
            _paymentLog.Remove(item);
        }
        public IList<PaymentLog> GetAll()
        {
            return _paymentLog.ToList();
        }

        //public IList<ProductGroupVM> GetAllActive()
        //{
        //    return _paymentLog.Where(ll => ll.IsActive == true).Select(a => new ProductGroupVM
        //    {
        //        ProductGroupId = a.ProductGroupId,
        //        ProductGroupTitle = a.ProductGroupTitle
        //    }).ToList();
        //}
        public bool IsExist(int paymentLogId)
        {
            return _paymentLog.Any(x => x.PaymentLogID == paymentLogId);
        }
        public PaymentLog Find(int paymentLogId)
        {
            return _paymentLog.Find(paymentLogId);
        }

        
    }
}
