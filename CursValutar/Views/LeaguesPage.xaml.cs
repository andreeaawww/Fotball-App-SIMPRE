using CursValutar.Models;
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

namespace CursValutar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeaguesPage : ContentPage
    {
        private readonly ApiService apiService;
        public List<League> Leagues = new List<League>();

        public LeaguesPage(string CountryId)
        {
            InitializeComponent();

            apiService = new ApiService();

            InitializeLeaguesListAsync(CountryId);

            leaguesListView.ItemsSource = Leagues;

        }

        private void SelectedItem(object sender, EventArgs e)
        {
            var league = (League)leaguesListView.SelectedItem;

            var leagueId = Leagues.Where(i => i.LeagueName == league.LeagueName).Select(i => i.LeagueId).FirstOrDefault();

            var teamsPage = new TeamsPage(leagueId);

            Navigation.PushAsync(teamsPage);
        }

        private async Task InitializeLeaguesListAsync(string CountryId)
        {
            try
            {
                var response = apiService.GetAllLeagues(Int32.Parse(CountryId));

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    do
                    {
                        new LeaguesPage(CountryId);

                        response = apiService.GetCountries();

                    } while (response.StatusCode == HttpStatusCode.OK);

                    if (response.StatusCode == HttpStatusCode.OK)
                        foreach (var league in JsonConvert.DeserializeObject<JArray>(response.Content))
                        {
                            Leagues.Add(new League()
                            {
                                LeagueName = league["league_name"].ToString(),
                                LeagueFlag = league["league_logo"].ToString(),
                                LeagueId = league["league_id"].ToString()
                            });
                        }

                }
                else
                {
                    foreach (var league in JsonConvert.DeserializeObject<JArray>(response.Content))
                    {
                        Leagues.Add(new League()
                        {
                            LeagueName = league["league_name"].ToString(),
                            LeagueFlag = league["league_logo"].ToString(),
                            LeagueId = league["league_id"].ToString()
                        });
                    }
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Ups! We hit an error", e.Message.ToString(), "OK");
            }
        }

    }
}