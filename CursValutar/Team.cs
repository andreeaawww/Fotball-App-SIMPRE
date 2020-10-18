using System;
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
        public float averagePlayerAge { get; set; }

        public float totalMatchesPlayed { get; set; }

        public float totalGoalsScored { get; set; }

        public void computeTotalGoals()
        {
            int playerCount = this.teamPlayers.Count;
            if (playerCount == 0)
            {
                this.totalGoalsScored = 0;
                return;
            }
            foreach (TeamPlayer teamPlayer in this.teamPlayers)
            {
                this.totalGoalsScored += Int32.Parse(teamPlayer.playerGoals);
            }
        }

        public void computeTotalMatchPlayed()
        {
            int playerCount = this.teamPlayers.Count;
            if (playerCount == 0)
            {
                this.totalMatchesPlayed = 0;
                return;
            }
            foreach (TeamPlayer teamPlayer in this.teamPlayers)
            {
                this.totalMatchesPlayed += Int32.Parse(teamPlayer.playerMatchPlayed);
            }
        }
        public void computeAveragePlayerAge()
        {
            int playerCount = this.teamPlayers.Count;
            if(playerCount == 0)
            {
                this.averagePlayerAge = 0;
                return;
            }
            int sum = 0;
            foreach(TeamPlayer teamPlayer in this.teamPlayers)
            {
                sum += Int32.Parse(teamPlayer.playerAge);
            }
            this.averagePlayerAge = sum / playerCount;
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