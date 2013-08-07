using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace data.crawl.htmlparser.tags
{
    public class TConst
    {
        public EConst eConst { get; set; }
        public TValue tValue { get; set; }
        public TConst(string econst, TValue tvalue)
        {
            eConst = (EConst)Enum.Parse(typeof(EConst), econst);
            this.tValue = tvalue;
        }
    }
}
