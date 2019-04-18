using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using DiscordBotBase.API;
using System;

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

                    await ReplyAsync($"```json{Environment.NewLine}{response.Truncate(1800)}```");
                }
                catch
                {
                    await ReplyAsync($"Sorry, I can't let you do that **{Context.User.Username}**");
                }
            }

            [Command("get challenges"), Alias("gcha")]
            [Summary("Fetches information about a supplied user's challenges from codewars")]
            [RequireUserPermission(GuildPermission.SendMessages)]
            public async Task ChallengesValuesAsync([Remainder]string text)
            {
                try
                {
                    var response = utility.ExecuteRequest(CodeWarsUri, Version, $"/users/{text}/code-challenges/completed?page=0", RestSharp.Method.GET);

                    await ReplyAsync($"```json{Environment.NewLine}{response.Truncate(1800)}```");
                }
                catch
                {
                    await ReplyAsync($"Sorry, I can't let you do that **{Context.User.Username}**");
                }
            }

            [Command("get challenge"), Alias("gscha")]
            [Summary("Fetches information about a supplied user's challenges from codewars")]
            [RequireUserPermission(GuildPermission.SendMessages)]
            public async Task ChallengeValuesAsync([Remainder]string text)
            {
                try
                {
                    var response = utility.ExecuteRequest(CodeWarsUri, Version, $"/code-challenges/{text}", RestSharp.Method.GET);

                    await ReplyAsync($"```json{Environment.NewLine}{response.Truncate(1800)}```");
                }
                catch
                {
                    await ReplyAsync($"Sorry, I can't let you do that **{Context.User.Username}**");
                }
            }

            /* TODO: Refactor, create a clean reponse class (parse and output), add webhook, add auth specifically to this bot (register bot account on cw?) */
        }
    }
}
