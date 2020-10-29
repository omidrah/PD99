using DataLayer.Context;
using DomainClass.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ServiceLayer.EFServices
{
    public class MasterService : IMasterService
    {

        private IUnitOfWork _uow;
        private readonly IDbSet<Master> _Masters;
        private readonly IDbSet<User> _Users;
        public MasterService(IUnitOfWork uow)
        {
            _uow = uow;
            _Masters = _uow.Set<Master>();
            _Users = _uow.Set<User>();
        }
        public IList<Master> GetAll()
        {
            return _Masters.Include("Users").Where(x => x.Users.IsActive == true && x.Users.IsDeleted == false).ToList();
        }
        public IList<MasterVm> GetInfoAll()
        {
            var all = _Masters.Include("Users").Where(x => x.Users.IsActive == true && x.Users.IsDeleted == false).Select(a => new MasterVm()
            {
                UserId = a.UserId,
                Name = a.Users.Name + " " + a.Users.Family,
                Rotbe = a.Rotbe,
                Takhasos = a.Takhasos,
                IsHeiatElmi = a.IsHeiatElmi,
                Bio = a.Bio,
                ImgThumbPath = a.Users.ImgPath
            });
            return all.ToList();
        }
        public IList<FMasterVm> InfoAll()
        {
            var all = _Masters.Include("Users").Where(x => x.Users.IsActive == true && x.Users.IsDeleted==false).Select(a => new FMasterVm()
            { UserId = a.UserId, Name = a.Users.Name + " " + a.Users.Family, Rotbe = a.Rotbe, ImgThumbPath = a.Users.ImgPath });
            return all.ToList();
        }
        public Master Find(int id)
        {
          return _Masters.Find(id);
            
        }
    }

}
