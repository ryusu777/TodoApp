using Library.Models;

namespace ProjectManagement.Domain.Common.ValueObjects
{
	public class Url : ValueObject
	{
        public string Value { get; init; }

		private Url(string uri)
		{
			Value = uri;
		}
        public override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
		public static Url Create(string url) 
		{
			return new(url);
		}
	}
}
