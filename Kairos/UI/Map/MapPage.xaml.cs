using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Kairos.UI.Map
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        #region Fields
        private BasicGeoposition _basicGeoposition;
        #endregion

        public MapPage()
        {
            this.InitializeComponent();
            _basicGeoposition = new BasicGeoposition { Latitude = 51.314110, Longitude = 9.485962 };
            AddSpaceNeedleIcon();
        }

        public void AddSpaceNeedleIcon()
        {
            var MyLandmarks = new List<MapElement>();
            Geopoint snPoint = new Geopoint(_basicGeoposition);

            var spaceNeedleIcon = new MapIcon
            {
                Location = snPoint,
                NormalizedAnchorPoint = new Point(0.5, 1.0),
                ZIndex = 0,
                Title = "Position"
            };

            MyLandmarks.Add(spaceNeedleIcon);

            var LandmarksLayer = new MapElementsLayer
            {
                ZIndex = 1,
                MapElements = MyLandmarks
            };

            myMap.Layers.Add(LandmarksLayer);

            myMap.Center = snPoint;
            myMap.ZoomLevel = 14;

        }
        private void MyMap_MapTapped(Windows.UI.Xaml.Controls.Maps.MapControl sender, Windows.UI.Xaml.Controls.Maps.MapInputEventArgs args)
        {

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var accessStatus = await Geolocator.RequestAccessAsync();

            switch (accessStatus)
            {
                case GeolocationAccessStatus.Unspecified:
                    break;
                case GeolocationAccessStatus.Allowed:
                    Geolocator geolocator = new Geolocator();
                    geolocator.DesiredAccuracyInMeters = 10;
                    geolocator.StatusChanged += Geolocator_StatusChanged;
                    Geoposition pos = await geolocator.GetGeopositionAsync();
                    break;
                case GeolocationAccessStatus.Denied:
                    break;
                default:
                    break;
            }
        }

        private void Geolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            throw new NotImplementedException();
        }

        private void UpdateLocation(BasicGeoposition newPoint) 
        {
            //(myMap.MapElements[0] as MapIcon).Location.Position.Latitude = newPoint.Latitude;
            //myMap.loca
        }
    }
}
