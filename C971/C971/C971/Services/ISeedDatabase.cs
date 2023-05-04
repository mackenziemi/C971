using System.Threading.Tasks;

namespace C971.Services
{
    public interface ISeedDatabase
	{
		void Seed();
        Task<bool> IsDatabaseEmptyAsync();

    }
}

