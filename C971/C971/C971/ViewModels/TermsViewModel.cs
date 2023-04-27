using C971.Models;
using C971.Services;
using System;
using System.Collections.Generic;

namespace C971.ViewModels
{
    public class TermsViewModel: BaseViewModel
    {
        private IC971DataStore _dataStore;

        public List<Term> Terms { get; set; }

        public TermsViewModel(IC971DataStore dataStore)
        {
            _dataStore = dataStore;

            Terms = _dataStore.GetTerms();
        }

        public void RemoveTerm(Term term)
        {
            _dataStore.RemoveTerm(term);
            Terms.Remove(term);
        }

        public void AddNewTerm()
        {
            var newTermId = _dataStore.GetTerms().Count + 1;
            var newTerm = new Term
            {
                TermId = newTermId,
                TermName = "New Term",
                StartDate = DateTime.Today.AddDays(1),
                NotifyStartDate = false,
                EndDate = DateTime.Today.AddMonths(6),
                NotifyEndDate = false
            };
            _dataStore.AddTerm(newTerm);
        }
    }
}