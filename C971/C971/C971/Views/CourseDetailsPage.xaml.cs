using C971.Models;
using C971.Services;
using C971.ViewModels;
using Newtonsoft.Json;
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
    }
}
