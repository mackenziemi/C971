using System;
using System.Threading.Tasks;
using C971.Data;
using C971.Models;
using SQLite;
using Xamarin.Forms;

namespace C971.Services
{
    public class SeedDatabaseService : ISeedDatabase
	{
		private ISqliteDbContext _context;

		public SeedDatabaseService()
		{
			_context = DependencyService.Get<ISqliteDbContext>();
		}

        public async void Seed()
        {
            try
            {
                //await _context.GetConnection().DropTableAsync<Term>();
                //await _context.GetConnection().DropTableAsync<Course>();
                //await _context.GetConnection().DropTableAsync<Assessment>();

                var isEmpty = await IsDatabaseEmptyAsync();
                if (isEmpty)
                {
                    var connection = _context.GetConnection();

                    await connection.CreateTableAsync<Assessment>();
                    await connection.CreateTableAsync<Course>();
                    await connection.CreateTableAsync<Term>();

                    var term = new Term
                    {
                        TermName = "Spring 20231",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(180)
                    };

                    var termRepo = new TermRepository(_context);
                    var termId = await termRepo.InsertAsync(term);

                    var course = new Course
                    {
                        CourseName = "C971",
                        StartDate = DateTime.Now,
                        NotifyStartDate = true,
                        EndDate = DateTime.Now.AddDays(45),
                        NotifyEndDate = true,
                        CourseStatus = "In Progress",
                        TermId = termId,
                        InstructorName = "MacKenzie Mickelsen",
                        InstructorPhone = "801-856-1043",
                        InstructorEmail = "mmick13@wgu.edu"
                    };
                   
                    var courseRepo = new CourseRepository(_context);
                    var courseId = await courseRepo.InsertAsync(course);


                    var pa = new Assessment
                    {
                        AssessmentName = "PAL1",
                        AssessmentType = AssessmentTypeTypes.PERFORMANCE,
                        StartDate = DateTime.Now,
                        NotifyStartDate = true,
                        EndDate = DateTime.Now.AddDays(30),
                        NotifyEndDate = true,
                        CourseId = courseId
                    };
                    var oa = new Assessment
                    {
                        AssessmentName = "PAS2",
                        AssessmentType = AssessmentTypeTypes.OBJECTIVE,
                        StartDate = DateTime.Now.AddDays(31),
                        NotifyStartDate = false,
                        EndDate = DateTime.Now.AddDays(60),
                        NotifyEndDate = false,
                        CourseId = courseId
                    };
                    var assessmentRepo = new AssessmentRepository(_context);
                    var paId = await assessmentRepo.InsertAsync(pa);
                    var oaId = await assessmentRepo.InsertAsync(oa);


                    var termCount = await connection.Table<Term>().CountAsync();
                    var courseCount = await connection.Table<Course>().CountAsync();
                    var assessmentCount = await connection.Table<Assessment>().CountAsync();

                    var termFromDb = await termRepo.GetByIdAsync(termId);
                    var courseFromDb = await courseRepo.GetCoursesForTermId(termId); 
                    var assessmentsFromDb = await assessmentRepo.GetAssessmentsForCourseId(courseId);

                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

		public async Task<bool> IsDatabaseEmptyAsync()
		{
            try
            {
                long count = await _context.GetConnection().
                    Table<Term>()
                    .CountAsync();
                return count == 0;
            }
            catch(SQLiteException err)
            {
                if(err.Message.Contains("no such table"))
                {
                    return true;
                }
                return false;
            }

        }
    }
}

