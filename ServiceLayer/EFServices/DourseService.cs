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
   public  class DourseService : IDourseService
    {
        IUnitOfWork _uow;
        IDbSet<Doruses> _Drses;
        
        public DourseService(IUnitOfWork uow)
        {
            _uow = uow;
            _Drses = uow.Set<Doruses>();
        }

        public Doruses Add(Doruses item)
        {
            return _Drses.Add(item);
        }

        public void Remove(Doruses item)
        {
            _Drses.Remove(item);
        }
        public IList<Doruses> GetAll()
        {
            return _Drses.Where(ll=>ll.IsDeleted == false).ToList();
        }
        public bool IsExist(int drsid)
        {
            return _Drses.Any(x => x.Id == drsid);
        }
        public Doruses Find(int drsId) {
            return _Drses.Find(drsId);
        }

        //public Task<IQueryable<Doruse>> GetAllAsync()
        //{
        //    return _Drses.ToListAsync();
        //}
    }
}
