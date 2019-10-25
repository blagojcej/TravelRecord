using System;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using TravelRecord.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TravelRecord
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;

        public static MobileServiceClient MobileService =
            new MobileServiceClient("https://apptravelrecord.azurewebsites.net");

        public static IMobileServiceSyncTable<Post> postsTable;

        public static User CurrentUser = new User();

        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());

            DatabaseLocation = databaseLocation;

            //** Sync local and central databas
            // Configure database location
            var store = new MobileServiceSQLiteStore(databaseLocation);
            // Create the local table
            store.DefineTable<Post>();
            // How to sync local and central database
            // Initialize sync service
            MobileService.SyncContext.InitializeAsync(store);
            // Get table first from local storage, then from cloud
            postsTable = MobileService.GetSyncTable<Post>();
            //**
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
