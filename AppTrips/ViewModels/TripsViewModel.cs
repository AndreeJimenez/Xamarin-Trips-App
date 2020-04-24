using AppTrips.Models;
using AppTrips.Services;
using AppTrips.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace AppTrips.ViewModels
{
    public class TripsViewModel : BaseViewModel
    {
        static TripsViewModel _instance;

        Command refreshCommand;
        public Command RefreshCommand => refreshCommand ?? (refreshCommand = new Command(RefreshTrips));

        Command _newCommand;
        public Command NewCommand => _newCommand ?? (_newCommand = new Command(NewAction));

        Command _selectCommand;
        public Command SelectCommand => _selectCommand ?? (_selectCommand = new Command(SelectAction));

        TripModel tripSelected;
        public TripModel TripSelected
        {
            get => tripSelected;
            set => SetProperty(ref tripSelected, value);
        }

        ObservableCollection<TripModel> _Trips;
        public ObservableCollection<TripModel> Trips 
        { 
            get => _Trips; 
            set => SetProperty(ref _Trips, value); 
        }

        public TripsViewModel()
        {
            _instance = this;
            LoadTrips();
        }

        private async void LoadTrips()
        {
            ApiResponse response = await new ApiService().GetDataAsync<TripModel>("trips");
            if (response == null || !response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error loading trips", response.Message, "Ok");
                return;
            }
            Trips = (ObservableCollection<TripModel>)response.Result;

            IsBusy = false;
        }

        public static TripsViewModel GetInstance()
        {
            if (_instance == null) _instance = new TripsViewModel();
            return _instance;
        }

        private void NewAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new TripDetailPage());
        }

        private void SelectAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new TripDetailPage(TripSelected));
        }

        public void RefreshTrips()
        {
            LoadTrips();
        }
    }
}
