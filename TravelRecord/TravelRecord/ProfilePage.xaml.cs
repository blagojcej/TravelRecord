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
	        var postTable = await Post.Read();

	        var categoriesCount = Post.PostCategories(postTable);

	        categoriesListView.ItemsSource = categoriesCount;

	        postCountLabel.Text = postTable.Count.ToString();

	        //}
	    }
	}
}