using AppTrips.Models;
using AppTrips.Services;
using AppTrips.Views;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppTrips.ViewModels
{
    public class TripDetailViewModel : BaseViewModel
    {
        private int id;

        Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(SaveAction));

        Command _deleteCommand;
        public Command DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(DeleteAction));

        Command _GetLocationCommand;
        public Command GetLocationCommand => _GetLocationCommand ?? (_GetLocationCommand = new Command(GetLocationAction));

        Command _MapCommand;
        public Command MapCommand => _MapCommand ?? (_MapCommand = new Command(MapAction));

        Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));

        Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));

        string _Title;
        public string Title 
        { 
            get => _Title;
            set => SetProperty(ref _Title, value); 
        }

        string _Notes;
        public string Notes
        {
            get => _Notes;
            set => SetProperty(ref _Notes, value);
        }

        DateTime _TripDate;
        public DateTime TripDate
        {
            get => _TripDate;
            set => SetProperty(ref _TripDate, value);
        }

        double _Latitude;
        public double Latitude
        {
            get => _Latitude;
            set => SetProperty(ref _Latitude, value);
        }

        double _Longitude;
        public double Longitude
        {
            get => _Longitude;
            set => SetProperty(ref _Longitude, value);
        }

        int _Rating;
        public int Rating
        {
            get => _Rating;
            set => SetProperty(ref _Rating, value);
        }

        string _ImageUrl;
        public string ImageUrl
        {
            get => _ImageUrl;
            set => SetProperty(ref _ImageUrl, value);
        }

        public TripDetailViewModel()
        {
        }

        public TripDetailViewModel(TripModel trip)
        {
            if (trip != null)
            {
                id = trip.ID;
                Title = trip.Title;
                Notes = trip.Notes;
                Latitude = trip.Latitude;
                Longitude = trip.Longitude;
                Rating = trip.Rating;
                TripDate = trip.TripDate;
                ImageUrl = trip.ImageUrl;
            }
        }

        private async void SaveAction()
        {
            IsBusy = true;
            if (id == 0)
            {
                ApiResponse response = await new ApiService().PostDataAsync("trips", new TripModel
                {
                    Title = this.Title,
                    Notes = this.Notes,
                    Latitude = this.Latitude,
                    Longitude = Longitude,
                    Rating = Rating,
                    TripDate = TripDate,
                    ImageUrl = ImageUrl
                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("AppTrips", "Error creating trip", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("AppTrips", response.Message, "Ok");
                    return;
                }
                TripsViewModel.GetInstance().RefreshTrips();
                await Application.Current.MainPage.DisplayAlert("AppTrips", response.Message, "Ok");
            }
            else
            {
                ApiResponse response = await new ApiService().PutDataAsync("trips", new TripModel
                {
                    ID = id,
                    Title = this.Title,
                    Notes = this.Notes,
                    Latitude = this.Latitude,
                    Longitude = Longitude,
                    Rating = Rating,
                    TripDate = TripDate,
                    ImageUrl = ImageUrl
                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("AppTrips", "Error updating trip", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("AppTrips", response.Message, "Ok");
                    return;
                }
                TripsViewModel.GetInstance().RefreshTrips();
                await Application.Current.MainPage.DisplayAlert("AppTrips", response.Message, "Ok");
            }

            IsBusy = false;
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void DeleteAction()
        {
            IsBusy = true;

            ApiResponse response = await new ApiService().DeleteDataAsync("trips", id);
            if (response == null)
            {
                await Application.Current.MainPage.DisplayAlert("AppTrips", "Error removing trip", "Ok");
                return;
            }
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("AppTrips", response.Message, "OK");
                return;
            }
            TripsViewModel.GetInstance().RefreshTrips();
            await Application.Current.MainPage.DisplayAlert("AppTrips", response.Message, "Ok");

            IsBusy = false;
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void MapAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new TripMapPage(new TripModel
            {
                Title = this.Title,
                Notes = this.Notes,
                Latitude = this.Latitude,
                Longitude = Longitude,
                Rating = Rating,
                TripDate = TripDate,
                ImageUrl = ImageUrl
            }));
        }

        private async void GetLocationAction()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Latitude = location.Latitude;
                    Longitude = location.Longitude;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        private async void TakePictureAction()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            ImageUrl = await new ImageService().ConvertImageFileToBase64(file.Path);
            await Application.Current.MainPage.DisplayAlert("File Location", file.Path, "OK");
        }

        private async void SelectPictureAction()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Not supported", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            if (file == null)
                return;

            ImageUrl = await new ImageService().ConvertImageFileToBase64(file.Path);
        }
    }
}
