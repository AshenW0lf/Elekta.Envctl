namespace Elekta.Envctl.Models.Helm
{
    public class HelmChart
    {
        public string ApiVersion { get; set; }
        public string Kind { get; set; }
        public Metadata Metadata { get; set; }
        public Spec Spec { get; set; }
    }
}
