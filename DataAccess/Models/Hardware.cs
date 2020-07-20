using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
    public class Hardware
    {
        [Key]
        public int hardware_id { get; set; }
        [StringLength(255)]
        public string hardware_name { get; set; }
    }
}
