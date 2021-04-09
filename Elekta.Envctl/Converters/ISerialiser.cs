using Elekta.Envctl.Models;

namespace Elekta.Envctl.Converters
{
    public interface ISerialiser<out T>
    {
        T Deserialise(ProcessOutput output);
    }
}
