using Microcharts;
using Microcharts.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CursValutar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPage : ContentPage
    {
        public ContactPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string uri = "https://github.com/andreeaawww/Fotball-App-SIMPRE?fbclid=IwAR0ea7UagbJ-C6lyqpSVtTkq9RDN6aIe-HtH8_vQhwjsqufaJsXTGRNoZA4";

            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
    }
}
