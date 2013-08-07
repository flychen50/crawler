using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace data.crawl.htmlparser.tags
{
    public class TValue
    {
        public string Value { get; set; }
        public TValue(string value)
        {
            this.Value = value;
        }
    }
}
