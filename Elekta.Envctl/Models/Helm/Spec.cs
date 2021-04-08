namespace Elekta.Envctl.Models.Helm
{
    public class Spec
    {
        public Chart Chart { get; set; }
        public string ReleaseName { get; set; }
        public bool Wait { get; set; }
        public int Timeout { get; set; }
        public dynamic Values { get; set; }
    }
}