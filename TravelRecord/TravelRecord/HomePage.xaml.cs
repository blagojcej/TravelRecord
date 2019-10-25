using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecord.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        private HomeVM viewModel;

        public HomePage()
        {
            InitializeComponent();
            viewModel=new HomeVM();
            BindingContext = viewModel;
        }
    }
}