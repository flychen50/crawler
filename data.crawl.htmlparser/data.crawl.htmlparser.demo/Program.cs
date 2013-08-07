using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace data.crawl.htmlparser.demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //HtmlParser<News> parser = new HtmlParser<News>();
            //ParseConfig config = new ParseConfig("save.xml");
            //String html = File.ReadAllText("1.txt",Encoding.GetEncoding("GBK"));
            //News entity = parser.GetEntity(html, config);


            //HtmlParser<PriceInfo> parser = new HtmlParser<PriceInfo>();
            //ParseConfig config = new ParseConfig("save_list.xml");
            //String html = File.ReadAllText("2.html", Encoding.GetEncoding("UTF-8"));
            //List<PriceInfo> entity = parser.GetEntityList(html, config);

            HtmlParser<HotelInfo> parser = new HtmlParser<HotelInfo>();
            ParseConfig config = new ParseConfig("save_xc.xml");
            String html = File.ReadAllText("xiecheng.txt", Encoding.GetEncoding("GBK"));
            HotelInfo entity = parser.GetEntity(html, config);
            
            Console.WriteLine();
        }
    }
}
