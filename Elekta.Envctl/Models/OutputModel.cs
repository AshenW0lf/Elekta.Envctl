using Elekta.Envctl.Models.Docker;
using Elekta.Envctl.Models.Helm;
using Elekta.Envctl.Models.Kubernetes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elekta.Envctl.Models
{
    public class OutputModel
    {
        private readonly IFormat _formatter;

        public OutputModel(IFormat formatter)
        {
            _formatter = formatter;
        }

        public DockerVersion DockerVersion { get; set; }
        public KubernetesVersion KubernetesVersion { get; set; }
        public IList<HelmChart> HelmCharts { get; set; }

        public override string ToString()
        {
            return _formatter.Format(this);
        }

        public async Task UpdateAsync(List<Task> tasks)
        {
            await Task.WhenAll(tasks);
        }
    }
}
