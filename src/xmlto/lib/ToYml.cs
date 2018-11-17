using System;
using System.Xml;
using System.Text;
using System.IO;

namespace xmlto
{
    class ToYml 
    {

        public static String buildYml(XmlDocument xml) 
        {
            StringBuilder doc = new StringBuilder();

            foreach (XmlNode node in xml.ChildNodes)
            {
                if (!node.NodeType.Equals(XmlNodeType.XmlDeclaration))
                {
                    // number of tabs to insert
                    int count = 0;
                    recursiveXml(doc, node, count);
                }
            }

            Console.WriteLine(doc);

            return doc.ToString();
        }

        public static void recursiveXml(StringBuilder doc, XmlNode node, int count) 
        {
            string tabs = new String(' ', count);
            string tabsNested = new String(' ', count + 2);

            bool isText = node.FirstChild.NodeType.Equals(XmlNodeType.Text);

            doc.Append(tabs);
            doc.AppendFormat("{0}:", node.Name);
            doc.AppendLine();

            foreach (XmlAttribute att in node.Attributes)
            {
                doc.Append(tabsNested);
                doc.AppendFormat("_{0}: {1}", att.Name, att.Value);
                doc.AppendLine();
            }

            if (isText) 
            {
                doc.Append(tabsNested);
                doc.AppendFormat("content: {0}", node.InnerText);
                doc.AppendLine();
            }
            else 
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    recursiveXml(doc, child, count + 2);
                }
            }
        }

    }
}
