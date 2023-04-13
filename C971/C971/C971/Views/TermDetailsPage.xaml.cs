using C971.Models;
using C971.Services;
using C971.ViewModels;
using System.IO;
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

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if(e.Item == null)
            {
                throw new InvalidDataException($"There is no data to use to return");
            }

            var course = e.Item as Course;
            await Shell.Current.GoToAsync($"coursedetails?courseId={course.CourseId}");
        }

        private async void AddCourse_Clicked(object sender, System.EventArgs e)
        {
            await DisplayAlert("Button Clicked", "Add Course button clicked!!!!","Cancel");
        }

        private async void RemoveCourse_Clicked(object sender, System.EventArgs e)
        {
            await DisplayAlert("Button Clicked", "Remove Course button clicked!!!!", "Cancel");
        }
    }
}
