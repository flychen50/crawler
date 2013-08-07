using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace data.crawl.htmlparser.tags
{
    public class TSave
    {
        public string RootxPath { get; set; }
        public List<TField> TFields { get; set; }
        public TSave(string rootxpath, List<TField> fields)
        {
            this.RootxPath = rootxpath;
            this.TFields = fields;
        }
    }
}
