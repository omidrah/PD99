using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DomainClass.Models;
using System.Data.Entity;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;

namespace ServiceLayer.EFServices
{
    public class KelassService : IKelassService
    {
        IUnitOfWork _uow;
        IDbSet<Kelass> _Kelass;
        private readonly IDbSet<Master> _Masters;
        private readonly IDbSet<User> _Users;
        private readonly IDbSet<Doruses> _Dorous;
        private readonly IDbSet<MahaleBargozari> _MahaleBargozari;

        public KelassService(IUnitOfWork uow)
        {
            _uow = uow;
            _Kelass = uow.Set<Kelass>();
            _Masters = uow.Set<Master>();
            _Users = uow.Set<User>();
            _Dorous = uow.Set<Doruses>();
            _MahaleBargozari = uow.Set<MahaleBargozari>();
        }

        public Kelass Add(Kelass item)
        {
            return _Kelass.Add(item);
        }

        public void Remove(Kelass item)
        {
            _Kelass.Remove(item);
        }
        public IList<Kelass> GetAll()
        {
            return _Kelass.Where(ll => ll.IsDeleted == false).ToList();
        }

        public IList<KelassVM> GetAllKelass()
        {
            
            var all = _Kelass.Include("Doruses").Where(ll => ll.IsDeleted == false).Select(a => new KelassVM
            {
                KelassId = a.KelassId,
                MasterName =a.Masters.Users.Name + " " + a.Masters.Users.Family,
                dorusTitle = a.Doruses.Title
            });
            return all.ToList();
        }

        public bool IsExist(int KelassId)
        {
            return _Kelass.Any(x => x.KelassId == KelassId);
        }
        public Kelass Find(int klassId)
        {
            return _Kelass.Find(klassId);
        }

        //public Task<IQueryable<Doruse>> GetAllAsync()
        //{
        //    return _Drses.ToListAsync();
        //}
    }
}
