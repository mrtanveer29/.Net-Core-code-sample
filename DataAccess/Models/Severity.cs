using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Severity
    {
        [Key]
        public int SeverityId { get; set; }
        public string SeverityName { get; set; }
    }
}
