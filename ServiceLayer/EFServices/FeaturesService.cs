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
   public  class FeaturesService : IFeaturesService
    {
        IUnitOfWork _uow;
        IDbSet<Features> _features;
        
        public FeaturesService(IUnitOfWork uow)
        {
            _uow = uow;
            _features = uow.Set<Features>();
        }

        public Features Add(Features item)
        {
            return _features.Add(item);
        }

        public void Remove(Features item)
        {
            _features.Remove(item);
        }
        public IList<Features> GetAll()
        {
            return _features.Where(ll=>ll.IsDeleted == false).ToList();
        }
        public bool IsExist(int featuresId)
        {
            return _features.Any(x => x.FeatureId == featuresId);
        }
        public Features Find(int featuresId) {
            return _features.Find(featuresId);
        }

        //public Task<IQueryable<Doruse>> GetAllAsync()
        //{
        //    return _Drses.ToListAsync();
        //}
    }
}
