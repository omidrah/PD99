using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IPaymentLogService
    {
        PaymentLog Add(PaymentLog item);
        PaymentLog Find(int paymentLogId);
        IList<PaymentLog> GetAll();
        bool IsExist(int paymentLogId);
        void Remove(PaymentLog item);
    }
}