using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTriage.ViewModels
{
    public class BugViewModel 
    {
        [Display(Name = "Bug ID")]
        public int Id { get; set; }
        [Display(Name ="Bug ID")]
        public int ManualId { get; set; }
        [Key]
        public int BugId { get; set; }
    
        public string BugTitle { get; set; }
        [StringLength(255)]
        [Required]
        public string Products { get; set; }
        [StringLength(255)]
        [Required]
        public string Component { get; set; }
        [Display(Name = "Hardware/Platform")]
        [Required]
        public string Hardware { get; set; }
        [StringLength(255)]
        public string Resolution { get; set; }
        [Display(Name ="Priority")]
        public int Priority { get; set; }
        [Required]
        public int Saverity { get; set; }
        [Display(Name = "Severity")]
        public string SeverityName { get; set; }
        public string KeyWords { get; set; }
        public decimal version { get; set; }
        public string Status { get; set; }

        public string Assignee { get; set; }
       
        [Required]
        public string Summery { get; set; }
        public string Description { get; set; }
        public string Changed { get; set; }
    }
}
