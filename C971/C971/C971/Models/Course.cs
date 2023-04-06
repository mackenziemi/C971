﻿using SQLite;
using System;
using System.Collections.Generic;

namespace C971.Models
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int CourseId { get; set; }
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

        public List<Assessment> Assessments { get; set; } = new List<Assessment>();
    }
}