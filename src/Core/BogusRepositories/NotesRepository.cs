using Core.Models;
using Diware.SL.Pagination;

namespace Core.BogusRepositories;

internal class NotesRepository
    : INotesRepository
{
    private readonly Storage storage;

    public NotesRepository(Storage storage)
    {
        this.storage = storage;
    }


    Task<Note?> INotesRepository.GetPersonNoteAsync(
        PersonId personId,
        NoteId noteId,
        CancellationToken ct)
    {
        var rv = storage.Notes.FirstOrDefault(x
            => x.NoteId == noteId
            && x.PersonId == personId);

        return Task.FromResult(rv);
    }


    Task<ListPage<Note>> INotesRepository.GetPersonNotesAsync(
        PersonId personId,
        PageInfo page,
        CancellationToken ct)
    {
        var q = storage.Notes.AsQueryable();
        var total = q.LongCount();

        q = q.Skip(page.Skipped).Take(page.ItemsPerPage);

        if (page.Ordering.Any())
        {
            //TODO: implement
        }
        else
        {
            q = q
                .OrderBy(x => x.Text);
        }

        var items = q.AsEnumerable();

        var rv = new ListPage<Note>(page, total, items);
        return Task.FromResult(rv);
    }
}
