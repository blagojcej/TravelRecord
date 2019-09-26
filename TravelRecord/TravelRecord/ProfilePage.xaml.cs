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
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //using (SQLiteConnection conn=new SQLiteConnection(App.DatabaseLocation))
            //{
                //Get post table and list all posts in table
                //var postTable = conn.Table<Post>().ToList();
                var postTable = await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.CurrentUser.Id)
                    .ToListAsync();

                //Get all distinct categories using LINQ
                var categories = (from p in postTable
                    orderby p.CategoryId
                    select p.CategoryName).Distinct().ToList();

                //Another way to get all distinct categories using LINQ
                var categories2 = postTable.OrderBy(p => p.CategoryId).Select(p => p.CategoryName).Distinct().ToList();

                //Get amount of posts by category using LINQ
                Dictionary<string, int> categoriesCount=new Dictionary<string, int>();
                foreach (var category in categories)
                {
                    var count = (from post in postTable
                        where post.CategoryName == category
                        select post).ToList().Count;

                    //Another way to get amount of posts by category using LINQ
                    var count2 = postTable.Where(p => p.CategoryName == category).ToList().Count;

                    categoriesCount.Add(category, count);
                }

                categoriesListView.ItemsSource = categoriesCount;

                postCountLabel.Text = postTable.Count.ToString();

            //}
        }
    }
}