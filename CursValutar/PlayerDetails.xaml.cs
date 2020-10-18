using CursValutar.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public partial class PlayerDetails : ContentPage
    {
        private readonly ApiService apiService;
        private TeamPlayer CurrentPlayer = new TeamPlayer();
        public PlayerDetails()
        {
            InitializeComponent();
        }

        public PlayerDetails(string playerName)
        {

            InitializeComponent();

            apiService = new ApiService();

            AsyncGetPlayerData(playerName);

            DisplayedPlayer.BindingContext = CurrentPlayer;

        }

        private async Task AsyncGetPlayerData(string playerName)
        {
            try
            {
                var response = apiService.GetPlayerData(playerName);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    await DisplayAlert("Ups! We hit an error", "Server is not responding", "Retry");

                    do
                    {
                        new PlayerDetails(playerName);

                        response = apiService.GetPlayerData(playerName);

                    } while (response.StatusCode != HttpStatusCode.OK);

                }

                var player = JsonConvert.DeserializeObject<JArray>(response.Content)[0];

                CurrentPlayer = new TeamPlayer(
                    player["player_key"].ToString(),
                    player["player_name"].ToString(),
                    player["player_number"].ToString(),
                    player["player_country"].ToString(),
                    player["player_type"].ToString(),
                    player["player_age"].ToString(),
                    player["player_match_played"].ToString(),
                    player["player_goals"].ToString(),
                    player["player_yellow_cards"].ToString(),
                    player["player_red_cards"].ToString()
                );
            }
            catch (Exception e)
            {
                await DisplayAlert("Ups! We hit an error", e.Message.ToString(), "OK");
            }
        }
    }
}