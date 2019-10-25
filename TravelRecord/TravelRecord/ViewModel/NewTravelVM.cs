using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using TravelRecord.Annotations;
using TravelRecord.Model;
using TravelRecord.ViewModel.Commands;

namespace TravelRecord.ViewModel
{
    public class NewTravelVM : INotifyPropertyChanged
    {
        public PostCommand PostCommand { get; set; }

        private Post post;

        public Post Post
        {
            get { return post; }
            set
            {
                post = value;
                OnPropertyChanged("Post");
            }
        }

        private string experiance;

        public string Experience
        {
            get { return experiance; }
            set
            {
                experiance = value;
                Post=new Post()
                {
                    Experience = this.Experience,
                    Venue = this.Venue
                };
                OnPropertyChanged("Experience");
            }
        }

        private Venue venue;

        public Venue Venue
        {
            get { return venue; }
            set
            {
                venue = value;
                Post = new Post()
                {
                    Experience = this.Experience,
                    Venue = this.Venue
                };
                OnPropertyChanged("Venue");
            }
        }


        public NewTravelVM()
        {
            PostCommand=new PostCommand(this);
            Post=new Post();
            Venue=new Venue();
        }

        public async void PublishPost(Post post)
        {
            try
            {
                #region Saving into local Sqlite database
                //Connection to database
                /*
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
                */
                #endregion Saving into local Sqlite database

                //await App.MobileService.GetTable<Post>().InsertAsync(post);
                // After cleaning view using MVVM
                Post.Insert(post);
                await App.Current.MainPage.DisplayAlert("Success", "Experience successfully inserted", "OK");
            }
            catch (NullReferenceException nre)
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Experience failed to be inserted", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Experience failed to be inserted", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
