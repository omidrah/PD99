using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IOrderDetailsService
    {
        OrderDetail Add(OrderDetail item);
        OrderDetail Find(int orderId);
        IList<OrderDetail> GetAll();
        bool IsExist(int orderId);
        void Remove(OrderDetail item);
    }
}