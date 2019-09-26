using System;
using System.IO;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using Environment = System.Environment;

namespace TravelRecord.Droid
{
    [Activity(Label = "TravelRecord", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //Use maps for Xamarin Forms
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            //User azure services
            CurrentPlatform.Init();

            string dbName = "travel_db.sqlite";
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string fullPath = Path.Combine(folderPath, dbName);

            //LoadApplication(new App());
            // New constructor for database location
            LoadApplication(new App(fullPath));
        }
    }
}