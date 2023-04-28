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
        private IC971DataStore _dataStore;

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
                var assessment = _dataStore.GetAssessmentById(_assessmentId);               
                BindingContext = new AssessmentDetailsViewModel(_dataStore, assessment);

                CheckAssessmentNotifications();
            }
        }


        public AssessmentDetailsPage()
        {
            InitializeComponent();

            _dataStore = DependencyService.Get<IC971DataStore>();
            BindingContext = new AssessmentDetailsViewModel(_dataStore);
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
    }
}
