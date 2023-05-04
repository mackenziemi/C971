using C971.Data;
using C971.Models;
using C971.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace C971.ViewModels
{
    public class TermsViewModel: BaseViewModel
    {
        private TermRepository _termRepository;

        public List<Term> Terms { get; set; }

        public TermsViewModel()
        {
            var context = DependencyService.Get<ISqliteDbContext>();
            _termRepository = new TermRepository(context);

            Terms = _termRepository.GetAllAsync().Result;
        }

        public async void RemoveTerm(Term term)
        {
            _ = _termRepository.DeleteAsync(term).Result;
            Terms = _termRepository.GetAllAsync().Result;
        }

        public async void AddNewTerm()
        {
            var newTerm = new Term
            {
                TermName = "New Term",
                StartDate = DateTime.Today.AddDays(1),
                NotifyStartDate = false,
                EndDate = DateTime.Today.AddMonths(6),
                NotifyEndDate = false
            };
            _ = _termRepository.InsertAsync(newTerm).Result;
            Terms = _termRepository.GetAllAsync().Result;
        }

        public async void RefreshTerms()
        {
            Terms = _termRepository.GetAllAsync().Result;
        }
    }
}