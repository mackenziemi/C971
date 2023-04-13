using C971.Models;
using System.Collections.Generic;

namespace C971.Services
{
    public interface IC971DataStore
    {
        Assessment GetAssessmentById(int id);
        List<Assessment> GetAssessments();

        Course GetCourseById(int id);
        List<Course> GetCourses();

        Term GetTermById(int id);
        List<Term> GetTerms();


    }
}
