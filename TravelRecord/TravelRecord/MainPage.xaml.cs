using System;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void LoginButton_OnClicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text.Trim());
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text.Trim());

            if (isEmailEmpty || isPasswordEmpty)
            {
                
            }
            else
            {
                
            }
        }
    }
}
