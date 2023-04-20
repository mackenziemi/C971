using C971.Commands;
using C971.Models;
using C971.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace C971.ViewModels
{
    public class TermDetailsViewModel : BaseViewModel
    {
        private IC971DataStore _dataStore;


        public int TermId { get; set; }
        public string TermName { get; set; }
        public DateTime StartDate { get; set; }
        public bool NotifyStartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool NotifyEndDate { get; set; }

        public ObservableCollection<Course> Courses { get; set; }


        public ICommand AddNewCourseCommand { get; private set; }

        public TermDetailsViewModel(IC971DataStore dataStore)
        {
            _dataStore = dataStore;

            var term = _dataStore.GetTermById(1);

            BindTermToViewModel(term);
            AddNewCourseCommand = new AddNewCourseCommand();
        }

        public TermDetailsViewModel(Term term)
        {
            BindTermToViewModel(term);
            AddNewCourseCommand = new AddNewCourseCommand();
        }

        private void BindTermToViewModel(Term term)
        {
            TermId = term.TermId;
            TermName = term.TermName;
            StartDate = term.StartDate.Value;
            NotifyStartDate = term.NotifyStartDate;
            EndDate = term.EndDate.Value;
            NotifyEndDate = term.NotifyEndDate;
            Courses = new ObservableCollection<Course>(term.Courses);
        }

        public async void AddNewCourse()
        {
            var term = _dataStore.GetTermById(TermId);
            if(term != null)
            {
                if(term.Courses.Count <6)
                {
                    var newCourseId = _dataStore.GetCourses().Count + 1;
                    var newCourse = term.AddNewCourse(newCourseId);
                    Courses.Add(newCourse);

                    await Shell.Current.GoToAsync($"coursedetails?courseId={newCourse.CourseId}");
                }
                else
                {
                    throw new Exception($"Unable to add more than six courses to a given term.");
                }
            }
        }   

        public async void RemoveCourse(Course course)
        {
            var term = _dataStore.GetTermById(TermId);
            if(term != null)
            {
                term.Courses.Remove(course);
                Courses.Remove(course);
            }
        }

    }
}