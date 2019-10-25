using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TravelRecord.Model;

namespace TravelRecord.ViewModel.Commands
{
    public class PostCommand : ICommand
    {
        private NewTravelVM ViewModel;

        public PostCommand(NewTravelVM viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            var post = (Post) parameter;

            if (post!=null)
            {
                if (string.IsNullOrEmpty(post.Experience)) return false;

                if (post.Venue != null)
                    return true;

                return false;
            }

            return false;
        }

        public void Execute(object parameter)
        {
            var post = (Post) parameter;
            ViewModel.PublishPost(post);
        }

        public event EventHandler CanExecuteChanged;
    }
}
