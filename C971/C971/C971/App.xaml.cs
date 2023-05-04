using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using C971.Services;
using C971.Views;
using C971.Data;

namespace C971
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<SeedDatabaseService>();

            MainPage = new AppShell();
        }

        protected async override void OnStart ()
        {
            DependencyService.Register<IC971DataStore, C971DataStore>();

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
