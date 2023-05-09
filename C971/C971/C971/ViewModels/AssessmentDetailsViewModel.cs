using C971.Data;
using C971.Models;
using C971.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace C971.ViewModels
{

    public class AssessmentDetailsViewModel : BaseViewModel
    {
        private AssessmentRepository _assessmentRepository;
        private CourseRepository _courseRepository; 

        public int AssessmentId { get; set; }

        public int? CourseId { get; set; }

        public string AssessmentName { get; set; }

        public string AssessmentType { get; set; }

        public DateTime StartDate { get; set; }

        public bool NotifyStartDate { get; set; }

        public DateTime EndDate { get; set; }  
        
        public bool NotifyEndDate { get; set; } 

        public List<string> AssessmentTypes { get; set; }

        public bool LockAssessmentType
        {
            get
            {
                try
                {
                    if (_courseRepository != null)
                    {
                        if (CourseId != null)
                        {
                            var assessments = _assessmentRepository.GetAssessmentsForCourseId(CourseId.Value).Result;
                            if (assessments.Count > 1)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        return true;
                    }
                    return true;
                }
                catch (Exception err)
                {
                    Debug.WriteLine(err.Message);
                    return false;
                }
            }
        }

        public AssessmentDetailsViewModel()
        {
            InitializeRepositories();

            AssessmentName = string.Empty;

            StartDate = DateTime.Today;
            NotifyStartDate = false;
            EndDate = DateTime.Today.AddDays(7);
            NotifyEndDate = false;

            AssessmentTypes = new List<string>
            {
                AssessmentTypeTypes.PERFORMANCE,
                AssessmentTypeTypes.OBJECTIVE
            };
            AssessmentType = AssessmentTypes.First();
        }

        public AssessmentDetailsViewModel(Assessment assessment)
        {
            InitializeRepositories();

            BindAssessmentToViewModel(assessment);
        }

        private void BindAssessmentToViewModel(Assessment assessment)
        {
            AssessmentId = assessment.AssessmentId;
            CourseId = assessment.CourseId;
            AssessmentName = assessment.AssessmentName;
            StartDate = assessment.StartDate.Value;
            NotifyStartDate = assessment.NotifyStartDate;
            EndDate = assessment.EndDate.Value;
            NotifyEndDate = assessment.NotifyEndDate;
            AssessmentTypes = new List<string>
            {
                AssessmentTypeTypes.PERFORMANCE,
                AssessmentTypeTypes.OBJECTIVE,
            };
            AssessmentType = assessment.AssessmentType;
        }

        public void SaveAssessment()
        {
            var assessment = _assessmentRepository.GetByIdAsync(AssessmentId).Result;
            if(assessment != null)
            {
                assessment.AssessmentName = AssessmentName;
                assessment.AssessmentType = AssessmentType;
                assessment.StartDate = StartDate;
                assessment.NotifyStartDate = NotifyStartDate;
                assessment.EndDate = EndDate;
                assessment.NotifyEndDate = NotifyEndDate; 
                
                _assessmentRepository.UpdateAsync(assessment);
            }
        }

        private void InitializeRepositories()
        {
            var dbContext = DependencyService.Get<ISqliteDbContext>();
            _courseRepository = new CourseRepository(dbContext);
            _assessmentRepository = new AssessmentRepository(dbContext);
        }
    }
}