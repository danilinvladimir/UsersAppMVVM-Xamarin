using UsersAppMVVM.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UsersAppMVVM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        public UserPage(UserViewModel ViewModel)
        {
            InitializeComponent();

            this.BindingContext = ViewModel;
        }
    }
}