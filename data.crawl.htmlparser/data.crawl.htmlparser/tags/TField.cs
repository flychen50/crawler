using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace data.crawl.htmlparser.tags
{
    public class TField
    {
        public string Name { get; set; }
        public TXPath TXPath { get; set; }
        public TRegex TRegex { get; set; }
        public TConst TConst { get; set; }
        public TField(string name, TXPath txpath, TRegex tregex, TConst tconst) 
        {
            this.Name = name;
            this.TXPath = txpath;
            this.TRegex = tregex;
            this.TConst = tconst;
        }
    }
}
