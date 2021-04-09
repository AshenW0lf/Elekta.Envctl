using Elekta.Envctl.Models;
using System.Text;

namespace Elekta.Envctl.Formatter
{
    public class ShortFormat : IFormat
    {
        public string Format(OutputModel model)
        {
            var builder = new StringBuilder();
            builder.AppendLine("==== Kubernetes ====");
            builder.Append("Client:\t");
            builder.AppendLine(model.KubernetesVersion.Client?.FullVersion ?? "--- Failed to get Data ---");
            builder.Append("Server:\t");
            builder.AppendLine(model.KubernetesVersion.Server?.FullVersion ?? "--- Failed to get Data ---");

            builder.AppendLine();

            if (model.KubernetesVersion.HasErrors)
            {
                builder.Append("Errors:\t");
                builder.AppendLine(model.KubernetesVersion.Errors);
                builder.AppendLine();
            }

            builder.AppendLine("==== Helm Charts ====");
            foreach (var helmChart in model.HelmCharts)
            {
                builder.Append("Name:\t");
                builder.AppendLine(helmChart.Spec.Chart.Name);

                builder.Append("Version:\t");
                builder.AppendLine(helmChart.Spec.Chart.Version);
            }

            return builder.ToString();
        }
    }
}
