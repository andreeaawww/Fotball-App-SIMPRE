using CursValutar.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CursValutar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamsPage : ContentPage
    {
        private readonly ApiService apiService;
        private List<string> Teams = new List<string>();
        public TeamsPage()
        {
            InitializeComponent();
        }
        public TeamsPage(string leagueId)
        {
            InitializeComponent();


            DisplayAlert(leagueId, "hello", "helloooo");

            apiService = new ApiService();

            AsyncGetLeagueTeams(leagueId);

            teamsListView.ItemsSource = Teams;

        }

        private async Task AsyncGetLeagueTeams(string leagueId)
        {
            try
            {
                var response = apiService.GetTeamsByLeagueId(leagueId);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    await DisplayAlert("Ups! We hit an error", "Server is not responding", "Retry");

                    do
                    {
                        new TeamsPage(leagueId);

                        response = apiService.GetTeamsByLeagueId(leagueId);

                    } while (response.StatusCode != HttpStatusCode.OK);

                }
               
                foreach (var team in JsonConvert.DeserializeObject<JArray>(response.Content))
                {
                    // Teams.Add(Team(team_id, team_name)) -- teamId; teamName
                    Teams.Add(team["team_name"].ToString());
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Ups! We hit an error", e.Message.ToString(), "OK");
            }
        }

    }
}