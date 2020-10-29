using DataLayer.Context;
using DomainClass.Models;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.EFServices
{
    public class CustomerTypeServic : ICustomerTypeServic
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<CustomerType> _ctyp;
        public CustomerTypeServic(IUnitOfWork uow)
        {
            _uow = uow;
            _ctyp = uow.Set<CustomerType>();            
        }
        public IList<CustomerType> GetAll()
        {
            return _ctyp.ToList();
        }  
        public CustomerType Add(CustomerType item)
        {
            return _ctyp.Add(item);
        }
        public void AddList(IList<CustomerType> items)
        {
            foreach (var item in items)
            {
                _ctyp.Add(item);
            }
        }
    }
}
