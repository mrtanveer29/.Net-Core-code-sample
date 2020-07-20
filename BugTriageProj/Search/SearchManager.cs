using BugTriage.Search.Searchables;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Util;
using System;
using Lucene.Net.Search;

using System.Linq;
using Lucene.Net.QueryParsers.Classic;

namespace BugTriage.Search
{
    public class SearchManager : ISearchManager
    {
        private static FSDirectory _directory;
        private readonly IHostingEnvironment _env;

        public SearchManager(IHostingEnvironment env)
        {
            _env = env;
        }

        private FSDirectory Directory
        {
            get
            {
                if (_directory != null)
                {
                    return _directory;
                }

                var info = System.IO.Directory.CreateDirectory(LuceneDir);
                return _directory = FSDirectory.Open(info);
            }
        }

        private string LuceneDir => Path.Combine(_env.ContentRootPath, "Lucene_Index");

        public void AddToIndex(params Searchable[] searchables)
        {
            UseWriter(x => x.DeleteAll());
            UseWriter(x =>
            {
                foreach (var searchable in searchables)
                {
                    var doc = new Document();
                    foreach (var field in searchable.GetFields())
                    {
                        doc.Add(field);
                    }
                    x.AddDocument(doc);
                }
            });
        }
        public void DeleteFromIndex(params Searchable[] searchables)
        {
            UseWriter(x =>
            {
                foreach (var searchable in searchables)
                {
                    x.DeleteDocuments(new Term(Searchable.FieldStrings[Searchable.Field.Id], searchable.Id.ToString()));
                }
            });
        }
        public void Clear()
        {
            UseWriter(x => x.DeleteAll());
        }

        public SearchResultCollection Search(string searchQuery, int hitsStart, int hitsStop, string[] fields)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return new SearchResultCollection();
            }
            if (searchQuery == "General" || searchQuery=="general")
            {
                int a = 0;
            }

            const int hitsLimit = 1000;
            SearchResultCollection results;
            using (var analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48))
            {
                using (var reader = DirectoryReader.Open(Directory))
                {
                    var searcher = new IndexSearcher(reader);
                    
                     var parser = new MultiFieldQueryParser(LuceneVersion.LUCENE_48, fields, analyzer);
                    var query = parser.Parse(QueryParserBase.Escape(searchQuery.Trim()));
                    var hits = searcher.Search(query, null, hitsLimit, Sort.RELEVANCE).ScoreDocs;
                    results = new SearchResultCollection
                    {
                        Count = hits.Length,
                        Data = hits.Where((x, i) => i >= hitsStart && i < hitsStop)
                            .Select(x => new SearchResult(searcher.Doc(x.Doc)))
                            .ToList()
                    };
                }
            }
            return results;
        }

        private void UseWriter(Action<IndexWriter> action)
        {
            using (var analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48))
            {
                using (var writer = new IndexWriter(Directory, new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer)))
                {
                    action(writer);
                    writer.Commit();
                }
            }
        }
    }
}
