using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using data.crawl.htmlparser.tags;
using HtmlAgilityPack;
using System.Xml;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.IO;

namespace data.crawl.htmlparser
{
    public class HtmlParser<T> where T : class
    {
        private Type type;
        private Type type_list;
        private XmlSerializer xs;
        private XmlSerializer xs_list;

        public HtmlParser()
        {
            type = typeof(T);
            xs = new XmlSerializer(type);
            type_list = typeof(List<T>);
            xs_list = new XmlSerializer(type_list);
        }

        public T GetEntity(string html, ParseConfig config)
        {
            TSave tsave = config.Page.TSave;
            if (tsave == null)
                throw new ArgumentNullException(tsave.GetType().Name);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            T obj = AnalyzeTSave(tsave, doc);
            return obj;
        }

        public List<T> GetEntityList(string html, ParseConfig config)
        {
            TSave_M tsave_m = config.Page.TSave_M;
            if (tsave_m == null)
                throw new ArgumentNullException(tsave_m.GetType().Name);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            List<T> obj = AnalyzeTSave_M(tsave_m, doc);
            return obj;
        }

        private List<T> AnalyzeTSave_M(TSave_M tsave_m, HtmlDocument doc)
        {
            string xRoot = tsave_m.RootxPath;
            List<TField> fields = tsave_m.TFields;

            HtmlNodeCollection hnc = doc.DocumentNode.SelectNodes(xRoot);
            if (hnc == null)
                throw new Exception("root position failed  " + xRoot);

            XmlDocument xdResult = new XmlDocument();
            XmlDeclaration dec = xdResult.CreateXmlDeclaration("1.0", null, null);
            xdResult.AppendChild(dec);
            XmlElement root = xdResult.CreateElement("ArrayOf" + type.Name);
            xdResult.AppendChild(root);

            foreach (HtmlNode hn in hnc)
            {
                foreach (TField tfield in fields)
                {
                    XmlElement root_e = xdResult.CreateElement(type.Name);
                    root.AppendChild(root_e);
                    AnalyzeTField(tfield, hn, root_e, xdResult);
                }
            }

            using (StringReader reader = new StringReader(xdResult.OuterXml))
            {
                List<T> obj = (List<T>)xs_list.Deserialize(reader);
                return obj;
            }

        }

        private T AnalyzeTSave(TSave tsave, HtmlDocument doc)
        {
            string xRoot = tsave.RootxPath;
            List<TField> fields = tsave.TFields;

            HtmlNode hn = doc.DocumentNode.SelectSingleNode(xRoot);
            if (hn == null)
                throw new Exception("root position failed  " + xRoot);

            XmlDocument xdResult = new XmlDocument();
            XmlDeclaration dec = xdResult.CreateXmlDeclaration("1.0", null, null);
            xdResult.AppendChild(dec);
            XmlElement root = xdResult.CreateElement(type.Name);
            xdResult.AppendChild(root);

            foreach (TField tfield in fields)
            {
                AnalyzeTField(tfield, hn, root, xdResult);
            }

            using (StringReader reader = new StringReader(xdResult.OuterXml))
            {
                T obj = (T)xs.Deserialize(reader);
                return obj;
            }

        }

        private void AnalyzeTField(TField tfield, HtmlNode hn, XmlElement root, XmlDocument xdResult)
        {
            string name = tfield.Name;
            TConst tconst = tfield.TConst;
            TXPath txpath = tfield.TXPath;
            TRegex tregex = tfield.TRegex;

            if (txpath == null)
            {
                String valConst = AnalyzeTConst(tconst);
                AddElement(name, valConst, root, xdResult);
                return;
            }

            String valXpath = AnalyzeTXPath(txpath, hn);
            if (tregex == null)
            {
                AddElement(name, valXpath, root, xdResult);
                return;
            }

            String valRegex = AnalyzeTRegex(tregex, valXpath);
            AddElement(name, valRegex, root, xdResult);
        }

        private string AnalyzeTRegex(TRegex tregex, string valXpath)
        {
            Regex regex = tregex.TPattern.Regex;
            string format = tregex.TFormat == null ? "" : tregex.TFormat.Format;

            Match m = regex.Match(valXpath);

            if (format == null)
                return m.Groups[1].Value;

            List<string> valGroup = new List<string>();
            for (int i = 1; i < m.Groups.Count; i++)
            {
                string gv = m.Groups[i].Value;
                valGroup.Add(gv);
            }

            string value = Format(format, valGroup);
            return value;
        }

        private string Format(String format, List<string> valGroup)
        {
            for (int i = 0; i < valGroup.Count; i++)
            {
                format = format.Replace("{" + i + "}", valGroup[i]);
            }
            return format;
        }

        private string AnalyzeTConst(TConst tconst)
        {
            EConst ec = tconst.eConst;
            switch (ec)
            {
                case EConst.now:
                    return DateTime.Now.ToString();
                case EConst.value:
                default:
                    return tconst.tValue.Value;
            }
        }

        private string AnalyzeTXPath(TXPath txpath, HtmlNode hnPre)
        {
            string xpath = txpath.Xpath;
            string attri = txpath.Attri;
            string position = txpath.Position;

            //特别处理
            if (xpath.StartsWith("./@"))
            {
                string val = hnPre.Attributes[attri].Value;
                return val;
            }

            HtmlNode hn = hnPre.SelectSingleNode(xpath);
            if (hn == null)
                throw new Exception("xpath position failed " + xpath);

            if (attri != null)
            {
                string val = hn.Attributes[attri].Value;
                return val;
            }

            String value = Position(hn, position);
            return value;
        }

        private string Position(HtmlNode hn, string position)
        {
            switch (position)
            {
                case "innertext":
                    return hn.InnerText;
                case "outerhtml":
                    return hn.OuterHtml;
                case "innerhtml":
                    return hn.InnerHtml;
                default:
                    throw new Exception("only support (innerText,innerHtml,outerHtml) " + position);
            }
        }

        private void AddElement(string name, string val, XmlElement root, XmlDocument doc)
        {
            XmlElement xe = doc.CreateElement(name);
            xe.InnerText = val;
            root.AppendChild(xe);
        }

    }
}
