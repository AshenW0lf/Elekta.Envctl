using YamlDotNet.Serialization;

namespace Elekta.Envctl.Models.Helm
{
    public class Annotations
    {
        [YamlMember(Alias = "devctl.io/depends-on", ApplyNamingConventions = false)]
        public string Dependents { get; set; }
    }
}
