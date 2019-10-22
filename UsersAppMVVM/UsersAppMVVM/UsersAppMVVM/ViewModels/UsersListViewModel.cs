using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UsersAppMVVM.Models;
using UsersAppMVVM.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UsersAppMVVM.ViewModels
{
    public class UsersListViewModel : BaseViewModel
    {
        DataService dataService;

        readonly List<UserViewModel> AllUsers;
        public ObservableCollection<UserViewModel> Users { get; set; }

        UserViewModel selectedUser;

        public ICommand CallUserCommand { protected set; get; }
        public ICommand WriteUserCommand { protected set; get; }
        public ICommand TapImageCommand { protected set; get; }
        public ICommand RepeatCommand { protected set; get; }
        public ICommand MoreCommand { protected set; get; }
        public INavigation Navigation { get; set; }

        bool buttonRepeatActivated { get; set; }
        bool buttonMoreActivated { get; set; }
        bool loading { get; set; }
        string status { get; set; }

        async void LoadUsers()
        {
            buttonRepeatActivated = false;
            OnPropertyChanged(nameof(ButtonRepeatActivated));
            loading = true;
            OnPropertyChanged(nameof(Loading));
            status = "";
            OnPropertyChanged(nameof(Status));

            string response = await dataService.GetUsers();

            try
            {
                var users = JsonConvert.DeserializeObject<Response>(response);

                foreach (Results item in users.results)
                {
                    AllUsers.Add(new UserViewModel
                    {
                        FirstName = item.name.first,
                        LastName = item.name.last,
                        Email = item.email,
                        Phone = item.phone,
                        ImageMedium = item.picture.large,
                        ImageLarge = item.picture.medium
                    });
                }

                loading = false;
                OnPropertyChanged(nameof(Loading));

                DisplayUsers();
            }
            catch
            {
                loading = false;
                OnPropertyChanged(nameof(Loading));
                buttonRepeatActivated = true;
                OnPropertyChanged(nameof(ButtonRepeatActivated));

                if (response == "network_error")
                {
                    status = "Отсутствует подключение к сети";
                    OnPropertyChanged(nameof(Status));
                }
                else if (response == "request_error")
                {
                    status = "Произошла ошибка при выполнении запроса";
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public bool ButtonRepeatActivated
        {
            get { return buttonRepeatActivated; }
            set
            {
                buttonRepeatActivated = value;
                OnPropertyChanged(nameof(ButtonRepeatActivated));
            }
        }

        public bool ButtonMoreActivated
        {
            get { return buttonMoreActivated; }
            set
            {
                buttonMoreActivated = value;
                OnPropertyChanged(nameof(ButtonMoreActivated));
            }
        }

        public bool Loading
        {
            get { return loading; }
            set
            {
                loading = value;
                OnPropertyChanged(nameof(Loading));
            }
        }

        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        void DisplayUsers()
        {
            buttonMoreActivated = true;
            OnPropertyChanged(nameof(ButtonMoreActivated));

            for (int i = 0; i < 20; i++)
            {
                if (Users.Count < AllUsers.Count)
                {
                    Users.Add(AllUsers[Users.Count]);
                }
                else
                {
                    buttonMoreActivated = false;
                    OnPropertyChanged(nameof(ButtonMoreActivated));
                    break;
                }
            }
        }

        public UsersListViewModel()
        {
            dataService = new DataService();
            AllUsers = new List<UserViewModel>();
            Users = new ObservableCollection<UserViewModel>();
            CallUserCommand = new Command(CallUser);
            WriteUserCommand = new Command(WriteUser);
            TapImageCommand = new Command(TapImage);
            RepeatCommand = new Command(Repeat);
            MoreCommand = new Command(More);
            LoadUsers();
        }

        public UserViewModel SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (selectedUser != value)
                {
                    UserViewModel tempFriend = value;
                    selectedUser = null;
                    OnPropertyChanged(nameof(SelectedUser));
                    Navigation.PushAsync(new UserPage(new UserViewModel()
                    {
                        ListViewModel = this,
                        FirstName = "Имя: " + tempFriend.FirstName,
                        LastName = "Фамилия: " + tempFriend.LastName,
                        Phone = "Номер телефона: " + tempFriend.Phone,
                        Email = "Email: " + tempFriend.Email,
                        ImageLarge = tempFriend.ImageLarge,
                        ImageMedium = tempFriend.ImageMedium
                    }));
                }
            }
        }

        void CallUser(object userObject)
        {
            UserViewModel user = userObject as UserViewModel;
            if (user != null)
            {
                try
                {
                    PhoneDialer.Open(user.Phone);
                }
                catch { }
            }
        }

        void WriteUser(object userObject)
        {
            UserViewModel user = userObject as UserViewModel;
            if (user != null)
            {
                try
                {
                    var message = new SmsMessage("", new[] { user.Phone });
                    Sms.ComposeAsync(message);
                }
                catch { }
            }
        }

        void TapImage(object userObject)
        {
            UserViewModel user = userObject as UserViewModel;
            Navigation.PushAsync(new ImageViewerPage(user));
        }

        void Repeat()
        {
            LoadUsers();
        }

        void More()
        {
            DisplayUsers();
        }
    }
}
