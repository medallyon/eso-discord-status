using System.Diagnostics;

namespace DiscordStatus
{
    public interface IProcess
    {
        Process Process { get; }
        bool Exists { get; }
    }
}