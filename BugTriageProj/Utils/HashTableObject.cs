using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTriage.Utils
{
    public class HashTableObject
    {
        public string key { get; set; }
        public string value { get; set; }
        public string ClaimType { get; set; }
        public bool IsSelected { get; set; }
    }
}