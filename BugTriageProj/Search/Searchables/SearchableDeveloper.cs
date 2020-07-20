using DataAccess.Models;
using DataAccess.Models.StronglyTypes;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTriage.Search.Searchables
{
    public class SearchableDeveloper : Searchable
    {
        public SearchableDeveloper(DeveloperModel dev)
        {
            Product = dev.ProductName??"";
            Component =dev.ComponentName??"";
            Id = dev.DeveloperId;
            Skill = dev.Skills??"";
            Name = dev.DeveloperName;
            DeveloperType = dev.DeveloperTypeId??1;
        }

        public override string Product { get; }
        public override string Component { get; }
        public override string Skill { get; }
        public override int Id { get; }
        public override string Name { get; }
        public override int DeveloperType { get; }
    }
}
