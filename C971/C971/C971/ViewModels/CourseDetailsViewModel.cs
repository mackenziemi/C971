using C971.Models;
using C971.Services;
using System;
using System.Collections.Generic;

namespace C971.ViewModels
{
    public class CourseDetailsViewModel : BaseViewModel
    {
        private IC971DataStore _dataStore;
        
        private int CourseId { get; set; }  
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

        public List<Assessment> CourseAssessments { get; set; }

        public List<string> CourseStatuses { get; set; }

        public CourseDetailsViewModel(IC971DataStore dataStore) 
        {
            _dataStore = dataStore;

            CourseStatuses = new List<string>
            {
                "In Progress",
                "Completed",
                "Plan to take"
            };

            CourseAssessments = new List<Assessment>();

            CourseName = "This is a test course";
            Notes = string.Empty;

            //CourseAssessments = _dataStore.GetAssessments();
            CourseAssessments = new List<Assessment>();
        }

        public CourseDetailsViewModel(IC971DataStore dataStore, Course course)
        {
            _dataStore = dataStore;

            CourseStatuses = new List<string>
            {
                "In Progress",
                "Completed",
                "Plan to take"
            };

            CourseAssessments = new List<Assessment>();

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

            CourseAssessments = course.Assessments;
        }  
        
        public void SaveCourse()
        {
            var course = _dataStore.GetCourseById(CourseId);
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
            }
        }

    }
}