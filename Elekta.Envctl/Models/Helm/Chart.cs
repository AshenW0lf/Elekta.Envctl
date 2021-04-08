namespace Elekta.Envctl.Models.Helm
{
    public class Chart
    {
        public string Repository { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
    }
}