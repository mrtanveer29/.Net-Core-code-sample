using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
    public class ProductDeveloperMapping
    {
        [Key]
        public int product_developer_mapping_id { get; set; }
        public int developer_id { get; set; }
        public int product_id { get; set; }
    }
}
