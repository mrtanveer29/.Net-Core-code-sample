using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models.IRepository
{
    public interface IInstantCreate
    {
        int CreateInsatntProductProfile(string ProductName);
        int CreateInsatntComponentProfile(string ComponentName);
        int CreateInsatntHardwareProfile(string HardwareName);
        int CreateInsatntDeveloperProfile(string DeveloperName, int developerTypeId=0);
    }
}
