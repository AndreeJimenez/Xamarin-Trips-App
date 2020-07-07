using AppTrips.Models;
using AppTrips.Renders;
using AppTrips.UWP.Renders;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace AppTrips.UWP.Renders
{
    public class CustomMapRenderer : MapRenderer
    {
        MapControl nativeMap;
        MarkerWindow MarkerWindow;
        bool MarkerWindowShown = false;
        TripModel Trip;
        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                nativeMap.MapElementClick -= OnMapElementClick;
                nativeMap.Children.Clear();
                MarkerWindow = null;
                nativeMap = null;
            }

            if (e.NewElement != null)
            {
                this.Trip = (e.NewElement as CustomMap).Trip;

                var formsMap = (CustomMap)e.NewElement;
                nativeMap = Control as MapControl;
                nativeMap.Children.Clear();
                nativeMap.MapElementClick += OnMapElementClick;

                var snPosition = new BasicGeoposition 
                { 
                    Latitude = Trip.Latitude, 
                    Longitude = Trip.Longitude 
                };

                var snPoint = new Geopoint(snPosition);

                var mapIcon = new MapIcon();
                mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///ping.png"));
                mapIcon.CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible;
                mapIcon.Location = snPoint;
                mapIcon.NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0);

                nativeMap.MapElements.Add(mapIcon);
            }
        }

        private void OnMapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var mapIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;
            if (mapIcon != null)
            {
                if(!MarkerWindowShown)
                {
                    if (MarkerWindow == null) MarkerWindow = new MarkerWindow(Trip);

                    var snPosition = new BasicGeoposition
                    {
                        Latitude = Trip.Latitude,
                        Longitude = Trip.Longitude
                    };
                    var snPoint = new Geopoint(snPosition);

                    nativeMap.Children.Add(MarkerWindow);
                    MapControl.SetLocation(MarkerWindow, snPoint);
                    MapControl.SetNormalizedAnchorPoint(MarkerWindow, new Windows.Foundation.Point(0.5, 1.0));

                    MarkerWindowShown = true;
                } else
                {
                    nativeMap.Children.Remove(MarkerWindow);
                    MarkerWindowShown = false;
                }
            }
        }
    }
}
