using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TravelRecord.Helpers;
using TravelRecord.Model;
using TravelRecord.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        private HistoryVM viewModel;

        public HistoryPage()
        {
            InitializeComponent();
            viewModel=new HistoryVM();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            #region Reading data from local database (Sqlite)

            /*
            using (SQLiteConnection conn=new SQLiteConnection(App.DatabaseLocation))
            {
                //If table is not exists would be created
                //We can go to History page before insert any records
                conn.CreateTable<Post>();

                var posts = conn.Table<Post>().ToList();

                postListView.ItemsSource = posts;
            }
            */

            #endregion Reading data from local database (Sqlite)

            await viewModel.UpdatePosts();

            // Synchronize local andcloud database
            await AzureAppServiceHelper.SyncAsync();
        }

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            var post = (Post)((MenuItem) sender).CommandParameter;
            viewModel.DeletePost(post);

            // After deleting refresh list
            await viewModel.UpdatePosts();
        }

        private async void PostListView_OnRefreshing(object sender, EventArgs e)
        {
            await viewModel.UpdatePosts();

            // Synchronize local andcloud database
            await AzureAppServiceHelper.SyncAsync();

            postListView.EndRefresh();
            //postListView.IsRefreshing = false; // same as calling function above
        }
    }
}