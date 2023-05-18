using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using C971.Services;
using C971.Views;
using C971.Data;
using C971.ViewModels;

namespace C971
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<SeedDatabaseService>();
            DependencyService.Register<TermRepository>();
            DependencyService.Register<CourseRepository>();
            DependencyService.Register<AssessmentRepository>();    

            MainPage = new AppShell();
        }

        protected async override void OnStart ()
        {
            var seedService = DependencyService.Get<ISeedDatabase>();
            if (seedService != null)
            {
                seedService.Seed();
            }
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}
