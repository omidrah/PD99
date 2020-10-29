using DataLayer.Context;
using DomainClass.Models;
using ServiceLayer.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ServiceLayer.EFServices
{
    public class PaymentUniqueNumberService : IPaymentUniqueNumberService
    {
        IUnitOfWork _uow;
        IDbSet<PaymentUniqueNumber> _paymentUniqueNumber;

        public PaymentUniqueNumberService(IUnitOfWork uow)
        {
            _uow = uow;
            _paymentUniqueNumber = uow.Set<PaymentUniqueNumber>();
        }

        public PaymentUniqueNumber Add(PaymentUniqueNumber item)
        {
            return _paymentUniqueNumber.Add(item);
        }

        public void Remove(PaymentUniqueNumber item)
        {
            _paymentUniqueNumber.Remove(item);
        }
        public IList<PaymentUniqueNumber> GetAll()
        {
            return _paymentUniqueNumber.ToList();
        }

        //public IList<ProductGroupVM> GetAllActive()
        //{
        //    return _paymentLog.Where(ll => ll.IsActive == true).Select(a => new ProductGroupVM
        //    {
        //        ProductGroupId = a.ProductGroupId,
        //        ProductGroupTitle = a.ProductGroupTitle
        //    }).ToList();
        //}
        public bool IsExist(int paymentUniqueNumberId)
        {
            return _paymentUniqueNumber.Any(x => x.PaymentUniqueID == paymentUniqueNumberId);
        }
        public PaymentUniqueNumber Find(int paymentUniqueNumberId)
        {
            return _paymentUniqueNumber.Find(paymentUniqueNumberId);
        }

        
    }
}
