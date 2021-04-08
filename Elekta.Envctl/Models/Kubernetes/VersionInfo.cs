using System;

namespace Elekta.Envctl.Models.Kubernetes
{
    public class VersionInfo
    {
        public string FullVersion => $"{Major}.{Minor}";
        public int Major { get; set; }
        public int Minor { get; set; }
        public string GitVersion { get; set; }
        public string GitCommit { get; set; }
        public string GitTreeState { get; set; }
        public DateTime BuildDate { get; set; }
        public string GoVersion { get; set; }
        public string Compiler { get; set; }
        public string Platform { get; set; }
    }
}