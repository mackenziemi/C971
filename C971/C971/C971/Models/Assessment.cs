﻿using C971.Enums;
using SQLite;
using System;

namespace C971.Models
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int AssessmentId { get; set; }
        public int CourseId { get; set; }
        public string AssessmentName { get; set; }
        public string AssessmentType { get; set; }  
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set;}
        public bool NotifyStartDate { get; set; } = false;
        public bool NotifyEndDate { get; set; } = false;

        [Ignore]
        public string DateRangeString
        {
            get
            {
                if(StartDate != null && EndDate != null)
                {
                    return $"{StartDate.Value.ToString("MMM dd, yyyy")} - {EndDate.Value.ToString("MMM dd, yyyy")}";
                }
                else
                {
                    return "Not Available";
                }
            }
        }

    }
}