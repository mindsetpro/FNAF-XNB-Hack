using System;
using System.IO;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace FNAFWorldAssetExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            // Path to exe
            string exePath = @"C:\Game\fnaf-world-0-1-24.exe";

            // Dictionaries to store extracted assets
            Dictionary<string, byte[]> extractedImages = new Dictionary<string, byte[]>();
            Dictionary<string, string> extractedJson = new Dictionary<string, string>();
            Dictionary<string, string> extractedXml = new Dictionary<string, string>();

            // Load assembly
            using (AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(exePath))
            {
                // Iterate embedded resources
                foreach (EmbeddedResource resource in assembly.MainModule.Resources)
                {
                    // Get name
                    string name = resource.Name;

                    // Get data
                    byte[] data = resource.GetResourceData();

                    // Check file type  
                    if (name.EndsWith(".png"))
                    {
                        extractedImages.Add(name, data);
                    }
                    else if (name.EndsWith(".json"))
                    {
                        string json = System.Text.Encoding.UTF8.GetString(data);
                        extractedJson.Add(name, json);
                    }
                    else if(name.EndsWith(".xml"))
                    {
                        string xml = System.Text.Encoding.UTF8.GetString(data);
                        extractedXml.Add(name, xml);  
                    }
                }
            }

            // Assets now extracted into dictionaries
            Console.WriteLine("Extracted:");
            Console.WriteLine($"{extractedImages.Count} images"); 
            Console.WriteLine($"{extractedJson.Count} json files");
            Console.WriteLine($"{extractedXml.Count} xml files");
        }
    }
}
