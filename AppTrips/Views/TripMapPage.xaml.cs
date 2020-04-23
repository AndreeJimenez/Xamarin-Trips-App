using AppTrips.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AppTrips.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TripMapPage : ContentPage
    {
        public TripMapPage(TripModel tripSelected)
        {
            InitializeComponent();

            MapTrip.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                new Position(tripSelected.Latitude, tripSelected.Longitude),
                Distance.FromMiles(.5)
            ));

            MapTrip.Pins.Add(
                new Pin
                {
                    Type = PinType.Place,
                    Label = tripSelected.Title,
                    Position = new Position(tripSelected.Latitude, tripSelected.Longitude)
                }
            );

            Title.Text = tripSelected.Title;
            Date.Text = tripSelected.TripDate.ToShortDateString();
            Rating.Text = $"{tripSelected.Rating} Estrellas";
            Notes.Text = tripSelected.Notes;
        }
    }
}