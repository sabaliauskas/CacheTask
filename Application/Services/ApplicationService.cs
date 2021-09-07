using Application.Interfaces;
using Application.Models;
using Repository.Interfaces;
using System.Threading.Tasks;

namespace Application.Services
{
	public class ApplicationService : IApplicationService
	{
		private readonly IUserRepository _userRep;

		public ApplicationService(IUserRepository userRepository)
		{
			_userRep = userRepository;
		}

		public async Task<UserModel> GetUser(long userID)
		{
			//check cache
			//if available, return cache
			//else
			//RefreshCacheForUser
			//
			var result = await _userRep.GetUser(userID);

			if (result != null)
				return new UserModel(result);

			return null;
		}
	}
}