using DomainClass.Models;
using ServiceLayer.Models;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IMasterService
    {
        IList<Master> GetAll();
        IList<MasterVm> GetInfoAll();
        IList<FMasterVm> InfoAll();
        Master Find(int id);
    }
}