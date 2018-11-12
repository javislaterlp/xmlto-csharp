using System;
using System.Xml;
using System.Text;
using System.IO;

namespace app 
{
    class Program 
    {
        static void Main(string[] args) 
        {
            try 
            {   
                // read xml source
                XmlDocument xml = new XmlDocument();  
                xml.Load(args[0]);

                String content = buildJson(xml);

                // write json response
                using (StreamWriter json = new StreamWriter(args[1])) {
                    json.WriteLine(content);
                }
            }
            catch (IndexOutOfRangeException e) 
            {
                Console.WriteLine("Please enter a file.");
                Console.WriteLine(e.Message);
            }
            catch (Exception e) 
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

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
            doc.Append(isText ? ": {" : ": {\n");

            foreach (XmlAttribute att in node.Attributes)
            {
                doc.Append(isText ? "" : tabsNested);
                doc.Append("\"_");
                doc.Append(att.Name);
                doc.Append("\": \"");
                doc.Append(att.Value);
                doc.Append(isText ? "\", " : "\", \n");
            }

            if (isText) 
            {
                doc.Append("\"#text\": \"");
                doc.Append(node.InnerText);
                doc.Append("\"");
            }
            else 
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    recursiveXml(doc, child, count + 2);
                }

                doc.Append(tabs);
            }
            
            doc.AppendLine(node.NextSibling != null ? "}," : "}");
        }

    }
}
