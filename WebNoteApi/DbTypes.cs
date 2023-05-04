namespace Storage.DB;

public interface IId
{
    string Id { get; }
    string ValueToString();
    bool IsDefault();
}

public record class Note : IId
{
    public string Id { get; init; }
    public string Title { get; init; }
    public string Body { get; init; }

    public Note()
    {
        Id = "defaultNote";
        Title = "Default Note";
        Body = "This is a body of a default note";
    }
    public Note(string id, string title, string body)
    {
        Id = id;
        Title = title;
        Body = body;
    }

    public bool IsDefault()
    {
        return Id == "defaultNote";
    }

    public string ValueToString()
    {
        return $"{Title} {Body}";
    }
}

public record class NoteBook : IId
{
    public string Id { get; init; }
    public string Title { get; init; }

    public NoteBook()
    {
        Id = "defaultNoteBook";
        Title = "Default NoteBook";
    }

    public NoteBook(string id, string title)
    {
        Id = id;
        Title = title;
    }
    public bool IsDefault()
    {
        return Id == "defaultNoteBook";
    }

    public string ValueToString()
    {
        return $"{Title}";
    }
}

public record class Tag : IId
{
    public string Id { get; init; }

    public Tag()
    {
        Id = "defaultTag";
    }
    public Tag(string id)
    {
        Id = id;
    }

    public bool IsDefault()
    {
        return Id == "defaultTag";
    }

    public string ValueToString()
    {
        return $"{Id}";
    }
}
