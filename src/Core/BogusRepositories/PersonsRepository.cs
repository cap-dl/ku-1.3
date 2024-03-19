using Core.Models;
using Diware.SL.Pagination;

namespace Core.BogusRepositories;

internal class PersonsRepository
    : IPersonsRepository
{
    private readonly Storage storage;

    public PersonsRepository(Storage storage)
    {
        this.storage = storage;
    }


    Task<ListPage<Person>> IPersonsRepository.GetPagedPersonsAsync(PageInfo page, CancellationToken ct)
    {
        var q = storage.Persons.AsQueryable();
        var total = q.LongCount();

        q = q.Skip(page.Skipped).Take(page.ItemsPerPage);

        if (page.Ordering.Any())
        {
            //TODO: implement
        }
        else
        {
            q = q
                .OrderBy(x => x.LastName)
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.FirstName);
        }

        var items = q.ToList();

        var rv = new ListPage<Person>(page, total, items);
        return Task.FromResult(rv);
    }


    Task<Person?> IPersonsRepository.GetPersonAsync(PersonId personId, CancellationToken ct)
    {
        var rv = storage.Persons.FirstOrDefault(x => x.PersonId == personId);
        return Task.FromResult(rv);
    }
}
