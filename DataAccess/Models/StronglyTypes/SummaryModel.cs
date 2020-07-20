using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models.StronglyTypes
{
    public class SummaryModel
    {
        public int? DeveloperType { get; set; }
        public int Count { get; set; }
        public int Products { get; set; }
        public int Components { get; set; }
        public int Bugs { get; set; }
        public int Developers { get; set; }
        public decimal AvaragePrecesion { get; set; }
        public decimal AvarageRecall { get; set; }
        public decimal AvarageFscore { get; set; }
    }
}
