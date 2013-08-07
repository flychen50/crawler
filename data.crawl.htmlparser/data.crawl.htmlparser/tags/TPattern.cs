using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace data.crawl.htmlparser.tags
{
    public class TPattern
    {
        public Regex Regex { get; set; }
        public TPattern(string pattern) 
        {
            Regex = new Regex(pattern);
        }
    }
}
