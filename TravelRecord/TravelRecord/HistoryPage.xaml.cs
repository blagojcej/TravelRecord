﻿using System;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn=new SQLiteConnection(App.DatabaseLocation))
            {
                //If table is not exists would be created
                //We can go to History page before insert any records
                conn.CreateTable<Post>();

                var posts = conn.Table<Post>().ToList();
            }
        }
    }
}