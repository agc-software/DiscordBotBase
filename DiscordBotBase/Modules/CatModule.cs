using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using DiscordBotBase.API;
using System;
using Newtonsoft.Json.Linq;

/* Don't use this in production, it's for references only 
 Use this as examples: https://github.com/Aux/Discord.Net-Example/tree/2.0/src/Modules */

namespace DiscordBotBase.Modules
{
    public class CatModule : ModuleBase<SocketCommandContext>
    {
        private readonly Utility utility = new Utility();
        private readonly Uri catBaseUrl = new Uri("https://api.thecatapi.com/");
        private readonly string version = "v1";

        [Group("cat"), Name("Cats")]
        [RequireContext(ContextType.Guild)]
        public class Cat : CatModule
        {
            [Command("random"), Alias("rc")]
            [Summary("Displays a random cat picture")]
            [RequireUserPermission(GuildPermission.SendMessages)]
            [RequireBotPermission(GuildPermission.SendMessages)]
            public async Task DisplayCatAsync()
            {
                try
                {
                    var response = utility.ExecuteRequest(catBaseUrl, version, "/images/search", RestSharp.Method.GET);
                    var parseJArrayImage = JArray.Parse(response).SelectToken("$[*].url");
                    await ReplyAsync($"{parseJArrayImage}");
                }
                catch(Exception ex)
                {
                    await ReplyAsync($"{ex}");
                }
            }

            /* TODO: Refactor, MORE CATS!, Probably change this to CAATAS instead... */
        }
    }
}
