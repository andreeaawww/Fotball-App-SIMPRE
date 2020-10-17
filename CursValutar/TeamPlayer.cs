namespace CursValutar
{
    public class TeamPlayer
    {
        public string playerKey { get; set; }
        public string playerName { get; set; }
        public string playerNumber { get; set; }

        public TeamPlayer(string playerKey, string playerName, string playerNumber)
        {
            this.playerKey = playerKey;
            this.playerName = playerName;
            this.playerNumber = playerNumber;
        }
        public TeamPlayer() { }
    }
}
