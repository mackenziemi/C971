using C971.Models;
using System;
using System.Collections.Generic;

namespace C971.ViewModels
{
    public class TermDetailsViewModel : BaseViewModel
    {
        public string TermName { get; set; }
        public DateTime StartDate { get; set; }
        public bool NotifyStartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool NotifyEndDate { get; set; }

        public List<Course> Courses { get; set; }

        public TermDetailsViewModel()
        {
            TermName = string.Empty;
            StartDate = DateTime.Today;
            NotifyStartDate = false;
            EndDate = DateTime.Today.AddDays(7);
            NotifyEndDate = false;
            Courses = new List<Course>();
        }

        public TermDetailsViewModel(Term term)
        {
            TermName = term.TermName;
            StartDate = term.StartDate.Value;
            NotifyStartDate = term.NotifyStartDate;
            EndDate = term.EndDate.Value;
            NotifyEndDate = term.NotifyEndDate;
            Courses = term.Courses;

        }
    }
}