using Elekta.Envctl.Converters;
using Elekta.Envctl.Formatter;
using Elekta.Envctl.Models;
using Elekta.Envctl.Models.Docker;
using Elekta.Envctl.Models.Helm;
using Elekta.Envctl.Models.Kubernetes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Elekta.Envctl
{
    class Program
    {
        const string rootPath = "C:\\Elekta\\elekta-platform-sdk";
        const string elementsPath = "scripts\\devctl.d\\elekta-platform-elements";

        static async Task<int> Main(string[] args)
        {
            var output = new OutputModel(new ShortFormat());
            var tasks = new List<Task>
            {
                GetOutputForProcessAsync("docker", "version")
                    .ToObject<DockerVersion, DirtyDockerVersionSerialiser>()
                    .UpdateAsync(output),
                GetOutputForProcessAsync("kubectl", "version")
                    .ToObject<KubernetesVersion, KubernetesVersionSerialiser>()
                    .UpdateAsync(output),
                GetChartVersionsAsync().UpdateAsync(output)
            };

            await output.UpdateAsync(tasks);

            Console.WriteLine(output);
            Console.ReadLine();
            return 0;
        }

        private static async Task<ProcessOutput> GetOutputForProcessAsync(string fileName, string arguments)
        {
            return await Task.Run(() =>
            {
                var output = new ProcessOutput();
                var stdout = new StringBuilder();
                var stderr = new StringBuilder();
                try
                {
                    using var process = new Process
                    {
                        StartInfo = new ProcessStartInfo(fileName, arguments)
                        {
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false
                        }
                    };

                    process.Start();
                    while (!process.HasExited)
                    {
                        stdout.Append(process.StandardOutput.ReadToEnd());
                        stderr.Append(process.StandardError.ReadToEnd());
                    }
                    output.Data = stdout.ToString();
                    output.Error = stderr.ToString();
                }
                catch (Exception ex)
                {
                    output.Error += " " + ex.Message;
                }
                return output;
            });
        }

        private static async Task<IList<HelmChart>> GetChartVersionsAsync()
        {
            return await Task.Run(() =>
            {
                var path = Path.Combine(rootPath, elementsPath);
                var charts = new List<HelmChart>();

                foreach (var file in Directory.GetFiles(path, "*.yaml"))
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
            });
        }
    }
}
