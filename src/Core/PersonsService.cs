using Core.Models;
using Diware.SL.Pagination;
using FluentResults;

namespace Core;

public class PersonsService
{
    private readonly IPersonsRepository repo;


    public PersonsService(
        IPersonsRepository personsRepository)
    {
        repo = personsRepository;
    }


    public async Task<IEnumerable<Person>> OldGetAllPersons(
        CancellationToken ct = default)
    {
        var page = await repo.GetPagedPersonsAsync(
            PageInfo.All(), ct);
        return page.Items;
    }


    public Task<ListPage<Person>> OldGetPagedPersonsAsync(
        PageInfo page,
        CancellationToken ct)
    {
        return repo.GetPagedPersonsAsync(page, ct);
    }


    public Task<Person?> OldGetPersonAsync(
        PersonId personId,
        CancellationToken ct = default)
    {
        return repo.GetPersonAsync(personId, ct);
    }


    public async Task<Result<Person>> GetPersonAsync(
        PersonId personId,
        CancellationToken ct = default)
    {
        var person = await repo.GetPersonAsync(personId, ct);
        if (person != null)
        {
            return Result.Ok(person);
        }
        else
        {
            return Result.Fail<Person>("Not found");
        }
    }


    public async Task<Result<ListPage<Person>>> GetPagedPersonsAsync(
        PageInfo pageInfo,
        CancellationToken ct)
    {
        var page = await repo.GetPagedPersonsAsync(pageInfo, ct);
        return Result.Ok(page);
    }
}
