using System.Linq;
using UsersAppMVVM.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UsersAppMVVM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersListPage : ContentPage
    {
        public UsersListPage()
        {
            InitializeComponent();

            BindingContext = new UsersListViewModel() { Navigation = this.Navigation };
        }

        private void Searching(object sender, TextChangedEventArgs e)
        {
            if (searchBar.Text == "")
                Foter.IsVisible = true;
            else
                Foter.IsVisible = false;

            var container = BindingContext as UsersListViewModel;

            UsersListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                UsersListView.ItemsSource = container.Users;
            else
                UsersListView.ItemsSource = container.Users.Where(i => (i.FirstName.ToLower() + " " + i.LastName.ToLower()).Contains(e.NewTextValue.ToLower()));

            UsersListView.EndRefresh();
        }
    }
}