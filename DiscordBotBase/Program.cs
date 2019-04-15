using System.Threading.Tasks;

namespace DiscordBotBase
{
    class Program
    {
        static Task Main(string[] args)
            => Startup.RunAsync(args);
    }
}
