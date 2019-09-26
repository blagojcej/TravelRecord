using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using SQLite;
using TravelRecord.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
		public MapPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;

            //Know when location changed
            locator.PositionChanged += Locator_PositionChanged;
            await locator.StartListeningAsync(new TimeSpan(0), 100);

            //get current position
            var position = await locator.GetPositionAsync();

            var center = new Position(position.Latitude, position.Longitude);
            //display in the center (1 degree to the right and 1 degree to the left) - first number 2, (1 degree from the top and 1 degree from the bottom) - second number 2
            var span = new MapSpan(center, 2, 2);
            locationsMap.MoveToRegion(span);

            #region Getting data from local storage (Sqlite)
            /*
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                //If table is not exists would be created
                //We can go to History page before insert any records
                conn.CreateTable<Post>();

                var posts = conn.Table<Post>().ToList();

                //Pin all history places on map
                DisplayInMap(posts);
            }
            */
            #endregion Getting data from local storage (Sqlite)

            var posts= await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.CurrentUser.Id)
                .ToListAsync();

            //Pin all history places on map
            DisplayInMap(posts);
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            var locator = CrossGeolocator.Current;
            locator.PositionChanged -= Locator_PositionChanged;
            await locator.StopListeningAsync();
        }

        private void DisplayInMap(List<Post> posts)
        {
            foreach (var post in posts)
            {
                try
                {
                    var position = new Position(post.Latitude, post.Longitude);

                    //Create the pin
                    var pin = new Pin()
                    {
                        Type = PinType.SavedPin,
                        Position = position,
                        Label = post.VenueName,
                        Address = post.Address
                    };
                    locationsMap.Pins.Add(pin);
                }
                catch (NullReferenceException nre)
                {

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var center = new Position(e.Position.Latitude, e.Position.Longitude);
            //display in the center (1 degree to the right and 1 degree to the left) - first number 2, (1 degree from the top and 1 degree from the bottom) - second number 2
            var span = new MapSpan(center, 2, 2);
            locationsMap.MoveToRegion(span);
        }
    }
}