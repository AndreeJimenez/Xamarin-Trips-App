using AppTrips.Models;
using AppTrips.Services;
using System;
using System.Collections.Generic;
using System.IO;
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

            tripSelected.ImageUrl = new ImageService().SaveImageFromBase64(tripSelected.ImageUrl);
            MapTrip.Trip = tripSelected;

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