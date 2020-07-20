using DataAccess.Models.StronglyTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models.IRepository
{
    public interface IBugRepository : IInstantCreate
    {
         object SaveBugProfile(BugModel bug);
      
 
        int ExecuteSql(List<Bug> queries);
        object Delete(string source);
        Confirmation SaveSearchProfileConfig(ProfileMatchConfig matchConfig);
        Confirmation GetSearchProfileConfig(string severity);
        object GetBugs(int startNo=0, int lengthNo=0, string search=null, string status=null);
        int GetBugCount();
    }
}
