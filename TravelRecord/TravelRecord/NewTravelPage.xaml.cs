using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using SQLite;
using TravelRecord.Logic;
using TravelRecord.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewTravelPage : ContentPage
	{
		public NewTravelPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //Request Venues
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await VenueLogic.GetVenuesAsync(position.Latitude, position.Longitude);

            //Listing into the UI
            venueListView.ItemsSource = venues;
        }

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            try
            {
                //Get information about seleted venue
                var selectedVenue = venueListView.SelectedItem as Venue;
                var firstCategory = selectedVenue.categories.FirstOrDefault();

                Post post = new Post()
                {
                    Experience = experienceEntry.Text.Trim(),
                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Address = selectedVenue.location.address,
                    Distance = selectedVenue.location.distance,
                    Latitude = selectedVenue.location.lat,
                    Longitude = selectedVenue.location.lng,
                    VenueName = selectedVenue.name
                };

                //Connection to database
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    //Table where we're going to inserting
                    conn.CreateTable<Post>();

                    //Insert into database (How many rows are inserted)
                    int rows = conn.Insert(post);

                    if (rows > 0)
                    {
                        DisplayAlert("Success", "Experience successfully inserted", "OK");
                    }
                    else
                    {
                        DisplayAlert("Failure", "Experience failed to be inserted", "OK");
                    }
                }
            }
            catch (NullReferenceException nre)
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}