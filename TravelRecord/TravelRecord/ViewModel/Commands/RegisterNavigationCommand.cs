using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TravelRecord.ViewModel.Commands
{
    public class RegisterNavigationCommand : ICommand
    {
        private MainVM ViewModel;

        public RegisterNavigationCommand(MainVM viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.Navigate();
        }

        public event EventHandler CanExecuteChanged;
    }
}
