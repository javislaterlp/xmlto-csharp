using System;
using System.Xml;
using System.Text;
using System.IO;

namespace xmltojson 
{
    class Program 
    {
        static void Main(string[] args) 
        {
            try 
            {   
                // if no second argument is supplied, input name will be taken
                var input = args[0];
                var output = args.Length > 1 ? args[1] : input.Remove(input.Length - 4) + ".json";

                // read xml source
                XmlDocument xml = new XmlDocument();  
                xml.Load(args[0]);

                String content = ToJson.buildJson(xml);

                // write json response
                using (StreamWriter json = new StreamWriter(output)) {
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
    }
}
