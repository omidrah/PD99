using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DomainClass.Models;
using System.Data.Entity;
using ServiceLayer.Interfaces;

namespace ServiceLayer.EFServices
{
   public  class OrderStatusService : IOrderStatusService
    {
        IUnitOfWork _uow;
        IDbSet<OrderStatus> _orderStatus;
        
        public OrderStatusService(IUnitOfWork uow)
        {
            _uow = uow;
            _orderStatus = uow.Set<OrderStatus>();
        }

        public OrderStatus Add(OrderStatus item)
        {
            return _orderStatus.Add(item);
        }

        public void Remove(OrderStatus item)
        {
            _orderStatus.Remove(item);
        }
        public IList<OrderStatus> GetAll()
        {
            return _orderStatus.ToList();
        }

        public bool IsExist(int orderStatusId)
        {
            return _orderStatus.Any(x => x.Id == orderStatusId);
        }
        public OrderStatus Find(int orderId) {
            return _orderStatus.Find(orderId);
        }

        //public Task<IQueryable<Doruse>> GetAllAsync()
        //{
        //    return _Drses.ToListAsync();
        //}
    }
}
