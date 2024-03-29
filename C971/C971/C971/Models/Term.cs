﻿using SQLite;
using System;
using System.Collections.Generic;

namespace C971.Models
{
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int TermId { get; set; }
        public string TermName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool NotifyStartDate { get; set; } = false;
        public bool NotifyEndDate { get; set; } = false;

        public List<Course> Courses { get; set; } = new List<Course>();

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

        public Course AddNewCourse()
        {
            var newCourse = new Course
            {
                TermId = TermId,
                CourseName = "New Course",
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(30),   
            };

            Courses.Add(newCourse); 
            return newCourse;
        }
    }
}