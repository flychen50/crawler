using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using data.crawl.htmlparser.tags;
using System.Xml;
using System.Text.RegularExpressions;

namespace data.crawl.htmlparser
{
    public class ParseConfig
    {
        public TPage Page;

        public ParseConfig()
        {
        }
        
        public ParseConfig(XmlDocument doc) 
        {
            Initial(doc);
        }

        public ParseConfig(String path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            Initial(doc);
        }

        private void Initial(XmlDocument doc)
        {
            XmlNode xnPage = doc.SelectSingleNode("/template/page");
            if (xnPage == null)
                throw new Exception("no page tag found");
            Page = GetTPage(xnPage);
        }

        private static TPage GetTPage(XmlNode xnPage)
        {
            TSave tsave = GetTSave(xnPage);
            TSave_M tsave_m = GetTSave_M(xnPage);
            TPage tp = new TPage(tsave, tsave_m);
            return tp;
        }

        private static TSave_M GetTSave_M(XmlNode xnPage)
        {
            XmlNode xnSave_m = xnPage.SelectSingleNode("./save_m");
            if (xnSave_m == null) return null;
            XmlNode xnRoot = xnSave_m.Attributes["root"];
            string root = xnRoot == null ? "." : xnRoot.Value;
            List<TField> list = GetTFields(xnSave_m);
            TSave_M ts_m = new TSave_M(root, list);
            return ts_m;
        }

        private static TSave GetTSave(XmlNode xnPage)
        {
            XmlNode xnSave = xnPage.SelectSingleNode("./save");
            if (xnSave == null) return null;
            XmlNode xnRoot = xnSave.Attributes["root"];
            string root = xnRoot == null ? "." : xnRoot.Value;
            List<TField> list = GetTFields(xnSave);
            TSave ts = new TSave(root, list);
            return ts;
        }

        private static List<TField> GetTFields(XmlNode xnSave)
        {
            XmlNodeList xnlField = xnSave.SelectNodes(@"./field");
            if (xnlField == null || xnlField.Count < 1)
                return null;
            List<TField> list = new List<TField>();
            foreach (XmlNode xnField in xnlField)
            {
                TField field = GetTField(xnField);
                list.Add(field);
            }
            return list;
        }

        private static TField GetTField(XmlNode xnField)
        {
            XmlNode xnName = xnField.SelectSingleNode(@"./name");
            if (xnName == null)
                throw new Exception("no name tag found");
            string name = xnName.InnerText;
            TXPath tx = GetTXPath(xnField);
            TRegex tr = GetTRegex(xnField);
            TConst tc = GetTConst(xnField);
            TField tf = new TField(name, tx, tr, tc);
            return tf;
        }

        private static TConst GetTConst(XmlNode xnField)
        {
            XmlNode xnConst = xnField.SelectSingleNode(@"./const");
            if (xnConst == null) return null;
            string econst = xnConst.InnerText;
            TValue tv = GetTValue(xnField);
            TConst tc = new TConst(econst, tv);
            return tc;
        }

        private static TValue GetTValue(XmlNode xnField)
        {
            XmlNode xnValue = xnField.SelectSingleNode(@"./value");
            if (xnValue == null) return null;
            string value = xnValue.InnerText;
            TValue tv = new TValue(value);
            return tv;
        }

        private static TRegex GetTRegex(XmlNode xnField)
        {
            XmlNode xnRegex = xnField.SelectSingleNode(@"./regex");
            if (xnRegex == null) return null;
            TPattern tp = GetTPattern(xnRegex);
            TFormat tf = GetTFormat(xnRegex);
            TRegex tr = new TRegex(tp, tf);
            return tr;
        }

        private static TFormat GetTFormat(XmlNode xnRegex)
        {
            XmlNode xnFormat = xnRegex.SelectSingleNode(@"./format");
            if (xnFormat == null) return null;
            string format = xnFormat.InnerText;
            TFormat tf = new TFormat(format);
            return tf;
        }

        private static TPattern GetTPattern(XmlNode xnRegex)
        {
            XmlNode xnPattern = xnRegex.SelectSingleNode(@"./pattern");
            if (xnPattern == null)
                throw new Exception("no pattern tag found");
            string pattern = xnPattern.InnerText;
            return new TPattern(pattern);
        }

        private static TXPath GetTXPath(XmlNode xnField)
        {
            XmlNode xnXPath = xnField.SelectSingleNode(@"./xpath");
            if (xnXPath == null) return null;
            string xpath = xnXPath.InnerText;
            XmlNode xnPostion = xnXPath.Attributes["position"];
            string position = xnPostion == null ? "innertext" : xnPostion.Value;
            TXPath tx = new TXPath(xpath, position);
            return tx;
        }

    }
}
