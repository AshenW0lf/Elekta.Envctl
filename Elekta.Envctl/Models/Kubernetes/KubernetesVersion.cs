namespace Elekta.Envctl.Models.Kubernetes
{
    public class KubernetesVersion
    {
        public KubernetesVersion(string errors)
        {
            Errors = errors;
        }
        public VersionInfo Client { get; set; }
        public VersionInfo Server { get; set; }
        public string Errors { get; set; }
        public bool HasErrors => !string.IsNullOrWhiteSpace(Errors);
    }
}
