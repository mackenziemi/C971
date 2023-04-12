using C971.Services;
using C971.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermDetailsPage : ContentPage
    {

        private IC971DataStore _dataStore;


        public TermDetailsPage()
        {
            _dataStore = DependencyService.Get<IC971DataStore>();

            InitializeComponent();
            var viewModel = new TermDetailsViewModel(_dataStore);
            BindingContext = viewModel;
        }
        public TermDetailsPage(IC971DataStore dataStore)
        {
            _dataStore = dataStore;

            InitializeComponent();
            var viewModel = new TermDetailsViewModel(_dataStore);
            BindingContext = viewModel;
        }
    }
}
