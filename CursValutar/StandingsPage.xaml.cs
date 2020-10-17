using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CursValutar.Services;
using CursValutar.ValueObjects;
using Microcharts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CursValutar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StandingsPage : ContentPage
    {
        private readonly ApiService apiService;
        string leagueId;

        public StandingsPage()
        {
            InitializeComponent();

            apiService = new ApiService();
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            string leagueName = string.Empty;
            var standingsList = new List<Standing>();

            if (selectedIndex != -1)
            {
                leagueName = (string)picker.ItemsSource[selectedIndex];
            }

            switch (leagueName)
            {
                case "Premier League":
                    leagueId = "148";
                    break;
                case "Ligue 1":
                    leagueId = "176";
                    break;
                case "Bundesliga":
                    leagueId = "195";
                    break;
                case "La Liga":
                    leagueId = "468";
                    break;
                case "Serie A":
                    leagueId = "262";
                    break;
            }

            var standingsData = apiService.GetStandings(leagueId);

            if (standingsData.StatusCode != HttpStatusCode.OK)
            {
                DisplayAlert("Server Error", "An error has occured...", "Ok");
            }
            else
            {
                var standings = JsonConvert.DeserializeObject<JArray>(standingsData.Content);

                foreach (var standing in standings)
                {
                    standingsList.Add(new Standing()
                    {
                        TeamName = standing["team_name"].ToString(),
                        NumberOfPoints = Int32.Parse(standing["overall_league_PTS"].ToString())
                    });
                }
            }

            DrawGraph(standingsList);
        }

        void DrawGraph(List<Standing> standingsList)
        {
            var graphValues = new List<ChartEntry>();

            foreach (var entry in standingsList)
            {
                var chartEntry = new ChartEntry(entry.NumberOfPoints)
                {
                    Color = new SkiaSharp.SKColor(120, 0, 120),
                    Label = entry.TeamName,
                    ValueLabel = entry.NumberOfPoints.ToString()
                };

                graphValues.Add(chartEntry);
            }

            if(ChartViewStandings.Chart != null)
            {
                ChartViewStandings.Chart = null;
            }

            ChartViewStandings.Chart = new BarChart() { Entries = graphValues };
        }
    }
}