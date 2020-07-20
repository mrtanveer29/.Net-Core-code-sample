using System.Collections.Generic;
using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace BugTriage.Search.Searchables
{
    public abstract class Searchable
    {
        public static readonly Dictionary<Field, string> FieldStrings = new Dictionary<Field, string>
        {
            {Field.Skill, "Skill"},
            {Field.Component, "Component"},
            {Field.Product, "Product"},
            {Field.Id, "Id"},
            {Field.Name, "Name"},
            {Field.DeveloperType, "DeveloperType"}
        };

        public static readonly Dictionary<Field, string> AnalyzedFields = new Dictionary<Field, string>
        {
           
            {Field.Product, FieldStrings[Field.Product] },
            {Field.Component, FieldStrings[Field.Component] },
            {Field.Skill, FieldStrings[Field.Skill] }
        };

        public abstract string Skill { get; }
        public abstract string Component { get; }
        public abstract string Product { get; }
        public abstract int Id { get; }
        public abstract string Name { get; }
        public abstract int DeveloperType { get; }

        public enum Field
        {
            Id,
            Name,
            Product,
            Component,
            Skill,
            DeveloperType
        }

        public IEnumerable<IIndexableField> GetFields()
        {
            return new Lucene.Net.Documents.Field[]
            {
                new StringField(AnalyzedFields[Field.Component], Component, Lucene.Net.Documents.Field.Store.YES),
                new TextField(AnalyzedFields[Field.Product], Product, Lucene.Net.Documents.Field.Store.YES),
                new TextField(AnalyzedFields[Field.Skill], Skill, Lucene.Net.Documents.Field.Store.YES),
                new StringField(FieldStrings[Field.Id], Id.ToString(), Lucene.Net.Documents.Field.Store.YES),
                new StringField(FieldStrings[Field.Name], Name, Lucene.Net.Documents.Field.Store.YES),
                new StringField(FieldStrings[Field.DeveloperType], DeveloperType.ToString(), Lucene.Net.Documents.Field.Store.YES),
               
            };
        }
    }
}