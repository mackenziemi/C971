using C971.Models;
using System.Collections.Generic;

namespace C971.Services
{
    public interface IC971DataStore
    {
        Assessment GetAssessmentById(int id);
        List<Assessment> GetAssessments();

        Term GetTermById(int id);
        List<Term> GetTerms();


    }
}
