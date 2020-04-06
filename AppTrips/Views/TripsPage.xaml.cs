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
        }
    }
}