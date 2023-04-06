using C971.Models;
using System.Collections.Generic;

namespace C971.ViewModels
{
    public class TermsViewModel: BaseViewModel
    {
        public List<Term> Terms { get; set; }

        public TermsViewModel()
        {
            Terms = new List<Term>();
        }

    }
}