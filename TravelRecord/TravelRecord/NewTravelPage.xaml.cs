using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SQLite;
using TravelRecord.Model;
using TravelRecord.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        private NewTravelVM viewModel;

        public NewTravelPage()
        {
            InitializeComponent();
            viewModel = new NewTravelVM();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                // Get current status of permission
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                // If permission is not granted
                if (status != PermissionStatus.Granted)
                {
                    // If current permission need user permission directly
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need permission", "We will have to access your location", "OK");
                    }

                    // After requesting permission, try get permission
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    // Check if permission is allowed
                    if (results.ContainsKey(Permission.Location))
                    {
                        status = results[Permission.Location];
                    }
                }

                if (status == PermissionStatus.Granted)
                {

                    //Request Venues
                    var locator = CrossGeolocator.Current;
                    var position = await locator.GetPositionAsync();

                    var venues = await Venue.GetVenuesAsync(position.Latitude, position.Longitude);

                    //Listing into the UI
                    venueListView.ItemsSource = venues;
                }
                else
                {
                    await DisplayAlert("Need permission", "We will have to access your location", "OK");
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}