using C971.Data;
using C971.Models;
using C971.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace C971.ViewModels
{
    public class CourseDetailsViewModel : BaseViewModel
    {
        private CourseRepository _courseRepository;
        private AssessmentRepository _assessmentRepository;
        
        public int CourseId { get; set; }  
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public bool NotifyStartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool NotifyEndDate { get; set; }
        public string CourseStatus { get; set; }
        public string InstructorName { get; set; } 
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        public string Notes { get; set; }

        public List<Assessment> Assessments { get; set; }

        public List<string> CourseStatuses { get; set; }

        public CourseDetailsViewModel()
        {
            InitializeRepositories();

            CourseStatuses = new List<string>
            {
                CourseStatusTypes.IN_PROGRESS,
                CourseStatusTypes.COMPLETED,
                CourseStatusTypes.PLAN_TO_TAKE
            };

            Assessments = new List<Assessment>();

            CourseName = "This is a test course";
            Notes = string.Empty;

            Assessments = new List<Assessment>();
        }

       

        public CourseDetailsViewModel(Course course)
        {
            InitializeRepositories();

            CourseStatuses = new List<string>
            {
               CourseStatusTypes.IN_PROGRESS,
               CourseStatusTypes.COMPLETED,
               CourseStatusTypes.PLAN_TO_TAKE
            };

            Assessments = _assessmentRepository.GetAssessmentsForCourseId(course.CourseId).Result;
            BindCourseToViewModel(course);
        }

        private void BindCourseToViewModel(Course course)
        {
            CourseId = course.CourseId;
            CourseName = course.CourseName;
            StartDate = course.StartDate.Value;
            NotifyStartDate = course.NotifyStartDate;
            EndDate = course.EndDate.Value;
            NotifyEndDate = course.NotifyEndDate;
            CourseStatus = course.CourseStatus;
            InstructorName = course.InstructorName;
            InstructorPhone = course.InstructorPhone;
            InstructorEmail = course.InstructorEmail;
            Notes = course.Notes;

            Assessments = _assessmentRepository.GetAssessmentsForCourseId(course.CourseId).Result;
        }

        public async void SaveCourse()
        {
            var course = _courseRepository.GetByIdAsync(CourseId).Result;
            if(course != null)
            {
                course.CourseName = CourseName;
                course.StartDate = StartDate;
                course.NotifyStartDate = NotifyStartDate;
                course.EndDate = EndDate;
                course.NotifyEndDate = NotifyEndDate;
                course.CourseStatus = CourseStatus;
                course.InstructorName = InstructorName;
                course.InstructorPhone = InstructorPhone;
                course.InstructorEmail = InstructorEmail;
                course.Notes = Notes;

                await _courseRepository.UpdateAsync(course);
            }
        }

        public async void AddFixedNewAssessment(string requiredAssessmentType)
        {
            var course = await _courseRepository.GetByIdAsync(CourseId);
            if(course != null)
            {
                var assessment = course.AddFixedNewAssessment(requiredAssessmentType);
                var newAssessmentId = await _assessmentRepository.InsertAsync(assessment);
                assessment.AssessmentId = newAssessmentId;

                Assessments = await _assessmentRepository.GetAssessmentsForCourseId(CourseId);

                await Shell.Current.GoToAsync($"assessmentdetails?assessmentId={assessment.AssessmentId}");
            }   
        }

        public async void AddNewAssessment()
        {
            var course = _courseRepository.GetByIdAsync(CourseId).Result;
            if (course != null)
            {
                var assessment = course.AddNewAssessment();
                assessment.CourseId = course.CourseId;
                var newAssessmentId = await _assessmentRepository.InsertAsync(assessment);  
                assessment.AssessmentId = newAssessmentId;

                Assessments = await _assessmentRepository.GetAssessmentsForCourseId(CourseId);

                await Shell.Current.GoToAsync($"assessmentdetails?assessmentId={assessment.AssessmentId}");
            }
        }

        public async Task<int> RemoveAssessment(Assessment assessment)
        {
            await _assessmentRepository.DeleteAsync(assessment);
            Assessments = await _assessmentRepository.GetAssessmentsForCourseId(CourseId);
            return await Task.FromResult(0);
        }

        private void InitializeRepositories()
        {
            var dbContext = DependencyService.Get<ISqliteDbContext>();
            _courseRepository = new CourseRepository(dbContext);
            _assessmentRepository = new AssessmentRepository(dbContext);
        }
    }
}