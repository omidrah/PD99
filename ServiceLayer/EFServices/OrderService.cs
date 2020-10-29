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
   public  class OrderService : IOrderService
    {
        IUnitOfWork _uow;
        IDbSet<Order> _order;
        
        public OrderService(IUnitOfWork uow)
        {
            _uow = uow;
            _order = uow.Set<Order>();
        }

        public Order Add(Order item)
        {
            return _order.Add(item);
        }

        public void Remove(Order item)
        {
            _order.Remove(item);
        }
        public IList<Order> GetAll()
        {
            return _order.ToList();
        }

        public bool IsExist(int orderId)
        {
            return _order.Any(x => x.OrderID == orderId);
        }
        public Order Find(int orderId) {
            return _order.Find(orderId);
        }

        //public Task<IQueryable<Doruse>> GetAllAsync()
        //{
        //    return _Drses.ToListAsync();
        //}
    }
}
