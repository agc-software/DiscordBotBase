using System.Collections.Generic;

namespace DiscordBotBase.DAL
{
    public partial class CodeWarsUser
    {

        public string Username { get; set; }
        public string Name { get; set; }
        public long Honor { get; set; }
        public string Clan { get; set; }
        public long LeaderboardPosition { get; set; }
        public List<string> Skills { get; set; }
        public Ranks Ranks { get; set; }
        public CodeChallenges CodeChallenges { get; set; }
    }

    public partial class CodeChallenges
    {
        public long TotalAuthored { get; set; }
        public long TotalCompleted { get; set; }
    }

    public partial class Ranks
    {
        public Overall Overall { get; set; }
        public Dictionary<string, Overall> Languages { get; set; }
    }

    public partial class Overall
    {
        public long Rank { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }
        public long Score { get; set; }
    }

    public enum Color { Blue, White, Yellow };

}