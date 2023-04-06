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

    }
}