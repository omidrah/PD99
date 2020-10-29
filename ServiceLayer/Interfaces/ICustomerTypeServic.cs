using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface ICustomerTypeServic
    {
        IList<CustomerType> GetAll();
        CustomerType Add(CustomerType item);
        void AddList(IList<CustomerType> items);
    }
}