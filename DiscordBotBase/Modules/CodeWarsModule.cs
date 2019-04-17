using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using DiscordBotBase.API;
using System;
using System.Collections.Generic;

/* Don't use this in production, it's for references only 
 Use this as examples: https://github.com/Aux/Discord.Net-Example/tree/2.0/src/Modules */

namespace DiscordBotBase.Modules
{
    public class CodeWarsModule : ModuleBase<SocketCommandContext>
    {
        private readonly Utility utility = new Utility();
        private readonly Uri CodeWarsUri = new Uri("https://www.codewars.com/api/");
        private readonly string Version = "v1";

        [Group("cw"), Name("CodeWars")]
        [RequireContext(ContextType.Guild)]
        public class CodeWars : CodeWarsModule
        {
            [Command("get user"), Alias("gusr")]
            [Summary("Fetches information about a supplied user from codewars")]
            [RequireUserPermission(GuildPermission.SendMessages)]
            public async Task UserValuesAsync([Remainder]string text)
            {
                try
                {
                    var response = utility.ExecuteRequest(CodeWarsUri, Version, $"/users/{text}", RestSharp.Method.GET);

                    await ReplyAsync($"```json{Environment.NewLine}{response}```");
                }
                catch
                {
                    await ReplyAsync($"`Sorry something went wrong!`");
                }
            }
        }
    }
}
