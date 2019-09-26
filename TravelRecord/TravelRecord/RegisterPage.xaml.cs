using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecord.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void RegisterButton_OnClicked(object sender, EventArgs e)
        {
            if (passwordEntry.Text==confirmPasswordEntry.Text)
            {
                //We can register the user
                User user=new User()
                {
                    Email = emailEntry.Text.Trim(),
                    Password = passwordEntry.Text.Trim()
                };

                await App.MobileService.GetTable<User>().InsertAsync(user);

            }
            else
            {
                await DisplayAlert("Error", "Passwords don't match", "OK");
            }
        }
    }
}