using C971.Enums;
using System;

namespace C971.Models
{
    public class Assessment
    {
        public int AssessmentId { get; set; }
        public string Name { get; set; }
        public AssessmentTypes Types { get; set; }  
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set;}
        public bool NotifyStartDate { get; set; } = false;
        public bool NotifyEndDate { get; set; } = false;

    }
}