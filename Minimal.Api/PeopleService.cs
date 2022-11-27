using System;
namespace Minimal.Api
{
	public record Person(string FullName);
	public class PeopleService
	{
		private readonly List<Person> _people = new()
		{
			new Person("Sean Livingston"),
			new Person("Cecelia Livingston"),
			new Person("Nicole Livingston")
		};

		public IEnumerable<Person> Search(string searchTerm)
		{
			return _people.Where(x => x.FullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
		}
		public PeopleService()
		{
		}
	}
}

