using Elekta.Envctl.Converters;
using Elekta.Envctl.Models;
using Elekta.Envctl.Models.Helm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Elekta.Envctl
{
    class Program
    {
        const string rootPath = "C:\\Elekta\\elekta-platform-sdk";
        const string elementsPath = "scripts\\devctl.d\\elekta-platform-elements";

        static void Main(string[] args)
        {
            var docker = GetStdOutForProcess("docker", "version");
            Console.WriteLine(docker);
            
            var output = new OutputModel();
            
            var k8s = GetStdOutForProcess("kubectl", "version");
            output.KubernetesVersion = new StringReader(k8s).DeserialiseKubernetesVersion();
            output.HelmCharts = GetChartVersions();

            Console.WriteLine(output);
            Console.ReadLine();
        }

        private static string GetStdOutForProcess(string fileName, string arguments)
        {
            var result = new StringBuilder();
            using (var process = new Process())
            {
                process.StartInfo = new ProcessStartInfo(fileName, arguments)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                };

                process.Start();
                while (!process.HasExited)
                {
                    result.Append(process.StandardOutput.ReadToEnd());
                }
            }
            return result.ToString();
        }

        private static IList<HelmChart> GetChartVersions()
        {
            var path = Path.Combine(rootPath, elementsPath);
            var charts = new List<HelmChart>();

            foreach (var file in Directory.GetFiles(path,"*.yaml"))
            {
                var yaml = new YamlStream();
                using var fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                using var sr = new StreamReader(fs, Encoding.UTF8);

                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();

                charts.Add(deserializer.Deserialize<HelmChart>(sr));
            }

            return charts;
        }
    }
}
