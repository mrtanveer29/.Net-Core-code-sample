using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
    public class BugExcelInfo
    {
        [Key]
        public int bug_excel_info_id { get; set; }
        public string bug_excel_info_file_name { get; set; }
        public string dumping_date { get; set; }
        public string excel_file_dump_status { get; set; }
    }
}
