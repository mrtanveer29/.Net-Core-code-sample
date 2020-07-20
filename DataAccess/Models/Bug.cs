using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Bug
      
    {
        [Key]
        public int BugId { get; set; }
        [StringLength(255)]
      
        public string Status { get; set; }
        public int Product { get; set; }
        public int Component { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal version { get; set; }
        public int Severity { get; set; }
        public int Assignee { get; set; }
        public int Priority { get; set; }
        
        public int Hardware { get; set; }
        [StringLength(255)]
        public string Resolution { get; set; }
        [StringLength(100)]
        public string CreatedDate { get; set; }
        [StringLength(100)]
        public string CreatedTime { get; set; }
        [StringLength(100)]
        public string Changed { get; set; }
        public int id { get; set; }

        public string Summary { get; set; }
        public string KeyWords { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public Nullable<int> bug_excel_info_id { get; set; }
    }
}
