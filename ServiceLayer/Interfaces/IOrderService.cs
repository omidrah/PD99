using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IOrderService
    {
        Order Add(Order item);
        Order Find(int orderId);
        IList<Order> GetAll();
        bool IsExist(int orderId);
        void Remove(Order item);
    }
}