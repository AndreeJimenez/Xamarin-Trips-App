using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppTrips.Droid.Renders;
using AppTrips.Models;
using AppTrips.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace AppTrips.Droid.Renders
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        TripModel Trip;

        public CustomMapRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                this.Trip = ((CustomMap)e.NewElement).Trip;
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.SetInfoWindowAdapter(this);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            // return base.CreateMarker(pin);
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(Trip.Latitude, Trip.Longitude));
            marker.SetTitle(Trip.Title);
            marker.SetSnippet(Trip.Notes);
            return marker;
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if(inflater != null)
            {
                Android.Views.View view;
                view = inflater.Inflate(Resource.Layout.MarkerWindow, null);
                var infoImage = view.FindViewById<ImageView>(Resource.Id.MarkerWindowImage);
                var infoTitle = view.FindViewById<TextView>(Resource.Id.MarkerWindowTitle);
                var infoNotes = view.FindViewById<TextView>(Resource.Id.MarkerWindowNotes);

                if (infoImage != null) infoImage.SetImageBitmap(BitmapFactory.DecodeFile(Trip.ImageUrl));
                if (infoTitle != null) infoTitle.Text = Trip.Title;
                if (infoNotes != null) infoNotes.Text = Trip.Notes;

                return view;
            }
            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            throw new NotImplementedException();
        }
    }
}