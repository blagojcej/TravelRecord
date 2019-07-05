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
	public partial class NewTravelPage : ContentPage
	{
		public NewTravelPage ()
		{
			InitializeComponent ();
		}

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            Post post=new Post()
            {
                Experience = experienceEntry.Text.Trim()
            };

            //Connection to database
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                //Table where we're going to inserting
                conn.CreateTable<Post>();

                //Insert into database (How many rows are inserted)
                int rows = conn.Insert(post);

                if (rows > 0)
                {
                    DisplayAlert("Success", "Experience successfully inserted", "OK");
                }
                else
                {
                    DisplayAlert("Failure", "Experience failed to be inserted", "OK");
                }
            }
        }
    }
}