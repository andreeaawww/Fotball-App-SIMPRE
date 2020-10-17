using CursValutar.Services;
using CursValutar.Views;
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
    public partial class CountryPage : ContentPage
    {
        private readonly ApiService apiService;
        private List<string> Countries = new List<string>();
        private Dictionary<string, int> CountriesIds = new Dictionary<string, int>();

        public CountryPage()
        {
            InitializeComponent();

            apiService = new ApiService();

            InitializeCountriesAsync();

            countriesListView.ItemsSource = Countries;

            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        
    private void ViewLeaguesButtonHandler(object sender, EventArgs e)
        {
            var country = countriesListView.SelectedItem.ToString();

            var CountryId = CountriesIds[country].ToString();

            var leaguePage = new LeaguesPage(CountryId);

            Navigation.PushAsync(leaguePage);
        }

        private void SelectedItem(object sender, EventArgs e)
        {
            var country = countriesListView.SelectedItem.ToString();

            var CountryId = CountriesIds[country].ToString();

            var leaguePage = new LeaguesPage(CountryId);

            Navigation.PushAsync(leaguePage);
        }

        private async Task InitializeCountriesAsync()
        {
            try
            {
                var response = apiService.GetCountries();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                   // await DisplayAlert("Ups! We hit an error", "Server is not responding", "Retry");

                    do
                    {
                        new CountryPage();

                        response = apiService.GetCountries();

                    } while (response.StatusCode == HttpStatusCode.OK);

                    if(response.StatusCode == HttpStatusCode.OK)
                        foreach (var country in JsonConvert.DeserializeObject<JArray>(response.Content))
                        {
                            Countries.Add(country["country_name"].ToString());
                            CountriesIds.Add(country["country_name"].ToString(), Int32.Parse(country["country_id"].ToString()));
                        }

                }
                else 
                {
                    foreach (var country in JsonConvert.DeserializeObject<JArray>(response.Content))
                    {
                        Countries.Add(country["country_name"].ToString());
                        CountriesIds.Add(country["country_name"].ToString(), Int32.Parse(country["country_id"].ToString()));
                    }
                }
            }catch(Exception e)
            {
                await DisplayAlert("Ups! We hit an error", e.Message.ToString(), "OK");
            }
        }
    }
}