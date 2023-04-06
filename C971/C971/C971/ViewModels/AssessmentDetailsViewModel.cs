using C971.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace C971.ViewModels
{

    public class AssessmentDetailsViewModel : BaseViewModel
    {
        public string AssessmentName { get; set; }

        public string AssessmentType { get; set; }

        public DateTime StartDate { get; set; }

        public bool NotifyStartDate { get; set; }

        public DateTime EndDate { get; set; }  
        
        public bool NotifyEndDate { get; set; } 

        public List<string> AssessmentTypes { get; set; }

        public AssessmentDetailsViewModel()
        {
            AssessmentName = string.Empty;

            StartDate = DateTime.Today;
            NotifyStartDate = false;    
            EndDate = DateTime.Today.AddDays(7);
            NotifyEndDate = false;

            AssessmentTypes = new List<string>
            {
                "Performance Assessment",
                "Objective Assessment"
            };
            AssessmentType = AssessmentTypes.First();
        }

        public AssessmentDetailsViewModel(Assessment assessment)
        {
            AssessmentName = assessment.AssessmentName;
            StartDate = assessment.StartDate.Value;
            NotifyStartDate = assessment.NotifyStartDate;
            EndDate = assessment.EndDate.Value;
            NotifyEndDate = assessment.NotifyEndDate;
            AssessmentTypes = new List<string>
            {
                "Performance Assessment",
                "Objective Assessment"
            };
            AssessmentType = assessment.AssessmentType;
        }
    }
}