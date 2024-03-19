using Core.Models;
using Diware.SL.Pagination;

namespace Core;

public interface INotesRepository
{
    Task<Note?> GetPersonNoteAsync(
        PersonId personId,
        NoteId noteId,
        CancellationToken ct);

    Task<ListPage<Note>> GetPersonNotesAsync(
        PersonId personId,
        PageInfo page,
        CancellationToken ct = default);
}
