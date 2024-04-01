using Library.Models;

namespace ProjectManagement.Domain.Project.ValueObjects
{
	public class ProjectId : ValueObject
	{
        public string Value { get; init; }

		private ProjectId(string code)
		{
			Value = code;
		}

        public override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
		public static ProjectId Create(string code) 
		{
			return new(code);
		}
	}
}
