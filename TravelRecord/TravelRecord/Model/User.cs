using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TravelRecord.Annotations;

namespace TravelRecord.Model
{
    public class User: INotifyPropertyChanged
    {
        #region Properties

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

        private string email;

        public  string Email
        {
            get { return  email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }

        #endregion Properties

        #region Methods

        public static async Task<bool> Login(string email, string password)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(email.Trim());
            bool isPasswordEmpty = string.IsNullOrEmpty(password.Trim());

            if (isEmailEmpty || isPasswordEmpty)
            {
                return false;
            }
            else
            {
                //Get User table from Azure
                var user = (await App.MobileService.GetTable<User>().Where(u => u.Email == email.Trim())
                    .ToListAsync()).FirstOrDefault();

                if (user != null)
                {
                    if (user.Password == password.Trim())
                    {
                        App.CurrentUser = user;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public static async void Register(User user)
        {
            await App.MobileService.GetTable<User>().InsertAsync(user);
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
