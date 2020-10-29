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
   public  class SettingsService : ISettingsService
    {
        IUnitOfWork _uow;
        IDbSet<Settings> _settings;
        
        public SettingsService(IUnitOfWork uow)
        {
            _uow = uow;
            _settings = uow.Set<Settings>();
        }

        public Settings Add(Settings item)
        {
            return _settings.Add(item);
        }

        public void Remove(Settings item)
        {
            _settings.Remove(item);
        }
        public IList<Settings> GetAll()
        {
            return _settings.Where(ll=>ll.IsDeleted == false).ToList();
        }
        public bool IsExist(int settingId)
        {
            return _settings.Any(x => x.Id == settingId);
        }
        public Settings Find(int settingId) {
            return _settings.Find(settingId);
        }

        
    }
}
