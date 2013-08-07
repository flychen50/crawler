using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace data.crawl.htmlparser.tags
{
    public class TFormat
    {
        public string Format { get; set; }
        public TFormat(string format)
        {
            this.Format = format;
        }
    }
}
