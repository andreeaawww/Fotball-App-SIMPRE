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
    public partial class CountryPage : ContentPage
    {
        private readonly ApiService apiService;
        private List<string> Countries = new List<string>();

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
            //DisplayAlert("dialog", "hei lucian");
        }

        private async Task InitializeCountriesAsync()
        {
            try
            {
                var response = apiService.GetCountries();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    await DisplayAlert("Ups! We hit an error", "Server is not responding", "Retry");

                    new CountryPage();
                }
                else 
                {
                    foreach (var country in JsonConvert.DeserializeObject<JArray>(response.Content))
                    {
                        Countries.Add(country["country_name"].ToString());
                    }
                }
            }catch(Exception e)
            {
                await DisplayAlert("Ups! We hit an error", e.Message.ToString(), "OK");
            }
        }
    }
}