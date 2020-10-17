namespace CursValutar
{
    public class Team
    {
        public string teamKey { get; set; }
        public string teamName { get; set; }
        public string teamBadgeURL { get; set; }
        public Team(string teamKey, string teamName, string teamBadgeURL) {
            this.teamKey = teamKey;
            this.teamName = teamName;
            this.teamBadgeURL = teamBadgeURL;
        }
    }
}