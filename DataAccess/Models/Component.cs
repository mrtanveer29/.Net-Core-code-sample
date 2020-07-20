using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
   public class Component
    {
        [Key]
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string DevelopersWorked { get; set; }
    }
}
