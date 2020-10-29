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
   public  class DoorehDorusService : IDoorehDorusService
    {
        IUnitOfWork _uow;
        IDbSet<DoorehaDorouse> _DoorehDorus;
        
        public DoorehDorusService(IUnitOfWork uow)
        {
            _uow = uow;
            _DoorehDorus = uow.Set<DoorehaDorouse>();
        }

        public DoorehaDorouse Add(DoorehaDorouse item)
        {
            return _DoorehDorus.Add(item);
        }

        public void Remove(DoorehaDorouse item)
        {
            _DoorehDorus.Remove(item);
        }
        public IList<DoorehaDorouse> GetAll(int doorehId)
        {
            return _DoorehDorus.Where(ll=>ll.IsDeleted == false && ll.DoorehaId==doorehId).ToList();
        }
        public bool IsExist(int doorehDorusId)
        {
            return _DoorehDorus.Any(x => x.Id == doorehDorusId);
        }
        public DoorehaDorouse Find(int doorehDorusId) {
            return _DoorehDorus.Find(doorehDorusId);
        }

        //public Task<IQueryable<Doruse>> GetAllAsync()
        //{
        //    return _Drses.ToListAsync();
        //}
    }
}
