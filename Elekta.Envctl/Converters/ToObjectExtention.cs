using Elekta.Envctl.Models;
using Elekta.Envctl.Models.Docker;
using Elekta.Envctl.Models.Helm;
using Elekta.Envctl.Models.Kubernetes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elekta.Envctl.Converters
{
    public static class ToObjectExtention
    {
        public static async Task<TOut> ToObject<TOut, TSerialiser>(this Task<ProcessOutput> task)
            where TSerialiser : ISerialiser<TOut>, new()
        {
            var output = await task.ConfigureAwait(false);
            var serialiser = Activator.CreateInstance<TSerialiser>();
            return serialiser.Deserialise(output);
        }

        public static async Task UpdateAsync<T>(this Task<T> task, OutputModel output)
        {
            await task.ConfigureAwait(false);
            switch (task.Result)
            {
                case DockerVersion docker:
                    output.DockerVersion = docker;
                    break;
                case KubernetesVersion kubernetes:
                    output.KubernetesVersion = kubernetes;
                    break;
                case IList<HelmChart> helm:
                    output.HelmCharts = helm;
                    break;
            }
        }
    }
}
