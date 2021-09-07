using Repository.Models;
using System;

namespace Application.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Address { get; set; }
		public string FullName { get; set; }

		public UserModel()
		{

		}
		public UserModel(User user)
		{
			Id = user.Key;
			UserName = user.UserName;
			FullName = $"{user.FirstName} {user.LastName}";
			Address = user.Address;
		}
	}
}