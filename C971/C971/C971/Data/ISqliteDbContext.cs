using System;
using SQLite;

namespace C971.Data
{
    public interface ISqliteDbContext
	{
        SQLiteAsyncConnection GetConnection();
    }
}

