using System;
using System.Xml;
using System.Text;
using System.IO;

namespace xmltojson 
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
            doc.Append("\"");
            doc.Append(node.Name);
            doc.Append("\"");
            doc.AppendLine(": {");

            foreach (XmlAttribute att in node.Attributes)
            {
                doc.Append(tabsNested);
                doc.Append("\"_");
                doc.Append(att.Name);
                doc.Append("\": \"");
                doc.Append(att.Value);
                doc.AppendLine("\", ");
            }

            if (isText) 
            {
                doc.Append(tabsNested);
                doc.Append("\"content\": \"");
                doc.Append(node.InnerText);
                doc.AppendLine("\"");
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
