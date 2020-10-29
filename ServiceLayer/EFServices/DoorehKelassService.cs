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
   public  class DoorehKelassService : IDoorehKelassService
    {
        IUnitOfWork _uow;
        IDbSet<KelassDooreh> _kelassDooreh;
        
        public DoorehKelassService(IUnitOfWork uow)
        {
            _uow = uow;
            _kelassDooreh = uow.Set<KelassDooreh>();
        }

        public KelassDooreh Add(KelassDooreh item)
        {
            return _kelassDooreh.Add(item);
        }

        public void Remove(KelassDooreh item)
        {
            _kelassDooreh.Remove(item);
        }
        public IList<KelassDooreh> GetAllKelass(int doorehId)
        {
            return _kelassDooreh.Where(ll=>ll.IsDeleted == false && ll.DoorehaId==doorehId).ToList();
        }
        public IList<KelassDooreh> GetAllDooreh(int KelassId)
        {
            return _kelassDooreh.Where(ll => ll.IsDeleted == false && ll.KelassId == KelassId).ToList();
        }
        public bool IsExist(int doorehKelassId)
        {
            return _kelassDooreh.Any(x => x.KelassDoorehId == doorehKelassId);
        }
        public KelassDooreh Find(int doorehKelassId) {
            return _kelassDooreh.Find(doorehKelassId);
        }

    }
}
