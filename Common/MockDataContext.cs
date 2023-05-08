using Bogus;

namespace Common
{
    public static class MockDataContext
    {
        public static Task<List<Person>> GetMockPeople(int quantity = 5000)
        {
            var faker = new Faker<Person>()
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.FullName, f => f.Name.FullName())
                .RuleFor(p => p.Email, (f, p) => f.Internet.Email(p.FullName.ToLower()))
                .RuleFor(p => p.Age, f => f.Random.Int(18, 80))
                .RuleFor(p => p.Birth, f => f.Date.Past(100, DateTime.UtcNow.AddYears(-18)));

            return Task.FromResult(faker.Generate(quantity));
        }
    }

    public class Person
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime Birth { get; set; }
    }
}