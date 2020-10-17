using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CursValutar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SavedPlayers : ContentPage
    {
        private readonly DBUtils dbUtils;

        public SavedPlayers()
        {
            InitializeComponent();
            dbUtils = new DBUtils();
        }

        protected override void OnAppearing()
        {
            var players = new List<Player>();

            players = dbUtils.GetSavedPlayers();

            var playersDisplay = new List<string>();

            foreach(var player in players)
            {
                playersDisplay.Add(player.ToString());
            }

            savedPlayersListView.ItemsSource = playersDisplay;
        }
    }
}