using C971.Models;
using C971.Services;
using C971.ViewModels;
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
    }
}
