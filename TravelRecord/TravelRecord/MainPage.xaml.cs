using System;
using System.Linq;
using TravelRecord.Model;
using TravelRecord.ViewModel;
using Xamarin.Forms;

namespace TravelRecord
{
    public partial class MainPage : ContentPage
    {
        private MainVM viewModel;

        public MainPage()
        {
            InitializeComponent();

            //Set source of the image
            //Get the type of MainPage
            var assembly = typeof(MainPage);

            viewModel=new MainVM();
            BindingContext = viewModel;

            iconImage.Source = ImageSource.FromResource("TravelRecord.Assets.Images.plane.png", assembly);
        }
    }
}
