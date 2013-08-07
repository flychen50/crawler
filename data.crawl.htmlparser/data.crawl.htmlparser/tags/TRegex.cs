using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace data.crawl.htmlparser.tags
{
    public class TRegex
    {
        public TPattern TPattern { get; set; }
        public TFormat TFormat { get; set; }
        public TRegex(TPattern tPattern, TFormat tFormat)
        {
            this.TPattern = tPattern;
            this.TFormat = tFormat;
        }
    }
}
