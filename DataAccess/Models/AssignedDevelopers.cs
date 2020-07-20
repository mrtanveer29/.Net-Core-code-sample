using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
    public partial class AssignedDevelopers
    {
        [Key]
        public int Id { get; set; }
        public int DeveloperId { get; set; }
        public int BugId { get; set; }
    }
}
