using C971.Models;
using System;
using System.Collections.Generic;

namespace C971.ViewModels
{
    public class CourseDetailsViewModel : BaseViewModel
    {
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

        public CourseDetailsViewModel() 
        {
            CourseStatuses = new List<string>
            {
                "In Progress",
                "Completed",
                "Plan to take"
            };

            CourseAssessments = new List<Assessment>();

            CourseName = "This is a test course";
            Notes = string.Empty;

            CourseAssessments = new List<Assessment>
            {
                new Assessment
                {
                    AssessmentName ="LAP1",
                    AssessmentType ="Performance Assessment",
                    StartDate = new DateTime(2023, 1, 1),
                    EndDate = new DateTime(2023,1,31)
                },
                new Assessment
                {
                    AssessmentName ="LSP2",
                    AssessmentType ="Objective Assessment",
                    StartDate = new DateTime(2023, 2, 1),
                    EndDate = new DateTime(2023,2,14)
                },
            };
        }

        public CourseDetailsViewModel(Course course)
        {
            CourseName = course.CourseName;
            StartDate = course.StartDate.Value;
            EndDate = course.EndDate.Value;
            NotifyStartDate = course.NotifyStartDate;
            NotifyEndDate = course.NotifyEndDate;
            CourseStatus = course.CourseStatus;
            InstructorName = course.InstructorName;
            InstructorPhone = course.InstructorPhone;
            Notes = course.Notes;

            CourseStatuses = new List<string>
            {
                "In Progress",
                "Completed",
                "Plan to take"
            };

            CourseAssessments = course.Assessments;
        }

    }
}