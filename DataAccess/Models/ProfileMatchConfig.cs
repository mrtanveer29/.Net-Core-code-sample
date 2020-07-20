using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
    public class ProfileMatchConfig
    {
        [Key]
        public int profile_match_config_id { get; set; }
        [StringLength(50)]
        public string severity { get; set; }
        public int no_of_sed { get; set; }
        public int no_of_ned { get; set; }
        public int no_of_fd { get; set; }

    }
}
