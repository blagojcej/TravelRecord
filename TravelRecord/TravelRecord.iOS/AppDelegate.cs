﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;

namespace TravelRecord.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // Implement FastRenderers
            global::Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");
            global::Xamarin.Forms.Forms.Init();
            //Use maps for Xamarin Forms
            Xamarin.FormsMaps.Init();
            //User azure services
            CurrentPlatform.Init();

            string dbName = "travel_db.sqlite";
            // ".." (two dots) navigate to parent of this (Personal) folder
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", "Library");
            string fullPath = Path.Combine(folderPath, dbName);

            //LoadApplication(new App());
            // New constructor for database location
            LoadApplication(new App(fullPath));

            return base.FinishedLaunching(app, options);
        }
    }
}
