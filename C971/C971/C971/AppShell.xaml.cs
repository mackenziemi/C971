using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using C971.Models;
using C971.ViewModels;
using C971.Views;
using Xamarin.Forms;

namespace C971
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(AssessmentDetailsPage), typeof(AssessmentDetailsPage));

            Routing.RegisterRoute("assessmentdetails", typeof(AssessmentDetailsPage));
            Routing.RegisterRoute("coursedetails", typeof(CourseDetailsPage));
            Routing.RegisterRoute("termdetails", typeof(TermDetailsPage));
            Routing.RegisterRoute("terms", typeof(TermsPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);
        }

    }
}
