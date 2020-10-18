namespace CursValutar
{
    public class TeamPlayer
    {
        public string playerKey { get; set; }
        public string playerName { get; set; }
        public string playerNumber { get; set; }

        public string playerCountry { get; set; }

        public string playerType { get; set; }
        public string playerAge { get; set; }
        public string playerMatchPlayed { get; set; }

        public TeamPlayer(string playerKey, string playerName, string playerNumber)
        {
            this.playerKey = playerKey;
            this.playerName = playerName;
            this.playerNumber = playerNumber;
        }

        public TeamPlayer(string playerKey, string playerName, string playerNumber,
                          string playerCountry, string playerType, string playerAge,
                          string playerMatchPlayed)
        {
            this.playerKey = playerKey;
            this.playerName = playerName;
            this.playerNumber = playerNumber;
            this.playerCountry = playerCountry;
            this.playerType = playerType;
            this.playerAge = playerAge;
            this.playerMatchPlayed = playerMatchPlayed;
        }
        public TeamPlayer() { }
    }
}
