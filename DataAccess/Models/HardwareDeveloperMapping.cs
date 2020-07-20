using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
    public class HardwareDeveloperMapping
    {
        [Key]
        public int hardware_developer_mapping_id { get; set; }
        public int developer_id { get; set; }
        public int hardware_id { get; set; }
    }
}
