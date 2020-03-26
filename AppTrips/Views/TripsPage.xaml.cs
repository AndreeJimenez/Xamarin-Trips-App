using AppTrips.Models;
using AppTrips.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTrips.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TripsPage : ContentPage
    {
        public TripsPage()
        {
            InitializeComponent();

            BindingContext = new TripsViewModel();

            /*List<TripModel> trips = new List<TripModel>
            {
                new TripModel
                {
                    Title = "Nueva York",
                    Rating = 4,
                    Notes = "Estatua de la libertad",
                    TripDate = new DateTime(2020, 2, 18),
                    Latitude = 23.45453675,
                    Longitude = -12.454666
                },
                new TripModel
                {
                    Title = "Paris",
                    Rating = 5,
                    Notes = "Torre Eiffel",
                    TripDate = new DateTime(2019, 12, 24),
                    Latitude = 31.3556537,
                    Longitude = -16.354666
                },
                new TripModel
                {
                    Title = "Checoslovaquia",
                    Rating = 3,
                    Notes = "Checoslovacos",
                    TripDate = new DateTime(2017, 5, 10),
                    Latitude = 19.34665653,
                    Longitude = -10.565657890
                }
            };

            TripsColView.ItemsSource = trips;*/
        }

        /*private void NewItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TripDetailPage());
        }

        private async void TripsColView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var trip = e.CurrentSelection.FirstOrDefault();
            if (trip != null)
            {
                await Navigation.PushAsync(new TripDetailPage(trip as TripModel));
            }
            TripsColView.SelectedItem = null;
        }*/
    }
}