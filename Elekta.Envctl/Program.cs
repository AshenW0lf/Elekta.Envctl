using Elekta.Envctl.Converters;
using Elekta.Envctl.Models.Helm;
using System;
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
            var k8s = GetStdOutForProcess("kubectl", "version");
            //Console.WriteLine(k8s);
            var info = new StringReader(k8s).DeserialiseKubernetesVersion();
            Console.WriteLine($"Client Version: {info.Client.FullVersion}");
            Console.WriteLine($"Server Version: {info.Server.FullVersion}");
            GetChartVersions();

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

        private static string GetChartVersions()
        {
            var path = Path.Combine(rootPath, elementsPath);

            foreach (var file in Directory.GetFiles(path,"*.yaml"))
            {
                var yaml = new YamlStream();
                using var fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                using var sr = new StreamReader(fs, Encoding.UTF8);

                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                var order = deserializer.Deserialize<HelmChart>(sr);
                
                Console.WriteLine($"Name: {order.Spec.Chart.Name}");
                Console.WriteLine($"Version: {order.Spec.Chart.Version}");
                Console.WriteLine();
            }

            return string.Empty;
        }
    }
}
