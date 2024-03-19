using Core.Models;

namespace Core.BogusRepositories;

internal class Storage
{
    private readonly IReadOnlyList<Person> persons;
    private readonly IReadOnlyList<Note> notes;


    public Storage()
    {
        var fp = new Bogus.Faker<Person>()
            .CustomInstantiator(x => new Person(
                new PersonId(x.IndexFaker + 1),
                x.Person.FirstName,
                x.Person.LastName));

        persons = fp.Generate(100);
        var personsIds = persons.Select(x => x.PersonId).ToList();

        var fn = new Bogus.Faker<Note>().CustomInstantiator(
            x => new Note(
                new NoteId(x.IndexFaker + 1),
                x.PickRandom(personsIds),
                x.Lorem.Text(),
                x.Date.Between(
                    new DateTime(1990, 1, 1),
                    new DateTime(2020, 1, 1)
                    )));

        notes = fn.Generate(200);
    }


    public IEnumerable<Person> Persons => persons;

    public IEnumerable<Note> Notes => notes;
}
