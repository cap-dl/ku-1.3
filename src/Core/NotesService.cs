using Core.Models;
using Diware.SL.Pagination;

namespace Core;

/// <summary>
/// Manages person's notes.
/// </summary>
public class NotesService
{
    private readonly INotesRepository repo;

    public NotesService(
        INotesRepository notesRepository)
    {
        repo = notesRepository;
    }


    public Task<Note?> GetNoteOfPerson(
        PersonId personId,
        NoteId noteId,
        CancellationToken ct = default)
    {
        return repo.GetPersonNoteAsync(personId, noteId, ct);
    }


    public Task<ListPage<Note>> GetNotesOfPerson(
        PersonId personId,
        PageInfo page,
        CancellationToken ct = default)
    {
        return repo.GetPersonNotesAsync(personId, page, ct);
    }
}
