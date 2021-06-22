using System;
using System.IO;
using System.Text.RegularExpressions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace LiveSetSummary
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Must specify an .als file.");
                return;
            }

            var alsFile = new FileInfo(args[0]);

            var extensionRegex = new Regex(@"\.als$");

            if (!extensionRegex.IsMatch(alsFile.Extension))
            {
                Console.WriteLine("Must specify an .als file.");
                return;
            }

            if (!alsFile.Exists)
            {
                Console.WriteLine($"File '{alsFile.FullName}' does not exist.");
                return;
            }

            var yamlFile = new FileInfo(extensionRegex.Replace(alsFile.FullName, ".yaml"));
            
            if (yamlFile.Exists)
            {
                Console.WriteLine($"Target file '{yamlFile.FullName}' already exists (and I was born too cautious).");
                return;
            }

            var xmlExtractor = new LiveSetXmlExtractor();
            var xml = xmlExtractor.ExtractXml(alsFile.FullName);

            var parser = new LiveSetXmlParser();
            var liveSet = parser.ParseLiveSet(xml);

            var serializer = new SerializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();

            var liveSetYaml = serializer.Serialize(liveSet);

            File.WriteAllText(yamlFile.FullName, liveSetYaml);
        }
    }
}
