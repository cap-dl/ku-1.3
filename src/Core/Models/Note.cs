namespace Core.Models;

public record class Note(
    NoteId NoteId,
    PersonId PersonId,
    string Text,
    DateTime CreatedOn)
{ }
