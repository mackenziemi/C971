﻿using C971.Models;
using C971.Services;
using System;
using System.Collections.Generic;

namespace C971.ViewModels
{
    public class CourseDetailsViewModel : BaseViewModel
    {
        private IC971DataStore _dataStore;
        
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

            CourseAssessments = _dataStore.GetAssessments();
        }

    }
}