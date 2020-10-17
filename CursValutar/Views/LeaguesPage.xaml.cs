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
        public List<string> Leagues = new List<string>();

        public LeaguesPage(string CountryId)
        {
            InitializeComponent();

            apiService = new ApiService();

            InitializeLeaguesListAsync(CountryId);

            leaguesListView.ItemsSource = Leagues;

        }

        private void SelectedItem(object sender, EventArgs e)
        {

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
                        new CountryPage();

                        response = apiService.GetCountries();

                    } while (response.StatusCode == HttpStatusCode.OK);

                    if (response.StatusCode == HttpStatusCode.OK)
                        foreach (var league in JsonConvert.DeserializeObject<JArray>(response.Content))
                        {
                            Leagues.Add(league["league_name"].ToString());
                        }

                }
                else
                {
                    foreach (var league in JsonConvert.DeserializeObject<JArray>(response.Content))
                    {
                        Leagues.Add(league["league_name"].ToString());
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