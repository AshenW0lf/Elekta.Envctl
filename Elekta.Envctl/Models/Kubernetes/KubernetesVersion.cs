namespace Elekta.Envctl.Models.Kubernetes
{
    public class KubernetesVersion
    {
        public VersionInfo Client { get; set; }
        public VersionInfo Server { get; set; }
    }
}
