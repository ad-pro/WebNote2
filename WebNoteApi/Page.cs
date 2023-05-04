namespace Storage.DB;

public record class Page : IId
{
    public NoteBook NoteBook { get; init; }
    public Note Note { get; init; }
    public List<Tag> Tags { get; init; } = new List<Tag>();
    public List<Link> Links { get; init; } = new List<Link>();

    public string Id => Page.GetPageId(NoteBook.Id, Note.Id);

    public static string GetPageId(string noteBookId, string noteId)
    {
        return $"{noteBookId}-{noteId}";
    }

    public Page()
    {
        NoteBook = new NoteBook();
        Note = new Note();
    }

    public Page(NoteBook noteBook, Note note)
    {
        NoteBook = noteBook;
        Note = note;
    }
    public Page(NoteBook noteBook, Note note, List<Tag> tags, List<Link> links)
    {
        NoteBook = noteBook;
        Note = note;
        Tags = new List<Tag>(tags);
        Links = new List<Link>(links);
    }

    public Page(string title)
    {
        NoteBook = new NoteBook(Utils.NewId(), title);
        Note = new Note();
    }

    public Page(string title, string body)
    {
        NoteBook = new NoteBook();
        Note = new Note(Utils.NewId(), title, body);
    }

    public string ValueToString()
    {
        throw new NotImplementedException();
    }

    public bool IsDefault()
    {
        return NoteBook.IsDefault() && Note.IsDefault();
    }
}

public record class Link
{
    public string NoteBookId { get; init; }
    public string NoteId { get; init; }

    public string Id { get => Page.GetPageId(NoteBookId, NoteId); }

    public Link() : this(new NoteBook(), new Note()) { }

    public Link(string noteBookId, string noteId)
    {
        NoteBookId = noteBookId;
        NoteId = noteId;
    }

    public Link(NoteBook noteBook, Note note) : this(noteBook.Id, note.Id) { }

    public Link(Page page) : this(page.NoteBook.Id, page.Note.Id) { }
}
