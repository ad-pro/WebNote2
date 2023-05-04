namespace Storage.DB;

public class NoteDb
{
    public const string TypeNoteBook = "NoteBook";
    public const string TypeNote = "Note";
    public const string TypeTag = "Tag";

    private static Storage<NoteBook> _noteBooks = new Storage<NoteBook>();
    private static Storage<Note> _notes = new Storage<Note>();
    private static Storage<Tag> _tags = new Storage<Tag>();
    private static Links _links = new Links();

    private static bool _initialized = false;

    public NoteDb()
    {
        if (!_initialized)
        {
            InitPages();
            _initialized = true;
        }
    }

    public string[] GetDefault()
    {
        string[] arr = new string[] { "notebooks", "notes", "tags", "search", "swagger/index.html" };
        return arr;
    }

    private void InitPages()
    {
        var nb0 = new NoteBook();
        var nb1 = new NoteBook("nb1", "Note Book 1");
        var nb2 = new NoteBook("nb2", "Note Book 2");
        var nb3 = new NoteBook("nb3", "Note Book 3");

        var note0 = new Note();
        var note1 = new Note("note1", "Note 1", "Note 1 Body");
        var note2 = new Note("note2", "Note 2", "Note 2 Body");
        var note3 = new Note("note3", "Note 3", "Note 3 Body");
        var note4 = new Note("note4", "Note 4", "Note 4 Body");

        var tag0 = new Tag();
        var tag1 = new Tag("tag1");
        var tag2 = new Tag("tag2");
        var tag3 = new Tag("tag3");
        var tag4 = new Tag("tag4");

        var linksPage0 = new List<Link>();
        var linksPage1 = new List<Link>() { new Link(nb1, note1) };
        var linksPage2 = new List<Link>() { new Link(nb2, note2) };
        var linksPage3 = new List<Link>() { new Link(nb3, note3) };

        var page0 = new Page(nb0, note0);
        var page1 = new Page(nb1, note1, new List<Tag>() { tag1, tag2 }, linksPage0);
        var page2 = new Page(nb1, note2, new List<Tag>() { tag2, tag3 }, linksPage1);
        var page3 = new Page(nb2, note2, new List<Tag>() { tag3, tag4 }, linksPage2);
        var page5 = new Page(nb3, note3, new List<Tag>() { tag1, tag2, tag3, tag4 }, linksPage1);
        var page6 = new Page(nb3, note4, new List<Tag>() { tag1, tag2, tag3, tag4 }, linksPage1);
        var page4 = new Page(nb3, note1, new List<Tag>() { tag1, tag2, tag3, tag4 }, linksPage3);

        StorePage(page0);
        StorePage(page1);
        StorePage(page2);
        StorePage(page3);
        StorePage(page4);
        StorePage(page5);
        StorePage(page6);
    }

    private bool IsValidLink(Link link)
    {
        string noteBookId = link.NoteBookId;
        string noteId = link.NoteId;

        return _noteBooks.ContainsKey(noteBookId) && _notes.ContainsKey(noteId);
    }

    public void StorePage(Page page)
    {
        _noteBooks.AddElement(page.NoteBook);
        _notes.AddElement(page.Note);

        var pageTags = page.Tags;
        var pageLinks = page.Links;

        var selfLink = new Link(page);

        foreach (var tag in pageTags)
        {
            _links.AddTag(selfLink, tag);
            _tags.AddElement(tag);
        }

        _links.AddLink(selfLink, selfLink);

        foreach (var link in pageLinks)
        {
            if (IsValidLink(link))
            {
                _links.AddLink(selfLink, link);
            }
        }
    }

    public Page RestorePage(NoteBook notebook, Note note)
    {
        return RestorePage(notebook.Id, note.Id);
    }

    public Page RestorePage(string noteBookId, string noteId)
    {
        var noteBook = _noteBooks.GetElement(noteBookId);
        var note = _notes.GetElement(noteId);

        var selfLink = new Link(noteBook, note);

        if (_links.IsKnownLink(selfLink))
        {
            var pageTagIds = _links.GetPageTagIds(selfLink);

            var pageTags = new List<Tag>();
            foreach (var tagId in pageTagIds)
            {
                pageTags.Add(_tags.GetElement(tagId));
            }

            var pageLinks = _links.GetPageLinks(selfLink);
            var page = new Page(noteBook, note, pageTags, pageLinks);

            return page;
        }

        return new Page(noteBook.Title);
    }

    public NoteBook[] GetNoteBooks() { return _noteBooks.GetAllElements(); }

    public NoteBook GetNoteBook(string id)
    {
        return _noteBooks.GetElement(id);
    }

    public Note[] GetNotes() { return _notes.GetAllElements(); }

    public Note[] GetNotes(string noteBookId)
    {
        var noteBookNoteIds = _links.GetNoteBookNoteIds(noteBookId);
        var noteList = new List<Note>();
        foreach (var noteId in noteBookNoteIds)
        {
            if (_notes.ContainsKey(noteId))
            {
                noteList.Add(_notes.GetElement(noteId));
            }
        }

        return noteList.ToArray();
    }

    public Note[] SearchNotes(string subString) {
     var noteList = new List<Note>();

        foreach (var note in _notes.GetAllElements()) {
            string str = note.ValueToString();
            if (str.Contains(subString,StringComparison.InvariantCultureIgnoreCase) ) {
                noteList.Add(note);
            }
        }

     return noteList.ToArray();
    }

    public Note GetNote(string id)
    {
        return _notes.GetElement(id);
    }

    public Tag[] GetTags() { return _tags.GetAllElements(); }

    public Tag GetTag(string id)
    {
        return _tags.GetElement(id);
    }

    public Page GetPage(string noteBookId, string noteId)
    {
        return RestorePage(noteBookId, noteId);
    }

    public void DeletePage(string noteBookId, string noteId)
    {
        var pageLink = new Link(noteBookId, noteId);
        _links.DeleteLink(pageLink);
    }

    public void CreatePage(string noteBookId, string noteId, Page page)
    {
        NoteBook newNoteBook = page.NoteBook with { Id = noteBookId };

        if (_noteBooks.ContainsKey(noteBookId))
        {
            newNoteBook = _noteBooks.GetElement(noteBookId);
        }
        var newNote = page.Note with { Id = noteId };
        var newPage = page with { NoteBook = newNoteBook, Note = newNote };
        StorePage(newPage);
    }

    public void CreatePage(Page page) { StorePage(page); }
}