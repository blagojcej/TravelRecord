using System;
using System.Linq;
using TravelRecord.Model;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //Set source of the image
            //Get the type of MainPage
            var assembly = typeof(MainPage);

            iconImage.Source = ImageSource.FromResource("TravelRecord.Assets.Images.plane.png", assembly);
        }

        private async void LoginButton_OnClicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text.Trim());
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text.Trim());

            if (isEmailEmpty || isPasswordEmpty)
            {

            }
            else
            {
                //Get User table from Azure
                var user = (await App.MobileService.GetTable<User>().Where(u => u.Email == emailEntry.Text.Trim())
                    .ToListAsync()).FirstOrDefault();

                if (user != null)
                {
                    if (user.Password == passwordEntry.Text.Trim())
                    {
                        App.CurrentUser = user;
                        await Navigation.PushAsync(new HomePage());
                    }
                    else
                    {
                        await DisplayAlert("Error ", "Email or password are incorrect", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "There was an error logging you in", "OK");
                }
            }
        }

        private void RegisterUserButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
