using CursValutar.Models;
using CursValutar.Services;
using CursValutar.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CursValutar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountryPage : ContentPage
    {
        private readonly ApiService apiService;
        private List<Country> Countries = new List<Country>();

        public CountryPage()
        {
            InitializeComponent();

            apiService = new ApiService();

            InitializeCountriesAsync();

            countriesListView.ItemsSource = Countries;
        }

        private void SelectedItem(object sender, EventArgs e)
        {
            var country = (Country)countriesListView.SelectedItem;

            var CountryId = Countries.Where(i => i.CountryName == country.CountryName).Select(i => i.CountryId).FirstOrDefault();

            var leaguePage = new LeaguesPage(CountryId);

            Navigation.PushAsync(leaguePage);
        }

        private async void InitializeCountriesAsync()
        {
            try
            {
                var response = apiService.GetCountries();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var answer = await DisplayAlert("Ups!Server is not responding!", "Do you wantt to retry?", "Yes", "No");

                    if (answer == true)
                    {
                        do
                        {
                            response = apiService.GetCountries();

                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                foreach (var country in JsonConvert.DeserializeObject<JArray>(response.Content))
                                {
                                    Countries.Add(new Country
                                    {
                                        CountryName = country["country_name"].ToString(),
                                        CountryId = country["country_id"].ToString(),
                                        CountryFlag = country["country_logo"].ToString()
                                    });
                                }

                                countriesListView.IsPullToRefreshEnabled = true;

                                countriesListView.RefreshCommand = new Command(() =>
                                {
                                    countriesListView.ItemsSource = Countries;
                                    countriesListView.IsRefreshing = false;
                                });

                                break;
                            }

                        } while (response.StatusCode == HttpStatusCode.OK);
                    }
                }
                else 
                {
                    foreach (var country in JsonConvert.DeserializeObject<JArray>(response.Content))
                    {
                        Countries.Add(new Country
                        {
                            CountryName = country["country_name"].ToString(),
                            CountryId = country["country_id"].ToString(),
                            CountryFlag = country["country_logo"].ToString()
                        });
                    }
                }
            }catch(Exception e)
            {
                await DisplayAlert("Ups! We hit an error", e.Message.ToString(), "OK");
            }
        }
    }
}