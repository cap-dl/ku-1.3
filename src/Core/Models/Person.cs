namespace Core.Models;

public record class Person(
    PersonId PersonId,
    string FirstName,
    string LastName)
{
}
