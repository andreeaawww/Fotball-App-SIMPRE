using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CursValutar
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            Routing.RegisterRoute("Setari", typeof(Setari));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
