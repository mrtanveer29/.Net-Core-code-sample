using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BugTriage.ViewModels
{
    public class ProfileMatchConfigViewModel
    {
        
        public int profile_match_config_id { get; set; }
        [Display(Name = "Severity"), Required]
        public string severity { get; set; }
        [Display(Name ="Senior Experienced Developers")]
        [Range(0,100),Required]
        public int no_of_sed { get; set; }
        [Display(Name = "New Experienced Developers")]
        [Range(0, 100), Required]
        public int no_of_ned { get; set; }
        [Display(Name = "Fresh Developers")]
        [Range(0, 100), Required]
        public int no_of_fd { get; set; }

    }
}
