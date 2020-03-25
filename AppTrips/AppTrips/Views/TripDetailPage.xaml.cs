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
    public partial class TripDetailPage : ContentPage
    {
        public TripDetailPage()
        {
            InitializeComponent();

            BindingContext = new TripDetailViewModel();
        }

        public TripDetailPage(TripModel trip)
        {
            InitializeComponent();

            BindingContext = new TripDetailViewModel(trip);
        }
    }
}