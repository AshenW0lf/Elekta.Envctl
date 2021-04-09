using Elekta.Envctl.Models;

namespace Elekta.Envctl
{
    public interface IFormat
    {
        string Format(OutputModel model);
    }
}