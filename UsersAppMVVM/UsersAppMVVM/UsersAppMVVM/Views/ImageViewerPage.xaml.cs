using UsersAppMVVM.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UsersAppMVVM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageViewerPage : ContentPage
    {
        public ImageViewerPage(UserViewModel ViewModel)
        {
            InitializeComponent();

            this.BindingContext = ViewModel;
        }
    }
}