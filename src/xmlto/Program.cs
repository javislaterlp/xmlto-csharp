using System;
using System.Xml;
using System.Text;
using System.IO;

namespace xmlto
{
    class Program 
    {
        static void Main(string[] args) 
        {
            try 
            {   
                // if no second file is supplied, input name will be taken
                var to = args[0];
                var input = args[1];
                var output = args.Length > 2 ? args[2] : input.Remove(input.Length - 4) + "." + to;

                // read xml source
                XmlDocument xml = new XmlDocument();  
                xml.Load(input);

                String content = "";

                switch (to)
                {
                    case "json":
                        content = ToJson.buildJson(xml);
                        break;
                    case "yml":
                        content = ToYml.buildYml(xml);
                        break;
                    default:
                        Console.WriteLine("Conversion not supported.");
                        return;
                }

                // write response
                using (StreamWriter response = new StreamWriter(output)) {
                    response.WriteLine(content);
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
    }
}
