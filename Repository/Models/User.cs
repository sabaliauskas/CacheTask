using Bogus;
using System;

namespace Repository.Models
{
	public class User
	{
		private static int _userId = 100;
		public int Key { get; set; }
		public string UserName { get; set; }
		public DateTime CreatedAt { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }

		public static Faker<User> FakeUser { get; } =
			new Faker<User>()
				.RuleFor(u => u.Key, f => _userId++)
				.RuleFor(u => u.UserName, f => f.Internet.UserName())
				.RuleFor(u => u.FirstName, f => f.Name.FirstName())
				.RuleFor(u => u.LastName, f => f.Name.LastName())
				.RuleFor(u => u.Address, f => f.Address.Random.Words(3))
			;
	}
}