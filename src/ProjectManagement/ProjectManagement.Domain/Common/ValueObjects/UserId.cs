using Library.Models;

namespace ProjectManagement.Domain.Common.ValueObjects
{
	public class UserId : ValueObject
	{
        public string Value { get; init; }

		private UserId(string username)
		{
			Value = username;
		}
        public override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
		public static UserId Create(string username) 
		{
			return new(username);
		}
	}
}
