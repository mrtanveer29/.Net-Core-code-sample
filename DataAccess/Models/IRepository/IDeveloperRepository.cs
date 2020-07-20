using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Models.StronglyTypes;

namespace DataAccess.Models.IRepository
{
    public interface IDeveloperRepository : IInstantCreate
    {
        IEnumerable<DeveloperTypes> GetDeveloperTypes();
        Object AddDeveloper(DeveloperModel developer);
        IEnumerable<DeveloperModel> GetDevelopers(int type);
        object GetDeveloper(int developerId);
        Object GetSkills();
        int ExecuteSql(List<DeveloperModel> queries);
        object DeleteDeveloper(int id, int type);
        object GetDevelopersForIndex();
        void MarkAsIndexed(IEnumerable<DeveloperModel> developers);
        ProfileMatchConfig GetConfig(string v);
        object GetCurrentDataCount();
    }
}
