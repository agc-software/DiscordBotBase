using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using DiscordBotBase.API;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Discord.WebSocket;

/* Don't use this in production, it's for references only 
 Use this as examples: https://github.com/Aux/Discord.Net-Example/tree/2.0/src/Modules */

namespace DiscordBotBase.Modules
{
    public class ModerationModule : ModuleBase<SocketCommandContext>
    {
        [Group("mod"), Name("Moderation")]
        [RequireContext(ContextType.Guild)]
        public class Moderation : ModerationModule
        {
            [Command("clear chat"), Alias("cc")]
            [Summary("Clear the chat with a specified amount")]
            [RequireUserPermission(GuildPermission.ManageMessages)]
            [RequireBotPermission(GuildPermission.ManageMessages)]
            public async Task ClearChatAsync(int amount)
            {
                try
                {
                    IEnumerable<IMessage> eMessages = await Context.Channel.GetMessagesAsync(amount + 1).FlattenAsync();
                    await ((ITextChannel)Context.Channel).DeleteMessagesAsync(eMessages);
                    await ReplyAsync($"Deleted: {amount} messages");
                }
                catch
                {
                    await ReplyAsync($"Sorry, I can't let you do that **{Context.User.Username}**");
                }
            }

            [Command("kick"), Alias("k")]
            [Summary("Kicks the specified user")]
            [RequireUserPermission(GuildPermission.KickMembers)]
            [RequireBotPermission(GuildPermission.KickMembers)]
            public async Task KickUserAsync(IGuildUser user, [Remainder] string optionalReason = null)
            {
                try
                {
                    await user.KickAsync(reason: optionalReason);
                    await ReplyAsync($"Kicked user: {user.Nickname} goodbye!");
                }
                catch
                {
                    await ReplyAsync($"Sorry, I can't let you do that **{Context.User.Username}**");
                }
            }

            [Command("ban"), Alias("b")]
            [Summary("Bans the specified user")]
            [RequireUserPermission(GuildPermission.BanMembers)]
            [RequireBotPermission(GuildPermission.BanMembers)]
            public async Task BanUserAsync(IGuildUser user, [Remainder] string optionalReason = null)
            {
                try
                {
                    await user.BanAsync(7, reason: optionalReason);
                    await ReplyAsync($"Banned user: {user.Nickname} see you never!");
                }
                catch
                {
                    await ReplyAsync($"Sorry, I can't let you do that **{Context.User.Username}**");
                }
            }

            [Command("info"), Alias("i")]
            [Summary("Performs a lookup on the specified user")]
            [RequireUserPermission(GuildPermission.SendMessages)]
            [RequireBotPermission(GuildPermission.SendMessages)]
            public async Task UserInfoAsync(IUser user = null)
            {
                try
                {
                    user = Context.User;
                    await ReplyAsync($"```Name: {user.Username}#{user.Discriminator} ID: {user.Id}{Environment.NewLine}Created: {user.CreatedAt}```");
                }
                catch
                {
                    await ReplyAsync($"Sorry, I can't let you do that **{Context.User.Username}**");
                }
            }

            [Command("mute"), Alias("m")]
            [Summary("Mutes specified user by adding them to role 'muted'")]
            [RequireUserPermission(GuildPermission.MuteMembers)]
            [RequireBotPermission(GuildPermission.MuteMembers)]
            public async Task MuteUserAsync(SocketGuildUser user, double timeToMute = 5.0, [Remainder] string optionalReason = null)
            {
                try
                {
                    var mutedRole = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToLower() == "muted" || x.Name.ToLower() == "mute");

                    await user.AddRoleAsync(mutedRole);
                    await ReplyAsync($"**{user.Username}** has been muted for {timeToMute} minutes");

                    var stopwatchStart = Stopwatch.StartNew();
                    while (stopwatchStart.ElapsedMilliseconds < timeToMute * 60000)
                    {
                        if (stopwatchStart.ElapsedMilliseconds >= timeToMute * 60000)
                            break;
                    }

                    await user.RemoveRoleAsync(mutedRole);
                    await ReplyAsync($"**{user.Username}** has now been un-muted");
                }
                catch
                {
                    await ReplyAsync($"Sorry, I can't let you do that **{Context.User.Username}**");
                }
            }

            /* TODO: Refactor, etc.. */
        }
    }
}
