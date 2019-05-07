using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgencyApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewAssignments : ContentPage
    {
        private bool hasLocationPermission = false;

        public ViewAssignments()
        {
            InitializeComponent();

            GetPermissions();
        }

        private async void GetPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                    {
                        await DisplayAlert("Need your location", "We need to access your location", "Ok");
                    }
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
                    if (results.ContainsKey(Permission.LocationWhenInUse))
                    {
                        status = results[Permission.LocationWhenInUse];
                    }
                    if (status == PermissionStatus.Granted)
                    {
                        hasLocationPermission = true;
                        mapViewAssignment.IsShowingUser = true;
                    }
                    else
                    {
                        await DisplayAlert("Need your location", "We need to access your location", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;
                locator.PositionChanged += Locator_PositionChanged;
                await locator.StartListeningAsync(TimeSpan.Zero, 100);
            }

            GetLocation();

            /*using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                var posts = conn.Table<Post>().ToList();
                DisplayInMap(posts);
            }*/
            //var posts = await Post.Read();
           // DisplayInMap(posts);
        }

        //private void DisplayInMap(List<Post> posts)
        //{
        //    foreach (var post in posts)
        //    {
        //        try
        //        {
        //            var position = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);

        //            var pin = new Xamarin.Forms.Maps.Pin()
        //            {
        //                Type = Xamarin.Forms.Maps.PinType.SavedPin,
        //                Position = position,
        //                Label = post.VenueName,
        //                Address = post.Address
        //            };

        //            mapLocation.Pins.Add(pin);
        //        }
        //        catch (NullReferenceException nre) { }
        //        catch (Exception ex) { }
        //    }
        //}

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            MoveMap(e.Position);
        }

        private async void GetLocation()
        {
            if (hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();

                MoveMap(position);
            }
        }

        private void MoveMap(Position position)
        {
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 1, 1);
            mapViewAssignment.MoveToRegion(span);
        }
    }
}