using Repository.Interfaces;
using Repository.Models;
using System.Threading.Tasks;

namespace Repository.Services
{
	public class UserRepositoryService : IUserRepository
	{
		public Task<User> GetUser(long id)
		{
			// just random, since task doesn't require this implemented
			// most probably would have used entity framework for this
			var fakeUser = User.FakeUser.Generate();

			fakeUser.Key = (int)id;

			return Task.FromResult(fakeUser);
		}
	}
}