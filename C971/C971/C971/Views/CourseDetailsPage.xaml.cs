using C971.Models;
using C971.Services;
using C971.ViewModels;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("CourseId", "courseId")]
    public partial class CourseDetailsPage : ContentPage
    {
        private int _courseId;
        public int CourseId
        {
            get
            {
                return _courseId;
            }
            set
            {
                _courseId = value;
                var dataStore = DependencyService.Get<IC971DataStore>();
                var course = dataStore.GetCourseById(_courseId);
                BindingContext = new CourseDetailsViewModel(dataStore, course);
            }
        }


        public CourseDetailsPage()
        {
            InitializeComponent();
            var dataStore = DependencyService.Get<IC971DataStore>();
            BindingContext = new CourseDetailsViewModel(dataStore);
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                throw new InvalidDataException($"There is no data to use to return");
            }
            var assessment = e.Item as Assessment;
            await Shell.Current.GoToAsync($"assessmentdetails?assessmentId={assessment.AssessmentId}");
        }

        private async void SaveButton_Clicked(object sender, System.EventArgs e)
        {
            var viewModel = BindingContext as CourseDetailsViewModel;
            if(viewModel != null)
            {
                viewModel.SaveCourse();
                await Shell.Current.Navigation.PopAsync();
            }
        }

        private async void AddAssessment_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as CourseDetailsViewModel;
            
            if(viewModel.Assessments.Count >=2)
            {
                await DisplayAlert("Error", "You may not have more than two assessments per course", "Ok"); 
            }
            else if(viewModel.Assessments.Count == 1)
            {
                var requiredAssessmentType = 
                        viewModel.Assessments[0].AssessmentType == AssessmentTypeTypes.OBJECTIVE ? 
                        AssessmentTypeTypes.PERFORMANCE : AssessmentTypeTypes.OBJECTIVE;

                viewModel.AddFixedNewAssessment(requiredAssessmentType);
                RebindAssessments();
            }
            else
            {
                viewModel.AddNewAssessment();
                RebindAssessments();
            }

        }

        private void RemoveAssessment_Clicked(object sender, EventArgs e)
        {
            //Walk the UI Tree to get back to the ViewCell
            var button = sender as Button;
            var stackLayout = button.Parent;
            var viewCell = stackLayout.Parent;

            //Get that data that the ViewCell is bound to
            var data = viewCell.BindingContext as Assessment;

            //Remove the course from the ViewModel
            var viewModel = BindingContext as CourseDetailsViewModel;
            viewModel.RemoveAssessment(data);

            //Update the UI
            RebindAssessments();
        }

        private void RebindAssessments()
        {
            var viewModel = BindingContext as CourseDetailsViewModel;
            ListViewAssessments.ItemsSource = null;
            ListViewAssessments.ItemsSource = viewModel.Assessments;
        }

        private void CourseDetailsPage_Appearing(object sender, EventArgs e)
        {
            RebindAssessments();
        }
    }
}
