using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
    public class BugDeveloperMapping
    {
        [Key]
        public int bug_developer_mapping_id { get; set; }
        public int developer_id { get; set; }
        public int bug_id { get; set; }
    }
}
