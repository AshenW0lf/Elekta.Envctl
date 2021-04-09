using Elekta.Envctl.Models.Docker;
using Elekta.Envctl.Models.Helm;
using Elekta.Envctl.Models.Kubernetes;
using System.Collections.Generic;

namespace Elekta.Envctl.Models
{
    public class OutputModel
    {
        public DockerVersion DockerVersion { get; set; }
        public KubernetesVersion KubernetesVersion { get; set; }
        public IList<HelmChart> HelmCharts { get; set; }

        public override string ToString()
        {
            return this.ToShortFormat();
        }
    }
}
