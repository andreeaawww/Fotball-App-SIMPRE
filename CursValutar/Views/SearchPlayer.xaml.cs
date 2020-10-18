using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CursValutar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPlayer : ContentPage
    {
        public SearchPlayer()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            PlayerDetails playerDetails = new PlayerDetails(PlayerNameInput.Text);

            Navigation.PushAsync(playerDetails);
        }
    }
}