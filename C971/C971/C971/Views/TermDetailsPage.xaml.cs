using C971.Commands;
using C971.Models;
using C971.Services;
using C971.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;

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
            var viewModel = BindingContext as TermDetailsViewModel;
            if(viewModel != null)
            {
                var term = _dataStore.GetTermById(viewModel.TermId);
                if(term.Courses.Count < 6)
                {
                    viewModel.AddNewCourseCommand.Execute(viewModel);
                }
                else
                {
                    await DisplayAlert("Error", "Unable to have more than six courses for a given term", "Cancel");
                }
                
            }
        }
        private async void RemoveCourse_Clicked(object sender, System.EventArgs e)
        {
            //Walk the UI Tree to get back to the ViewCell
            var button = sender as ImageButton;
            var stackLayout = button.Parent;
            var viewCell = stackLayout.Parent;

            //Get that data that the ViewCell is bound to
            var data = viewCell.BindingContext as Course;

            //Remove the course from the ViewModel
            var viewModel = BindingContext as TermDetailsViewModel;
            viewModel.RemoveCourse(data);

            //Update the UI
            RebindCourses();
        }
        private void TermDetailsPage_Appearing(object sender, System.EventArgs e)
        {
            RebindCourses();
        }

        private void RebindCourses()
        {
            var viewModel = BindingContext as TermDetailsViewModel;
            var term = _dataStore.GetTermById(viewModel.TermId);
            ListViewCourses.ItemsSource = null;
            ListViewCourses.ItemsSource = viewModel.Courses;
        }
    }
}
