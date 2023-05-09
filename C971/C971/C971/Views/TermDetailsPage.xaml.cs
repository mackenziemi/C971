using C971.Models;
using C971.Services;
using C971.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using Plugin.LocalNotifications;
using C971.Data;

namespace C971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("TermId", "termId")]
    public partial class TermDetailsPage : ContentPage
    {
        private TermRepository _termRepository;

        private int _termId;
        public int TermId
        {
            get
            {
                return _termId;
            }
            set
            {
                _termId = value;
                var dbContext = DependencyService.Get<ISqliteDbContext>();
                var termRepo = new TermRepository(dbContext);   
                var term = termRepo.GetByIdAsync(_termId).Result;
                BindingContext = new TermDetailsViewModel(term);

                CheckTermNotifications();
            }
        }

        public TermDetailsPage()
        {
            var dbContext = DependencyService.Get<ISqliteDbContext>();
            _termRepository = new TermRepository(dbContext);    

            InitializeComponent();
            var viewModel = new TermDetailsViewModel();
            BindingContext = viewModel;
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if(e.Item == null)
            {
                throw new InvalidDataException($"There is no data to use to return");
            }

            var course = e.Item as Course;
            await Shell.Current.GoToAsync($"coursedetails?courseId={course.CourseId}");
        }
        private async void AddCourse_Clicked(object sender, System.EventArgs e)
        {
            var viewModel = BindingContext as TermDetailsViewModel;
            if(viewModel != null)
            {
                if(viewModel.Courses.Count < 6)
                {
                    viewModel.AddNewCourse();
                }
                else
                {
                    await DisplayAlert("Error", "Unable to have more than six courses for a given term", "Ok");
                }
                
            }
        }
        private async void RemoveCourse_Clicked(object sender, System.EventArgs e)
        {
            //Walk the UI Tree to get back to the ViewCell
            var button = sender as Button;
            var stackLayout = button.Parent;
            var viewCell = stackLayout.Parent;

            //Get that data that the ViewCell is bound to
            var data = viewCell.BindingContext as Course;

            //Remove the course from the ViewModel
            var viewModel = BindingContext as TermDetailsViewModel;
            await viewModel.RemoveCourse(data);

            //Update the UI
            RebindCourses();
        }

        private void TermDetailsPage_Appearing(object sender, System.EventArgs e)
        {
            RebindCourses();
        }

        private void RebindCourses()
        {
            var viewModel = BindingContext as TermDetailsViewModel;
            ListViewCourses.ItemsSource = null;
            ListViewCourses.ItemsSource = viewModel.Courses;
        }

        private void CheckTermNotifications()
        {
            var viewModel = BindingContext as TermDetailsViewModel;
            if(viewModel != null)
            {
                if(viewModel.NotifyStartDate && viewModel.StartDate <= DateTime.Now)
                {
                    CrossLocalNotifications.Current.Show("Term Start Date",
                        $"Your term {viewModel.TermName} has started");
                }
                if(viewModel.NotifyEndDate && viewModel.EndDate <= DateTime.Now)
                {
                    CrossLocalNotifications.Current.Show("Term End Date",
                        $"Your term {viewModel.TermName} has ended");
                }
            }
        }
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as TermDetailsViewModel;
            if(viewModel != null)
            {
                viewModel.SaveTerm();
                await Shell.Current.Navigation.PopAsync();
            }
        }

        private void NotifyStartDate_OnToggle(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            CheckTermNotifications();
        }

        void NotifyEndDate_OnToggle(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            CheckTermNotifications();
        }
    }
}
