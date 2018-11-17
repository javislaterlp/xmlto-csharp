using System;
using System.Xml;
using System.Text;
using System.IO;

namespace xmlto
{
    class ToJson 
    {

        public static String buildJson(XmlDocument xml) 
        {
            StringBuilder doc = new System.Text.StringBuilder();
            doc.AppendLine("{");

            foreach (XmlNode node in xml.ChildNodes)
            {
                if (!node.NodeType.Equals(XmlNodeType.XmlDeclaration))
                {
                    // number of tabs to insert
                    int count = 2;
                    recursiveXml(doc, node, count);
                }
            }

            doc.AppendLine("}");
            Console.WriteLine(doc);

            return doc.ToString();
        }

        public static void recursiveXml(StringBuilder doc, XmlNode node, int count) 
        {
            string tabs = new String(' ', count);
            string tabsNested = new String(' ', count + 2);

            bool isText = node.FirstChild.NodeType.Equals(XmlNodeType.Text);

            doc.Append(tabs);
            doc.AppendFormat("\"{0}\": ", node.Name);
            doc.AppendLine("{");

            foreach (XmlAttribute att in node.Attributes)
            {
                doc.Append(tabsNested);
                doc.AppendFormat("\"_{0}\": \"{1}\",\n", att.Name, att.Value);
            }

            if (isText) 
            {
                doc.Append(tabsNested);
                doc.AppendFormat("\"content\": \"{0}\"\n", node.InnerText);
            }
            else 
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    recursiveXml(doc, child, count + 2);
                }
            }
            
            doc.Append(tabs);
            doc.AppendLine(node.NextSibling != null ? "}," : "}");
        }

    }
}
