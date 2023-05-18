using C971.Data;
using C971.Models;
using C971.Services;
using C971.ViewModels;
using Plugin.LocalNotifications;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace C971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("CourseId", "courseId")]
    public partial class CourseDetailsPage : ContentPage
    {

        private CourseRepository _courseRepository;

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
                var dbContext = DependencyService.Get<ISqliteDbContext>();
                _courseRepository = new CourseRepository(dbContext);
                var course = _courseRepository.GetByIdAsync(_courseId).Result;
                BindingContext = new CourseDetailsViewModel(course);

                CheckCourseNotifications();
            }
        }


        public CourseDetailsPage()
        {
            InitializeComponent();
            BindingContext = new CourseDetailsViewModel();
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                throw new InvalidDataException($"There is no data to use to return");
            }
            var assessment = e.Item as Assessment;
            await Shell
                .Current
                .GoToAsync($"assessmentdetails?assessmentId={assessment.AssessmentId}");
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
                await DisplayAlert("Error",
                    "You may not have more than two assessments per course",
                    "Ok"); 
            }
            else if(viewModel.Assessments.Count == 1)
            {
                var requiredAssessmentType = 
                        viewModel.Assessments[0].AssessmentType ==
                        AssessmentTypeTypes.OBJECTIVE ? 
                        AssessmentTypeTypes.PERFORMANCE :
                        AssessmentTypeTypes.OBJECTIVE;

                viewModel.AddFixedNewAssessment(requiredAssessmentType);
                RebindAssessments();
            }
            else
            {
                viewModel.AddNewAssessment();
                RebindAssessments();
            }

        }

        private async void RemoveAssessment_Clicked(object sender, EventArgs e)
        {
            //Walk the UI Tree to get back to the ViewCell
            var button = sender as Button;
            var stackLayout = button.Parent;
            var viewCell = stackLayout.Parent;

            //Get that data that the ViewCell is bound to
            var data = viewCell.BindingContext as Assessment;

            //Remove the course from the ViewModel
            var viewModel = BindingContext as CourseDetailsViewModel;
            await viewModel.RemoveAssessment(data);

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

        private void CheckCourseNotifications()
        {
            var viewModel = BindingContext as CourseDetailsViewModel;
            if (viewModel != null)
            {
                if (viewModel.NotifyStartDate && viewModel.StartDate <= DateTime.Now)
                {
                    CrossLocalNotifications.Current.Show("Course Start Date",
                        $"Your course {viewModel.CourseName} has started");
                }
                if (viewModel.NotifyEndDate && viewModel.EndDate <= DateTime.Now)
                {
                    CrossLocalNotifications.Current.Show("Course End Date",
                        $"Your course {viewModel.CourseName} has ended");
                }
            }
        }

        private async void ShareButton_Clicked(System.Object sender,
            System.EventArgs e)
        {
            var viewModel = BindingContext as CourseDetailsViewModel;
            if(viewModel!= null)
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Text = viewModel.Notes,
                    Title = "Share Text"
                });
            }
            
        }

        private void NotifyStartDate_OnToggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            CheckCourseNotifications();
        }

        private void NotifyEndDate_OnToggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            CheckCourseNotifications();
        }

        private async void EndDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            var oldDate = e.OldDate;
            var newDate = e.NewDate;

            var viewModel = BindingContext as CourseDetailsViewModel;
            if (viewModel != null)
            {
                if (newDate < viewModel.StartDate)
                {
                    await DisplayAlert("Error", "End date cannot be before Start date", "Ok");
                    viewModel.EndDate = oldDate;
                    var datePicker = sender as DatePicker;
                    datePicker.Date = oldDate;
                }
            }
        }

        private async void CourseName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                await DisplayAlert("Error", "Course name cannot be empty", "Ok");
                var viewModel = BindingContext as CourseDetailsViewModel;
                if (viewModel != null)
                {
                    viewModel.CourseName = e.OldTextValue;
                }
                var entry = sender as Entry;
                entry.Text = e.OldTextValue;
            }
        }

        private async void InstructorName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                await DisplayAlert("Error", "Instructor name cannot be empty", "Ok");
                var viewModel = BindingContext as CourseDetailsViewModel;
                if (viewModel != null)
                {
                    viewModel.InstructorName = e.OldTextValue;
                }
                var entry = sender as Entry;
                entry.Text = e.OldTextValue;
            }
        }

        private async void InstructorPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                await DisplayAlert("Error", "Instructor phone number cannot be empty", "Ok");
                var viewModel = BindingContext as CourseDetailsViewModel;
                if (viewModel != null)
                {
                    viewModel.InstructorPhone = e.OldTextValue;
                }
                var entry = sender as Entry;
                entry.Text = e.OldTextValue;
            }
        }

        private async void InstructorEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                await DisplayAlert("Error", "Instructor email cannot be empty", "Ok");
                var viewModel = BindingContext as CourseDetailsViewModel;
                if (viewModel != null)
                {
                    viewModel.InstructorEmail = e.OldTextValue;
                }
                var entry = sender as Entry;
                entry.Text = e.OldTextValue;
            }
        }
    }
}
