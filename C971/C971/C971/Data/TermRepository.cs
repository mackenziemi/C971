using C971.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C971.Data
{
    public class TermRepository : IRepository<Term>
    {
        private readonly SQLiteAsyncConnection _db;

        public TermRepository(ISqliteDbContext context)
        {
            _db = context.GetConnection();
        }

        public Task<List<Term>> GetAllAsync()
        {
            return _db.Table<Term>().ToListAsync();
        }

        public Task<Term> GetByIdAsync(int id)
        {
            return _db.Table<Term>().FirstOrDefaultAsync(x=>x.TermId == id);
        }

        public Task<int> InsertAsync(Term entity)
        {
            return _db.InsertAsync(entity);
        }

        public Task<int> UpdateAsync(Term entity)
        {
            return _db.UpdateAsync(entity);
        }

        public Task<int> DeleteAsync(Term entity)
        {
            return _db.DeleteAsync(entity);
        }
    }
}

