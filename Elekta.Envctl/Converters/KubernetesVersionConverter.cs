using Elekta.Envctl.Models.Kubernetes;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Elekta.Envctl.Converters
{
    public static class KubernetesVersionConverter
    {
        const string _client = "Client Version: version.Info";
        const string _server = "Server Version: version.Info";
        public static KubernetesVersion DeserialiseKubernetesVersion(this TextReader reader)
        {
            var version = new KubernetesVersion();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith(_client))
                {
                    version.Client = JsonConvert.DeserializeObject<VersionInfo>(line.Remove(0, _client.Length));
                }
                else if (line.StartsWith(_server))
                {
                    version.Server = JsonConvert.DeserializeObject<VersionInfo>(line.Remove(0, _server.Length));
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            return version;
        }
    }
}
