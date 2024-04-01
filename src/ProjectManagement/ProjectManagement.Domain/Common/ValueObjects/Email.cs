using Library.Models;

namespace ProjectManagement.Domain.Common.ValueObjects
{
	public class Email : ValueObject
	{
        public string Value { get; init; }

		private Email(string email)
		{
			Value = email;
		}
        public override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
		public static Email Create(string email) 
		{
			return new(email);
		}
	}
}
