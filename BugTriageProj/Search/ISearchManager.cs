using BugTriage.Search.Searchables;
using BugTriage.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTriage.Search
{
    public interface ISearchManager
    {
        void AddToIndex(params Searchable[] searchables);
        void DeleteFromIndex(params Searchable[] searchables);
        void Clear();
        SearchResultCollection Search(string searchQuery, int hitsStart, int hitsStop, string[] fields);
    }
}
