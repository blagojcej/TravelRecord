using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
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

        protected async override void OnAppearing()
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