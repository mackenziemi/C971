using C971.ViewModels;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentDetailsPage : ContentPage
    {
        public AssessmentDetailsPage()
        {
            InitializeComponent();
            BindingContext = new AssessmentDetailsViewModel();
        }
    }
}
