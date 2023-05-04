using System;
using System.IO;
using C971.Data;
using C971.Droid.Data;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidSqliteDbContext))]
namespace C971.Droid.Data
{

    public class AndroidSqliteDbContext: ISqliteDbContext
	{
        public AndroidSqliteDbContext() { }

        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "C971.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}

