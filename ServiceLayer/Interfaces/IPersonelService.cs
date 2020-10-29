using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClass.Models;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
   public  interface IPersonelService
    {
       IList<Personel> GetAll();
       Personel Add(Personel item);
        Personel Find(int userId);
        bool IsExist(int userId);
        void Remove(Personel item);
        IList<PersonelVM> GetInfoAll();

   }
}
