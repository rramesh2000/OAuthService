using Serilog.Core;

namespace Application.Common.Interfaces
{
    public interface ITSLogger
    {
        Logger Log { get; set; }
    }
}