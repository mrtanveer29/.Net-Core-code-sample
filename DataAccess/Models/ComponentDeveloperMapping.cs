using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
    public class ComponentDeveloperMapping
    {
        [Key]
        public int component_developer_mapping_id { get; set; }
        public int developer_id { get; set; }
        public int component_id { get; set; }
    }
}

