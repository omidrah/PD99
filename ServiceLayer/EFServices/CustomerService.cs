using DataLayer.Context;
using DomainClass.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ServiceLayer.EFServices
{
    public class CustomerService : ICustomerService
    {

        private IUnitOfWork _uow;
        private readonly IDbSet<Customer> _customers;
        public CustomerService(IUnitOfWork uow)
        {
            _uow = uow;
            _customers = _uow.Set<Customer>();
        }
        //public IList<Customer> GetAll()
        //{
        //    return _customers.ToList();
        //}

        public IList<CustomerVm> GetAll()
        {
            var list = _customers.Include(x => x.Users).Where(x => x.Users.IsDeleted == false).Select(x => new CustomerVm
            {
                customerTypeId = (byte)x.CustomerTypeId,
                customerTypeTitle = x.CustomerType.Title,
                Family = x.Users.Family,
                Name = x.Users.Name,
             //   FullName = x.Users.Name + " " + x.Users.Family,
                IsActive = x.Users.IsActive == true ? true : false,
                IsMadrakTaeed = x.IsMadrakTaeed,
                UserId = x.UserId,
                Email = x.Users.Email,
                Mobile = x.Users.PhoneNumber,
                Tel = x.Users.Tel,
                Address = x.Users.Address,
                nationCode = x.Users.NationCode,
                PostalCode = x.Users.PostalCode,
                PicPath = x.Users.ImgPath,
                Password = x.Users.PasswordHash,
                IsMadrakChecked = x.IsMadrakChecked,
                PicDoc = x.ImgDocPath.Substring(1, x.ImgDocPath.Length - 1),
                CreateDate = x.Users.CreatedDate,
                birthDate = x.Users.BirthDate
            }).ToList();

            foreach (var item in list)
            {
                item.FullName = item.Name + " " + item.Family;
            }
            return  list;
        }
        public Customer Add(Customer item)
        {
            return _customers.Add(item);
        }

        public Customer Find(int customerId)
        {
            return _customers.Find(customerId);
        }
    }
}
