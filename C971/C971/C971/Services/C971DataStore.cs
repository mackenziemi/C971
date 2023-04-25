using C971.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace C971.Services
{
    public class C971DataStore : IC971DataStore
    { 
        private List<Term> terms = new List<Term>();

        public C971DataStore()
        {
            CreateSeedData();
        }

        public Assessment GetAssessmentById(int id)
        {
            return GetAssessments().Find(a => a.AssessmentId == id);
        }

        public List<Assessment> GetAssessments()
        {
            var result = new List<Assessment>();

            foreach (var term in terms)
            {
                foreach (var course in term.Courses)
                {
                    foreach (var assessment in course.Assessments)
                    {
                        result.Add(assessment);
                    }
                }
            }

            return result;
        }

        public Course GetCourseById(int id)
        {
            return GetCourses().Find(a => a.CourseId == id);
        }

        public List<Course> GetCourses()
        {
            var result = new List<Course>();

            foreach (var term in terms)
            {
                foreach (var course in term.Courses)
                {
                    result.Add(course); 
                }
            }

            return result;
        }

        public Term GetTermById(int id)
        {
            return terms.Find(x=>x.TermId == id);
        }

        public List<Term> GetTerms()
        {
            return terms;
        }

        private void CreateSeedData()
        {
            var term = new Term
            {
                TermId = 1,
                TermName = "Winter 2023",
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2023, 6, 30),
                NotifyStartDate = false,
                NotifyEndDate = true
            };
            var c971 = new Course
            {
                CourseId = 1,
                CourseName = "C971",
                CourseStatus = "In Progress",
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2023, 1, 31),
                InstructorName = "John Smith",
                InstructorPhone = "555-555-5555",
                InstructorEmail = "j.smith@byteme.io"
            };
            var d191 = new Course
            {
                CourseId = 2,
                CourseName = "D191",
                CourseStatus = "Plan to take",
                StartDate = new DateTime(2023, 3, 1),
                EndDate = new DateTime(2023, 3, 31),
                InstructorName = "John Smith",
                InstructorPhone = "555-555-5555",
                InstructorEmail = "j.smith@byteme.io"
            };

            var lap1 = new Assessment
            {
                AssessmentId = 1,
                AssessmentName = "LAP1",
                AssessmentType = "Performance Assessment",
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2023, 1, 31)
            };
            var lsp2 = new Assessment
            {
                AssessmentId = 2,
                AssessmentName = "LSP2",
                AssessmentType = "Objective Assessment",
                StartDate = new DateTime(2023, 2, 1),
                EndDate = new DateTime(2023, 2, 14)
            };
            var ldp2 = new Assessment
            {
                AssessmentId = 3,
                AssessmentName = "LDP2",
                AssessmentType = "Objective Assessment",
                StartDate = new DateTime(2023, 3, 1),
                EndDate = new DateTime(2023, 3, 28)
            };

            c971.Assessments.AddRange(new List<Assessment> { lap1, ldp2 });
            d191.Assessments.AddRange(new List<Assessment> { lsp2 });   

            term.Courses.Add(c971);
            term.Courses.Add(d191);

            //courses.AddRange(new List<Course> { c971, d191 });

            terms.Add(term);
        }

    }
}
