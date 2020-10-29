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
   public  class OrderDetailsService : IOrderDetailsService
    {
        IUnitOfWork _uow;
        IDbSet<OrderDetail> _orderDetails;
        
        public OrderDetailsService(IUnitOfWork uow)
        {
            _uow = uow;
            _orderDetails = uow.Set<OrderDetail>();
        }

        public OrderDetail Add(OrderDetail item)
        {
            return _orderDetails.Add(item);
        }

        public void Remove(OrderDetail item)
        {
            _orderDetails.Remove(item);
        }
        public IList<OrderDetail> GetAll()
        {
            return _orderDetails.ToList();
        }

        public bool IsExist(int orderId)
        {
            return _orderDetails.Any(x => x.OrderID == orderId);
        }
        public OrderDetail Find(int orderId) {
            return _orderDetails.Find(orderId);
        }

        //public Task<IQueryable<Doruse>> GetAllAsync()
        //{
        //    return _Drses.ToListAsync();
        //}
    }
}
