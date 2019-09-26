using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TravelRecord.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryPage : ContentPage
	{
		public HistoryPage ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
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

            var posts = await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.CurrentUser.Id)
                .ToListAsync();

            postListView.ItemsSource = posts;
        }
    }
}