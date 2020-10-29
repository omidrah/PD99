using System.Collections.Generic;
using DomainClass.Models;
using ServiceLayer.EFServices;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface ICustomerService
    {
        //IList<Customer> GetAll();
        IList<CustomerVm> GetAll();
        Customer Add(Customer item);
        Customer Find(int customerId);
    }
}