using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models.StronglyTypes
{
    public class DeveloperModel : Developer
    {
        public string DeveloperTypeName { get; set; }
        public int SolveCount { get; set; }
        public string ProductName { get; set; }
        public string ComponentName { get; set; }
        public string HardwareName { get; set; }
        public List<ProductModel> ProductsWorked { get; set; }
        public List<ComponentModel> ComponentsWorked { get; set; }
        public List<HardwareModel> HardwaresWorked { get; set; }
    }
}
