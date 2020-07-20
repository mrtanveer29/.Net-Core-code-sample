using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lucene.Net.Documents;
namespace BugTriage.Search
{
    public class SearchResult
    {
        private readonly Document _doc;

        public SearchResult(Document doc)
        {
            _doc = doc;
        }

        public int DeveloperType { get; set; }
        public string Name { get; set; }

        public string Component { get; set; }
        public string Product { get; set; }

        public string Skill { get; set; }

        public void Parse(Action<Document> parseAction)
        {
            parseAction(_doc);
        }
    }
}