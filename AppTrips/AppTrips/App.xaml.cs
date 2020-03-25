using AppTrips.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTrips
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TripsPage());
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
