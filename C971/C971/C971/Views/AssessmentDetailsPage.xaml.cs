using C971.Data;
using C971.Models;
using C971.Services;
using C971.ViewModels;
using Plugin.LocalNotifications;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("AssessmentId", "assessmentId")]
    public partial class AssessmentDetailsPage : ContentPage
    {
        private AssessmentRepository _assessmentRepository;
        private CourseRepository _courseRepository;

        private int _assessmentId;
        public int AssessmentId
        {
            get
            {
                return _assessmentId;
            }
            set
            {
                _assessmentId = value;
                var assessment = _assessmentRepository.GetByIdAsync(_assessmentId).Result;               
                BindingContext = new AssessmentDetailsViewModel(assessment);

                CheckAssessmentNotifications();
            }
        }


        public AssessmentDetailsPage()
        {
            InitializeComponent();

            var dbContext = DependencyService.Get<ISqliteDbContext>();
            _assessmentRepository = new AssessmentRepository(dbContext);
            _courseRepository = new CourseRepository(dbContext);    
            BindingContext = new AssessmentDetailsViewModel();
        }

        private async void SaveButton_Clicked(object sender, System.EventArgs e)
        {
            var viewModel = BindingContext as AssessmentDetailsViewModel;
            if (viewModel != null)
            {
                viewModel.SaveAssessment();
                await Shell.Current.Navigation.PopAsync();
            }
        }

        private void CheckAssessmentNotifications()
        {
            var viewModel = BindingContext as AssessmentDetailsViewModel;
            if (viewModel != null)
            {
                if (viewModel.NotifyStartDate && viewModel.StartDate <= DateTime.Now)
                {
                    CrossLocalNotifications.Current.Show("Assessment Start Date",
                        $"Your assessment {viewModel.AssessmentName} has started");
                }
                if (viewModel.NotifyEndDate && viewModel.EndDate <= DateTime.Now)
                {
                    CrossLocalNotifications.Current.Show("Assessment End Date",
                        $"Your assessment {viewModel.AssessmentName} has ended");
                }
            }
        }

        private void NotifyStartDate_OnToggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            CheckAssessmentNotifications();
        }

        private void NotifyEndDate_OnToggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            CheckAssessmentNotifications();
        }

        private async void EndDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            var oldDate = e.OldDate;
            var newDate = e.NewDate;

            var viewModel = BindingContext as AssessmentDetailsViewModel;
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

        private async void AssessmentName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                await DisplayAlert("Error", "Assessment name cannot be empty", "Ok");
                var viewModel = BindingContext as AssessmentDetailsViewModel;
                if (viewModel != null)
                {
                    viewModel.AssessmentName = e.OldTextValue;
                }
                var entry = sender as Entry;
                entry.Text = e.OldTextValue;
            }
        }
    }
}
