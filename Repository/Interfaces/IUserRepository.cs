using Repository.Models;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
	public interface IUserRepository
	{
		public Task<User> GetUser(long id);
	}
}