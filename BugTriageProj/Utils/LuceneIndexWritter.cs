using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BugTriage.Utils
{
    public class LuceneIndexWritter
    {
       

        public static Directory CreateIndex(DataTable dt1)
        {
            // We are going to enhance the table so we make a copy
            var dt = dt1.Copy();

            // This column will contain the data of all other columns
            // Used for freetext searches
            string IndexColName="";
            var allCol = new DataColumn(IndexColName, typeof(string)) { DefaultValue = "" };

            dt.Columns.Add(allCol);

            // Index
            string indexPath = "C://myIndex";
            FSDirectory dir = FSDirectory.Open(indexPath);

           
            using (Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48)) 
            using (var writer = new IndexWriter(dir, new IndexWriterConfig(Lucene.Net.Util.LuceneVersion.LUCENE_48,analyzer)))
            { // the writer and analyzer will popuplate the directory with docuemnts

                foreach (DataRow dr in dt.Rows)
                {
                    var doc = new Document(); // Lucene document to be part of the index

                    // For each column in the table create a document field
                    // This allows searches on individual fields
                    //
                    // In addition we copy the contents of each column to a new column
                    // this will be used for a freetext search                    
                    foreach (DataColumn dc in dr.Table.Columns)
                    {
                        var val = string.Empty;

                        if (dr[dc.ColumnName] != null)
                        {
                            val = dr[dc.ColumnName].ToString();
                        }

                        if (dc.ColumnName != allCol.ColumnName)
                        {

                            // Add column as an indexed field of the document
                            doc.Add( new StringField(dc.ColumnName, val, Field.Store.YES));

                            // This column will have the contents of all other columns
                            // So we can freetext search very easily
                            // This is lazy technique that won't scale, but if your data set is small enough
                            // will simplify search
                            dr[allCol] = string.Format("{0}{1}", dr[allCol], val);
                        }
                    }

                    // add the allcolumn field to the document
                    doc.Add(new StringField(allCol.ColumnName, dr[allCol].ToString(), Field.Store.YES));

                    writer.AddDocument(doc);
                }

                writer.Commit();
               
            }

            return dir;
        }
    }
}
