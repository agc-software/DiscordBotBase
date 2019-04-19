using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBotBase.Modules
{
    public class DiagnosticsModule : ModuleBase<SocketCommandContext>
    {
        [Group("diag"), Name("Diagnostics")]
        [RequireContext(ContextType.Guild)]
        public class Diagnostics : DiagnosticsModule
        {
            [Command("ping"), Alias("p")]
            [Summary("Measure latency")]
            [RequireUserPermission(GuildPermission.Administrator)]
            public async Task GetLatencyAsync()
            {
                try
                {
                    var currentLatency = Context.Client.Latency;
                    await ReplyAsync($"{currentLatency}");
                }
                catch
                {
                    await ReplyAsync($"Sorry, I can't let you do that **{Context.User.Username}**");
                }
            }
        }
    }
}
