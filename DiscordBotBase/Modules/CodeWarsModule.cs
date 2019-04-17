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
        // This is ugly, i know!
        private readonly Utility utility = new Utility();

        [Command("cw get user"), Alias("cwgusr")]
        [Summary("Fetches information about a supplied user from codewars")]
        [RequireUserPermission(GuildPermission.SendMessages)]
        public async Task UserValuesAsync([Remainder]string text)
        {
            try
            {
                var Skills = string.Empty;
                var LanguageRank = string.Empty;

                var response = utility.ExecuteRequest($"{text}", "https://www.codewars.com/api/v1/users/", RestSharp.Method.GET);

                foreach (var item in response.Skills)
                {
                    Skills += $"{item}, ";
                }

                foreach (var item in response.Ranks.Languages)
                {
                    LanguageRank += $"{item.Key} : {item.Value.Name}({item.Value.Score}), ";
                }

                await ReplyAsync($"```Username: {response.Username}{Environment.NewLine}Honor: {response.Honor}{Environment.NewLine}CompletedChallenges: {response.CodeChallenges.TotalCompleted}{Environment.NewLine}" +
                    $"Skills: {Skills}{Environment.NewLine}LanguageRank: {LanguageRank}```");
            }
            catch
            {
                await ReplyAsync($"`Sorry something went wrong!`");
            }
        }

        [Group("set"), Name("Example")]
        [RequireContext(ContextType.Guild)]
        public class Set : ModuleBase
        {
            [Command("nick"), Priority(1)]
            [Summary("Change your nickname to the specified text")]
            [RequireUserPermission(GuildPermission.ChangeNickname)]
            public Task Nick([Remainder]string name)
                => Nick(Context.User as SocketGuildUser, name);

            [Command("nick"), Priority(0)]
            [Summary("Change another user's nickname to the specified text")]
            [RequireUserPermission(GuildPermission.ManageNicknames)]
            public async Task Nick(SocketGuildUser user, [Remainder]string name)
            {
                await user.ModifyAsync(x => x.Nickname = name);
                await ReplyAsync($"{user.Mention} I changed your name to **{name}**");
            }
        }
    }
}
