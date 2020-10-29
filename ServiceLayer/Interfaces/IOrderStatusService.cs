using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IOrderStatusService
    {
        OrderStatus Add(OrderStatus item);
        OrderStatus Find(int orderId);
        IList<OrderStatus> GetAll();
        bool IsExist(int orderStatusId);
        void Remove(OrderStatus item);
    }
}