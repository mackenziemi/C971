using C971.Data;
using C971.Models;
using C971.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace C971.ViewModels
{
    public class TermDetailsViewModel : BaseViewModel
    {
        private TermRepository _termRepository;
        private CourseRepository _courseRepository;


        public int TermId { get; set; }
        public string TermName { get; set; }
        public DateTime StartDate { get; set; }
        public bool NotifyStartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool NotifyEndDate { get; set; }

        public ObservableCollection<Course> Courses { get; set; }



        public TermDetailsViewModel()
        {
            //BindTermToViewModel(new Term());
        }

        public TermDetailsViewModel(Term term)
        {
            var dbContext = DependencyService.Get<ISqliteDbContext>();
            _termRepository = new TermRepository(dbContext);
            _courseRepository = new CourseRepository(dbContext);

            term.Courses = _courseRepository.GetCoursesForTermId(term.TermId).Result;

            BindTermToViewModel(term);
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
            var term = await _termRepository.GetByIdAsync(TermId);
            term.Courses = await _courseRepository.GetCoursesForTermId(term.TermId);
            {
                if(term.Courses.Count <6)
                {
                    var newCourse = term.AddNewCourse();
                    var courseId = await _courseRepository.InsertAsync(newCourse);
                    newCourse.CourseId = courseId;
                    Courses.Add(newCourse);

                    await Shell.Current.GoToAsync($"coursedetails?courseId={newCourse.CourseId}");
                }
            }
        }   

        public async Task<int> RemoveCourse(Course course)
        {
            var term = await _termRepository.GetByIdAsync(TermId);
            if(term != null)
            {
                await _courseRepository.DeleteAsync(course);
                term.Courses = await _courseRepository.GetCoursesForTermId(term.TermId);

                Courses = new ObservableCollection<Course>(term.Courses);
            }
            return await Task.FromResult(0);
        }

        public async void SaveTerm()
        {
            var term = await _termRepository.GetByIdAsync(TermId);
            if(term != null)
            {
                term.TermName = TermName;
                term.StartDate = StartDate;
                term.NotifyStartDate = NotifyStartDate;
                term.EndDate = EndDate; 
                term.NotifyEndDate = NotifyEndDate;
            }
            await _termRepository.UpdateAsync(term);
        }

    }
}