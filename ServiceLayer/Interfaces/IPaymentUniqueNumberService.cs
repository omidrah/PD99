using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IPaymentUniqueNumberService
    {
        PaymentUniqueNumber Add(PaymentUniqueNumber item);
        PaymentUniqueNumber Find(int paymentUniqueNumberId);
        IList<PaymentUniqueNumber> GetAll();
        bool IsExist(int paymentUniqueNumberId);
        void Remove(PaymentUniqueNumber item);
    }
}