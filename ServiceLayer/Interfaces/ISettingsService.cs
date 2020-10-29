using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface ISettingsService
    {
        Settings Add(Settings item);
        Settings Find(int settingId);
        IList<Settings> GetAll();
        bool IsExist(int settingId);
        void Remove(Settings item);
    }
}