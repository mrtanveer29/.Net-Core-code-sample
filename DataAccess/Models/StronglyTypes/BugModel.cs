using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models.StronglyTypes
{
    public class BugModel :Bug
    {
        public string ProductName { get; set; }
        public string ComponentName { get; set; }
        public string HardwareName { get; set; }
        public string AssigneeName { get; set; }
        public string SeverityTitle { get; set; }
        public string PriorityTitle { get; set; }
        public decimal Precision { get; set; }
        public decimal Recall { get; set; }
        public decimal Fscore { get; set; }
        public List<Developer> predictedDevelopers { get; set; }



    }
}
