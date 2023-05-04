using C971.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C971.Data
{
    public class AssessmentRepository : IRepository<Assessment>
    {
        private readonly SQLiteAsyncConnection _db;

        public AssessmentRepository(ISqliteDbContext context)
        {
            _db = context.GetConnection();
        }

        public Task<List<Assessment>> GetAllAsync()
        {
            return _db.Table<Assessment>().ToListAsync();
        }

        public Task<Assessment> GetByIdAsync(int id)
        {
            return _db.Table<Assessment>().FirstOrDefaultAsync(x => x.AssessmentId == id);
        }

        public Task<List<Assessment>> GetAssessmentsForCourseId(int courseId)
        {
            return _db.Table<Assessment>().Where(x => x.CourseId == courseId).ToListAsync();
        }

        public Task<int> InsertAsync(Assessment entity)
        {
            return _db.InsertAsync(entity);
        }

        public Task<int> UpdateAsync(Assessment entity)
        {
            return _db.UpdateAsync(entity);
        }

        public Task<int> DeleteAsync(Assessment entity)
        {
            return _db.DeleteAsync(entity);
        }
    }
}

