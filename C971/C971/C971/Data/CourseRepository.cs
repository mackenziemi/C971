using C971.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C971.Data
{
    public class CourseRepository : IRepository<Course>
    {
        private readonly SQLiteAsyncConnection _db;

        public CourseRepository(ISqliteDbContext context)
        {
            _db = context.GetConnection();
        }

        public Task<List<Course>> GetAllAsync()
        {
            return _db.Table<Course>().ToListAsync();
        }

        public Task<Course> GetByIdAsync(int id)
        {
            return _db.Table<Course>().FirstOrDefaultAsync(x => x.CourseId == id);
        }

        public Task<List<Course>> GetCoursesForTermId(int termId)
        {
            return _db.Table<Course>().Where(x=>x.TermId == termId).ToListAsync();
        }

        public Task<int> InsertAsync(Course entity)
        {
            return _db.InsertAsync(entity);
        }

        public Task<int> UpdateAsync(Course entity)
        {
            return _db.UpdateAsync(entity);
        }

        public Task<int> DeleteAsync(Course entity)
        {
            return _db.DeleteAsync(entity);
        }
    }
}

