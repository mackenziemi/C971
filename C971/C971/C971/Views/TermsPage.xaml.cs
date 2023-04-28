using System.IO;
using C971.Models;
using C971.Services;
using C971.ViewModels;
using Plugin.LocalNotifications;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermsPage : ContentPage
    {
        public TermsPage()
        {
            InitializeComponent();
            var dataStore = DependencyService.Get<IC971DataStore>();
            BindingContext = new TermsViewModel(dataStore);
        }

        public async void ListView_ItemTapped(System.Object sender,
            Xamarin.Forms.ItemTappedEventArgs e)
        {
            if(e.Item == null)
            {
                throw new InvalidDataException($"There is no data to use to return");
            }

            var term = e.Item as Term;
            await Shell
                .Current
                .GoToAsync($"termdetails?termId={term.TermId}");

        }

        public void AddTerm_Clicked(System.Object sender,
            System.EventArgs e)
        {
            var viewModel = BindingContext as TermsViewModel;
            if (viewModel != null)
            {
                viewModel.AddNewTerm();
            }
            RebindTerms();
        }

        public void RemoveTerm_Clicked(System.Object sender, System.EventArgs e)
        {
            //Walk the UI Tree to get back to the ViewCell
            var button = sender as Button;
            var stackLayout = button.Parent;
            var viewCell = stackLayout.Parent;

            //Get that data that the ViewCell is bound to
            var data = viewCell.BindingContext as Term;

            //Remove the course from the ViewModel
            var viewModel = BindingContext as TermsViewModel;
            viewModel.RemoveTerm(data);

            //Update the UI
            RebindTerms();
        }

        private void ContentPage_Appearing(System.Object sender, System.EventArgs e)
        {
            RebindTerms();
        }

        private void RebindTerms()
        {
            var viewModel = BindingContext as TermsViewModel;
            ListViewTerms.ItemsSource = null;
            ListViewTerms.ItemsSource = viewModel.Terms;
        }

        
    }
}
