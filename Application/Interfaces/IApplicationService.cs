using Application.Models;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IApplicationService
	{
		Task<UserModel> GetUser(long userID);
	}
}