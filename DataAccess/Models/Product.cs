using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [MaxLength(255)]
        public string ProductName { get; set; }
        public string DevelopersWorked { get; set; }
    }
}
