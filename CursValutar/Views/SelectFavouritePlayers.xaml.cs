using CursValutar.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CursValutar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectFavouritePlayers : ContentPage
    {
        private readonly DBUtils dbUtils;

        public SelectFavouritePlayers()
        {
            InitializeComponent();

            dbUtils = new DBUtils();
            dbUtils.InitializeDb();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var playerName = PlayerNameInput.Text;

            if (!playerName.HasValue())
            {
                DisplayAlert("Error!", "Please enter a player name!", "OK");
            }
            else
            {
                var apiService = new ApiService();
                var playerData = apiService.GetPlayerData(playerName.ToLower());

                if (playerData.StatusCode != HttpStatusCode.OK)
                {
                     DisplayAlert("Server error", "A server error has occured! Try again!", "OK");
                }
                else
                {
                    try
                    {
                        var item = JsonConvert.DeserializeObject<JArray>(playerData.Content);

                        var player = new Player
                        {
                            PlayerName = item[0]["player_name"].ToString(),
                            Age = int.Parse(item[0]["player_age"].ToString()),
                            Nationality = item[0]["player_country"].ToString(),
                            TeamName = item[0]["team_name"].ToString()
                        };

                        var savedPlayers = dbUtils.GetSavedPlayers();

                        if (savedPlayers.Any(x => x.PlayerName == player.PlayerName))
                        {
                            player.PlayerId = savedPlayers.Where(x => x.PlayerName == player.PlayerName).Select(x => x.PlayerId).FirstOrDefault();
                            dbUtils.UpdatePlayerData(player);
                        }
                        else
                        {
                            dbUtils.AddPlayer(player);
                        }

                        DisplayAlert("Success!", "Player's data has been saved/updated.", "OK");
                    } catch(Exception error)
                    {
                        DisplayAlert("Error!", "The player could not be found!", "OK");
                    }
                }
            }
        }
    }
}