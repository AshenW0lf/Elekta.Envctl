using System;

namespace Elekta.Envctl.Models.Docker
{
    public class Client
    {
        public string Version { get; set; }
        public string ApiVersion { get; set; }
        public string GoVersion { get; set; }
        public string GitCommit { get; set; }
        public DateTime Built { get; set; }
        public string OsArch { get; set; }
        public bool Experimental { get; set; }
    }
}