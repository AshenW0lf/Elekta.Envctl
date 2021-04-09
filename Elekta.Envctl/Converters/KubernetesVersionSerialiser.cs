using Elekta.Envctl.Models;
using Elekta.Envctl.Models.Kubernetes;
using Newtonsoft.Json;
using System.IO;

namespace Elekta.Envctl.Converters
{
    public  class KubernetesVersionSerialiser : ISerialiser<KubernetesVersion>
    {
        const string _client = "Client Version: version.Info";
        const string _server = "Server Version: version.Info";

        public KubernetesVersion Deserialise(ProcessOutput output)
        {
            var reader = new StringReader(output.Data);
            var version = new KubernetesVersion(output.Error);
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
            }
            return version;
        }
    }
}
