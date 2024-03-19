using Core.Models;
using Diware.SL.Pagination;

namespace Core;

public interface IPersonsRepository
{
    Task<ListPage<Person>> GetPagedPersonsAsync(
        PageInfo page,
        CancellationToken ct);

    Task<Person?> GetPersonAsync(
        PersonId personId,
        CancellationToken ct);
}
