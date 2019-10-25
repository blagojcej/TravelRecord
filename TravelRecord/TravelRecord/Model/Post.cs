using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using SQLite;
using TravelRecord.Annotations;

namespace TravelRecord.Model
{
    public class Post : INotifyPropertyChanged
    {
        #region Proeprties

        //[PrimaryKey, AutoIncrement]
        //public string Id { get; set; }
        private string id;

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }


        //[MaxLength(250)]
        //public string Experience { get; set; }
        private string experience;

        public string Experience
        {
            get { return experience; }
            set
            {
                experience = value;
                OnPropertyChanged("Experience");
            }
        }

        private string venueName;

        public string VenueName
        {
            get { return venueName; }
            set
            {
                venueName = value;
                OnPropertyChanged("VenueName");
            }
        }

        private string categoryId;

        public string CategoryId
        {
            get { return categoryId; }
            set
            {
                categoryId = value;
                OnPropertyChanged("CategoryId");
            }
        }

        private string categoryName;

        public string CategoryName
        {
            get { return  categoryName; }
            set
            {
                categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        private double latitude;

        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged("Latitude");
            }
        }

        private double longitude;

        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                OnPropertyChanged("Longitude");
            }
        }

        private int distance;

        public int Distance
        {
            get { return distance; }
            set
            {
                distance = value;
                OnPropertyChanged("Distance");
            }
        }

        private string userId;

        public string UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }

        private Venue venue;

        // Ignore this property when serializing into json, saving into database
        [JsonIgnore]
        public Venue Venue
        {
            get { return venue; }
            set
            {
                venue = value;

                if (venue != null && venue.categories != null)
                {
                    var firstCategory = venue.categories.FirstOrDefault();

                    // We set Properties, not values to fire OnPropertyChanged Event. If we assign to the value, the event won't fire
                    if (firstCategory != null)
                    {
                        CategoryId = firstCategory.id;
                        CategoryName = firstCategory.name;
                    }
                }

                // We set Properties, not values to fire OnPropertyChanged Event. If we assign to the value, the event won't fire
                if (venue != null)
                {
                    if (venue.location != null)
                    {
                        Address = venue.location.address;
                        Distance = venue.location.distance;
                        Latitude = venue.location.lat;
                        Longitude = venue.location.lng;
                    }

                    VenueName = venue.name;
                }

                UserId = App.CurrentUser.Id;

                OnPropertyChanged("Venue");
            }
        }

        private DateTimeOffset createat;

        public DateTimeOffset CREATEDAT
        {
            get { return createat; }
            set
            {
                createat = value;
                OnPropertyChanged("CREATEDAT");
            }
        }

        #endregion Properties

        #region Methods

        public static async void Insert(Post post)
        {
            //Commented after adding sync between local and cloud database
            //await App.MobileService.GetTable<Post>().InsertAsync(post);

            await App.postsTable.InsertAsync(post);
            await App.MobileService.SyncContext.PushAsync();
        }

        public static async Task<bool> Delete(Post post)
        {
            try
            {
                //Commented after adding sync between local and cloud database
                //await App.MobileService.GetTable<Post>().DeleteAsync(post);

                await App.postsTable.DeleteAsync(post);
                await App.MobileService.SyncContext.PushAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<List<Post>> Read()
        {
            //Commented after adding sync between local and cloud database
            //var posts= await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.CurrentUser.Id)
            //    .ToListAsync();

            var posts = await App.postsTable.Where(p => p.UserId == App.CurrentUser.Id).ToListAsync();

            return posts;
        }

        public static Dictionary<string, int> PostCategories(List<Post> posts)
        {
            //Get all distinct categories using LINQ
            var categories = (from p in posts
                              orderby p.CategoryId
                select p.CategoryName).Distinct().ToList();

            //Another way to get all distinct categories using LINQ
            var categories2 = posts.OrderBy(p => p.CategoryId).Select(p => p.CategoryName).Distinct().ToList();

            //Get amount of posts by category using LINQ
            Dictionary<string, int> categoriesCount = new Dictionary<string, int>();
            foreach (var category in categories)
            {
                var count = (from post in posts
                             where post.CategoryName == category
                    select post).ToList().Count;

                //Another way to get amount of posts by category using LINQ
                var count2 = posts.Where(p => p.CategoryName == category).ToList().Count;

                categoriesCount.Add(category, count);
            }

            return categoriesCount;
        }

        #endregion Methods

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Events
    }
}
