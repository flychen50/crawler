using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace data.crawl.htmlparser.tags
{
    public class TPage
    {
        public TSave TSave { get; set; }
        public TSave_M TSave_M { get; set; }
        public TPage(TSave tsave, TSave_M tsave_m)
        {
            this.TSave = tsave;
            this.TSave_M = tsave_m;
        }
    }
}
