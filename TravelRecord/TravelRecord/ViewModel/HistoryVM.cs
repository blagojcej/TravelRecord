using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TravelRecord.Model;

namespace TravelRecord.ViewModel
{
    public class HistoryVM
    {
        public ObservableCollection<Post> Posts { get; set; }

        public HistoryVM()
        {
            Posts = new ObservableCollection<Post>();
        }

        public async Task<bool> UpdatePosts()
        {
            try
            {
                var posts = await Post.Read();

                if (posts != null)
                {
                    // Clear all posts after waiting posts to be read from database
                    Posts.Clear();
                    foreach (var post in posts)
                    {
                        Posts.Add(post);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async void DeletePost(Post postToDelete)
        {
            await Post.Delete(postToDelete);
        }
    }
}
