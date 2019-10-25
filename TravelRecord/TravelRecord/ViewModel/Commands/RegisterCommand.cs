using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TravelRecord.Model;

namespace TravelRecord.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        private RegisterVM ViewModel;

        public RegisterCommand(RegisterVM viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            User user = (User) parameter;

            if (user == null) return false;

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }

            if (user.Password == user.ConfirmPassword)
            {
                return true;
            }

            return false;
        }

        public void Execute(object parameter)
        {
            User user = (User) parameter;
            ViewModel.Register(user);
        }

        public event EventHandler CanExecuteChanged;
    }
}
