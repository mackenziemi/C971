using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace C971.Models
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int CourseId { get; set; }
        public int TermId { get; set; }
        public string CourseName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool NotifyStartDate { get; set; } = false;
        public bool NotifyEndDate { get; set; } = false;
        public string CourseStatus { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        public string Notes { get; set; }

        [Ignore]
        public List<Assessment> Assessments { get; set; } = new List<Assessment>();


        [Ignore]
        public string DateRangeString
        {
            get
            {
                if (StartDate != null && EndDate != null)
                {
                    return $"{StartDate.Value.ToString("MMM dd, yyyy")} - {EndDate.Value.ToString("MMM dd, yyyy")}";
                }
                else
                {
                    return "Not Available";
                }
            }
        }

        public Assessment AddFixedNewAssessment(int assessmentId, string requiredAssessmentType)
        {
            var newAssessment = new Assessment
            {
                AssessmentId = assessmentId,
                CourseId = CourseId,
                AssessmentName = "New Assessment",
                AssessmentType = requiredAssessmentType,
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(30),
            };

            Assessments.Add(newAssessment);
            return newAssessment;
        }

        public Assessment AddNewAssessment(int assessmentId)
        {
            var newAssessment = new Assessment
            {
                AssessmentId = assessmentId,
                CourseId = CourseId,
                AssessmentName = "New Assessment",
                AssessmentType = AssessmentTypeTypes.PERFORMANCE,
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(30),
            };

            Assessments.Add(newAssessment);
            return newAssessment;
        }

    }
}