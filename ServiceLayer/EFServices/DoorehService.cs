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
   public  class DoorehService : IDoorehService
    {
        IUnitOfWork _uow;
        IDbSet<Dooreha> _Dooreh;
        
        public DoorehService(IUnitOfWork uow)
        {
            _uow = uow;
            _Dooreh = uow.Set<Dooreha>();
        }

        public Dooreha Add(Dooreha item)
        {
            return _Dooreh.Add(item);
        }

        public void Remove(Dooreha item)
        {
            _Dooreh.Remove(item);
        }
        public IList<Dooreha> GetAll()
        {
            return _Dooreh.Where(ll=>ll.IsDeleted == false).ToList();
        }
        public bool IsExist(int doorehId)
        {
            return _Dooreh.Any(x => x.Id == doorehId);
        }
        public Dooreha Find(int doorehId) {
            return _Dooreh.Find(doorehId);
        }

        //public Task<IQueryable<Doruse>> GetAllAsync()
        //{
        //    return _Drses.ToListAsync();
        //}
    }
}
