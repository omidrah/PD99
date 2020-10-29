using DataLayer.Context;
using DomainClass.Models;
using ServiceLayer.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using ServiceLayer.Models;

namespace ServiceLayer.EFServices
{
    public class PersonelService : IPersonelService
    {

        private IUnitOfWork _uow;
        private readonly IDbSet<Personel> _personels;
        public PersonelService(IUnitOfWork uow)
        {
            _uow = uow;
            _personels = _uow.Set<Personel>();
        }
        public IList<Personel> GetAll()
        {
            return _personels.ToList();
        }

        public Personel Add(Personel item)
        {
            return _personels.Add(item);
        }

        public Personel Find(int userId)
        {
            return _personels.Find(userId);
        }

        public bool IsExist(int userId)
        {
            throw new NotImplementedException();
        }

        public void Remove(Personel item)
        {
            throw new NotImplementedException();
        }

        public IList<PersonelVM> GetInfoAll()
        {
            var all = _personels.Include("Users").Where(x => x.Users.IsActive == true && x.Users.IsDeleted == false).Select(a => new PersonelVM()
            {
                UserId = a.UserId,
                Name = a.Users.Name + " " + a.Users.Family,
                Age = a.Age,
                PersonliNo = a.PersonliNo,
                HasInsurance = a.HasInsurance,
                ImgThumbPath = a.Users.ImgPath
            });
            return all.ToList();
        }
    }
}
