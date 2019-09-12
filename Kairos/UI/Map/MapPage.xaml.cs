using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
        private Geoposition _lastGeoPosition;
        private bool _firstMeasure;
        #endregion

        public MapPage()
        {
            this.InitializeComponent();
            _basicGeoposition = new BasicGeoposition { Latitude = 51.314110, Longitude = 9.485962 };
            //AddSpaceNeedleIcon();
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
            _firstMeasure = true;

            switch (accessStatus)
            {
                case GeolocationAccessStatus.Unspecified:
                    break;
                case GeolocationAccessStatus.Allowed:
                    Geolocator geolocator = new Geolocator
                    {
                        DesiredAccuracyInMeters = 10,
                        ReportInterval = 2000
                        
                    };
                    geolocator.PositionChanged += Geolocator_PositionChanged;
                    geolocator.StatusChanged += Geolocator_StatusChanged;
                    Geoposition pos = await geolocator.GetGeopositionAsync();
                    break;
                case GeolocationAccessStatus.Denied:
                    break;
                default:
                    break;
            }
        }

        private async void Geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var geoPoint = new BasicGeoposition();
                geoPoint.Latitude = args.Position.Coordinate.Latitude;
                geoPoint.Longitude = args.Position.Coordinate.Longitude;
                UpdateLocation(geoPoint, false);
            });
            _lastGeoPosition = args.Position;
            _firstMeasure = false;
        }

        private void Geolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            
        }

        private void UpdateLocation(BasicGeoposition newPoint, bool center) 
        {
            if (_firstMeasure || _lastGeoPosition == null || (_lastGeoPosition?.Coordinate?.Latitude != newPoint.Latitude && _lastGeoPosition?.Coordinate?.Longitude != newPoint.Longitude))
            {
                if (myMap.Layers.Count >= 1)
                {
                    myMap.Layers.Clear();
                }
                

                var MyLandmarks = new List<MapElement>();

                var spaceNeedleIcon = new MapIcon
                {
                    Location = new Geopoint(newPoint),
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

                if (center || _firstMeasure)
                {
                    myMap.Center = new Geopoint(newPoint);
                    myMap.ZoomLevel = 20;
                }
            }
            
        }

        private void BtnCenterToLocation_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            _firstMeasure = true;
        }

        private void BbtnCopyLocation_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
