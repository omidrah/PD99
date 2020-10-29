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
   public  class MahaleBargozariService : IMahaleBargozariService
    {
        IUnitOfWork _uow;
        IDbSet<MahaleBargozari> _mahaleBargozari;
        
        public MahaleBargozariService(IUnitOfWork uow)
        {
            _uow = uow;
            _mahaleBargozari = uow.Set<MahaleBargozari>();
        }

        public MahaleBargozari Add(MahaleBargozari item)
        {
            return _mahaleBargozari.Add(item);
        }

        public void Remove(MahaleBargozari item)
        {
            _mahaleBargozari.Remove(item);
        }
        public IList<MahaleBargozari> GetAll()
        {
            return _mahaleBargozari.Where(ll=>ll.IsDeleted == false).ToList();
        }
        public bool IsExist(int drsid)
        {
            return _mahaleBargozari.Any(x => x.Id == drsid);
        }
        public MahaleBargozari Find(int? mahalId) {
            return _mahaleBargozari.Find(mahalId);
        }

        //public MahaleBargozari FirstOrDefualt(int? mahalId)
        //{
        //    return _mahaleBargozari.FirstOrDefault(ll);
        //}
    }
}
