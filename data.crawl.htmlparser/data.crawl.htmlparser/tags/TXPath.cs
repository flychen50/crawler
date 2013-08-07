using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace data.crawl.htmlparser.tags
{
    public class TXPath
    {
        public string Xpath { get; set; }
        public string Attri { get; set; }
        public string Position { get; set; }
        public TXPath(string xpath, string position) 
        {
            this.Xpath = xpath;
            this.Attri = ExtractAttributeName(xpath);
            this.Position = position.ToLower();
        }

        private string ExtractAttributeName(string xpath)
        {
            int index = xpath.IndexOf("/@");
            if (index < 0)
                return null;
            return xpath.Substring(index + 2);
        }
    }
}
