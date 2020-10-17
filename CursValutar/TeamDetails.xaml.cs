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
    public partial class TeamDetails : ContentPage
    {

        private readonly ApiService apiService;
        private string CurrentTeam = "";
        // private Team CurrentTeam = new Team();

        public TeamDetails()
        {
            InitializeComponent();
        }

        public TeamDetails(string teamKey)
        {
 
            InitializeComponent();

            apiService = new ApiService();

            AsyncGetTeamDetails(teamKey);

            DisplayedTeam.BindingContext = CurrentTeam;
        }

       private async Task AsyncGetTeamDetails(string teamkey)
        {
            try
            {
                var response = apiService.GetTeamsDetailsByTeamId(teamkey);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    await DisplayAlert("Ups! We hit an error", "Server is not responding", "Retry");

                    do
                    {
                        new TeamDetails(teamkey);

                        response = apiService.GetTeamsDetailsByTeamId(teamkey);

                    } while (response.StatusCode != HttpStatusCode.OK);

                }

                var team = JsonConvert.DeserializeObject<JArray>(response.Content)[0];
               // var teamPlayers = new List<TeamPlayer>();

               /* CurrentTeam = new Team(
                   team["team_key"].ToString(),
                   team["team_name"].ToString(),
                   team["team_badge"].ToString(),
                   teamPlayers
                ) ;*/
            }
            catch (Exception e)
            {
                await DisplayAlert("Ups! We hit an error", e.Message.ToString(), "OK");
            }
        }

    }
}