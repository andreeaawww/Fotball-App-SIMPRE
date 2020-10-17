using System.Collections.Generic;

namespace CursValutar
{
    public class Team
    {
        public string teamKey { get; set; }
        public string teamName { get; set; }
        public string teamBadgeURL { get; set; }
        private List<TeamPlayer> teamPlayers { get; set; }
        public List<TeamPlayer> getListOfPlayers() {
            return this.teamPlayers;
        }
        public Team(string teamKey, string teamName, string teamBadgeURL) {
            this.teamKey = teamKey;
            this.teamName = teamName;
            this.teamBadgeURL = teamBadgeURL;
        }

        public Team(string teamKey, string teamName, string teamBadgeURL, List<TeamPlayer> teamPlayers)
        {
            this.teamKey = teamKey;
            this.teamName = teamName;
            this.teamBadgeURL = teamBadgeURL;
            this.teamPlayers = teamPlayers;
        }

        public Team() {
        }
    }
}