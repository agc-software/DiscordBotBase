using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using DiscordBotBase.API;
using System;
using System.Collections.Generic;

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
                    user ??= Context.User;
                    await ReplyAsync($"```Name: {user.Username}#{user.Discriminator} ID: {user.Id}{Environment.NewLine}Created: {user.CreatedAt}```");
                }
                catch
                {
                    await ReplyAsync($"Sorry, I can't let you do that **{Context.User.Username}**");
                }
            }

            /* TODO: Refactor, Add mute, etc.. */
        }
    }
}
