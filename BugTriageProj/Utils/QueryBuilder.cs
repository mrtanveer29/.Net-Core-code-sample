using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTriage.Utils
{
    public class QueryBuilder
    {
        private string _keys;
        private string _updateParams=null;
        private string _conditions=null;
        private string _values;
        private string tableName;
        private string returnColumn="";

        public QueryBuilder(string tableName)
        {
            this.tableName = tableName;
        } 
        public QueryBuilder AddString(string key,string value)
        {
            _values += "'" + value + "',";
            _keys +=  key + ",";
            return this;
        }
        public QueryBuilder AddInteger(string key,string value)
        {
            _keys += key + ",";
            _values += value + ",";
            return this;
        }

        public QueryBuilder UpdateInteger(string key, int value)
        {
            _updateParams += (key +"="+value+",");
            return this;
        }
        public QueryBuilder UpdateString(string key, string value)
        {
            _updateParams += (key + "='" + value + "',");
            return this;
        }
        public QueryBuilder UpdateBool(string key, bool value)
        {
            _updateParams += (key + "=" + value + ",");
            return this;
        }
        public QueryBuilder AddStringCondition(string key, string value)
        {
            _conditions = (_conditions == null) ? "" : " AND ";
            _conditions += (key + "='" + value + "'");
            return this;
        }
        public QueryBuilder AddIntegerCondition(string key, int value)
        {
            _conditions = (_conditions == null) ? "" : " AND ";
            _conditions += (key + "=" + value + "");
            return this;
        }

        public QueryBuilder AddBoolCondition(string key, bool value)
        {
            _conditions = (_conditions == null) ? "" : " AND ";
            _conditions += (key + "=" + value + "");
            return this;
        }
        public QueryBuilder Returning(string column)
        {
            returnColumn = " RETURNING " + column;
            return this;
        }

        public QueryBuilder AddList(List<HashTableObject> value)
        {
            foreach (var item in value)
            {
                _keys += item.key + ",";
                _values += "'"+item.value + "',";
            }
                return this;
        }

        public QueryBuilder AddListForUpdated(List<HashTableObject> value)
        {
            foreach (var item in value)
            {
                _updateParams += (item.key + "='" + value + "', ");
            }
            return this;
        }

        public string GetUpdateQueryString()
        {
            return "UPDATE " + tableName + " SET "+ _updateParams.TrimEnd(',')+ ((_conditions==null)?"" : " WHERE "+_conditions)+ returnColumn;
        }


        public string GetString()
        {
            string restrnval = " (" + _keys.TrimEnd(',') + ")  Values(" + _values.TrimEnd(',') + ") "+ returnColumn;
            return "INSERT INTO "+tableName+restrnval;
        }

    
    }
}