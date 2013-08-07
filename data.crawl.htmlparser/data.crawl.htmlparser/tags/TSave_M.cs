using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace data.crawl.htmlparser.tags
{
    public class TSave_M
    {
        public string RootxPath { get; set; }
        public List<TField> TFields { get; set; }
        public TSave_M(String rootxPath,List<TField> fields) 
        {
            this.RootxPath = rootxPath;
            this.TFields = fields;
        }
    }
}
