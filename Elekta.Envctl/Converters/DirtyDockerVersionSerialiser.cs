using Elekta.Envctl.Models;
using Elekta.Envctl.Models.Docker;

namespace Elekta.Envctl.Converters
{
    public class DirtyDockerVersionSerialiser : ISerialiser<DockerVersion>
    {
        public DockerVersion Deserialise(ProcessOutput output)
        {
            return new DockerVersion();
            /*Type type = typeof(T);
            var obj = Activator.CreateInstance(type);
            string line;
            while ((line = stream.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            foreach (var line in stream. File.ReadLines(fileName))
            {
                var keyVal = line.Split('=');
                if (keyVal.Length != 2) continue;

                var prop = type.GetProperty(keyVal[0].Trim());
                if (prop != null)
                {
                    prop.SetValue(obj, Convert.ChangeType(keyVal[1], prop.PropertyType));
                }
            }
            return (T)obj;*/
        }
    }
}
