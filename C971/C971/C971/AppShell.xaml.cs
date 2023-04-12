using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using C971.Models;
using C971.ViewModels;
using C971.Views;
using Newtonsoft.Json;
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
            Routing.RegisterRoute("termDetails", typeof(TermDetailsPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);

            if(args.Current?.Location.OriginalString.StartsWith("assessmentDetails")== true)
            {
                Debug.WriteLine($"Got this far...t");


            }
        }

    }
}
