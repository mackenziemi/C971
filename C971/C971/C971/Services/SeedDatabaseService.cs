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

                var isEmpty = await IsDatabaseEmptyAsync();
                if (isEmpty)
                {
                    var connection = _context.GetConnection();

                    await connection.CreateTableAsync<Assessment>();
                    await connection.CreateTableAsync<Course>();
                    await connection.CreateTableAsync<Term>();
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

