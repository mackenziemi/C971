using C971.Models;
using C971.ViewModels;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseDetailsPage : ContentPage
    {
        public CourseDetailsPage()
        {
            InitializeComponent();
            BindingContext = new CourseDetailsViewModel();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            var assessment = e.Item as Assessment;
            Debug.WriteLine($"{assessment.AssessmentName} has been tapped!");
        }
    }
}
