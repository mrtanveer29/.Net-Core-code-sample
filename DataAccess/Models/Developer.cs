using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Developer
    {
        public int DeveloperId { get; set; }

        public string DeveloperName { get; set; }
        public int? DeveloperTypeId { get; set; }
        public string Skills { get; set; }
        public string PreviousWorks { get; set; }
        public int? current_workload { get; set; }
        public bool? is_indexed { get; set; }
    }
}
