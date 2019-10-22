using System;
using UsersAppMVVM.Models;

namespace UsersAppMVVM.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        UsersListViewModel lvm;

        public User user { get; private set; }

        public UserViewModel()
        {
            user = new User();
        }

        public UsersListViewModel ListViewModel
        {
            get { return lvm; }
            set
            {
                if (lvm != value)
                {
                    lvm = value;
                    OnPropertyChanged(nameof(ListViewModel));
                }
            }
        }

        public string FirstName
        {
            get { return user.FirstName; }
            set
            {
                if (user.FirstName != value)
                {
                    user.FirstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        public string LastName
        {
            get { return user.LastName; }
            set
            {
                if (user.LastName != value)
                {
                    user.LastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        public string Email
        {
            get { return user.Email; }
            set
            {
                if (user.Email != value)
                {
                    user.Email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string Phone
        {
            get { return user.Phone; }
            set
            {
                if (user.Phone != value)
                {
                    user.Phone = value;
                    OnPropertyChanged(nameof(Phone));
                }
            }
        }

        public Uri ImageLarge
        {
            get { return user.ImageLarge; }
            set
            {
                if (user.ImageLarge != value)
                {
                    user.ImageLarge = value;
                    OnPropertyChanged(nameof(ImageLarge));
                }
            }
        }

        public Uri ImageMedium
        {
            get { return user.ImageMedium; }
            set
            {
                if (user.ImageMedium != value)
                {
                    user.ImageMedium = value;
                    OnPropertyChanged(nameof(ImageMedium));
                }
            }
        }
    }
}
