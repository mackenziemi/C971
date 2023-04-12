using C971.Models;
using C971.Services;
using System;
using System.Collections.Generic;

namespace C971.ViewModels
{
    public class TermDetailsViewModel : BaseViewModel
    {
        private IC971DataStore _dataStore;

        public string TermName { get; set; }
        public DateTime StartDate { get; set; }
        public bool NotifyStartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool NotifyEndDate { get; set; }

        public List<Course> Courses { get; set; }

        public TermDetailsViewModel(IC971DataStore dataStore)
        {
            _dataStore = dataStore;

            var term = _dataStore.GetTermById(1);

            TermName = term.TermName;
            StartDate = term.StartDate.Value;
            NotifyStartDate = term.NotifyStartDate;
            EndDate = term.EndDate.Value;
            NotifyEndDate = term.NotifyEndDate;
            Courses = term.Courses;
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