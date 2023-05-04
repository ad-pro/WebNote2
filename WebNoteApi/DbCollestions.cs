namespace Storage.DB;


public class Storage<T> where T : IId, new()
{
    private Dictionary<string, T> _storage = new Dictionary<string, T>();

    public void AddElement(T obj)
    {
        if (obj == null) { return; }
        IId keyObj = (IId)obj;
        _storage[keyObj.Id] = obj;
    }

    public T GetElement(IId keyObj)
    {
        string key = keyObj.Id;
        return GetElement(key);
    }

    public T GetElement(string key)
    {
        if (_storage.ContainsKey(key))
        {
            return _storage[key];
        }
        return new T();
    }

    public T[] GetAllElements() { return _storage.Values.ToArray<T>(); }

    public bool ContainsKey(IId keyObj) { return ContainsKey(keyObj.Id);}

    public bool ContainsKey(string Key) { return _storage.ContainsKey(Key);}
}

public class Links
{
    private Dictionary<Link, HashSet<string>> _tagLinks = new Dictionary<Link, HashSet<string>>();
    private Dictionary<Link, HashSet<Link>> _linkLinks = new Dictionary<Link, HashSet<Link>>();

    public Links() { }

    public void AddLink(Link parent, Link child)
    {
        if (!_linkLinks.ContainsKey(parent)) {
            _linkLinks[parent] = new HashSet<Link>();
        }
        _linkLinks[parent].Add(child);
    }

    public void DeleteLink(Link parent, Link child)
    {
        if (_linkLinks.ContainsKey(parent))
        {
            _linkLinks[parent].Remove(child);
        }
    }

    public void DeleteLink(Link pageLink) {
        _linkLinks.Remove(pageLink);
    }

    public bool IsKnownLink( Link parent) {
        return _linkLinks.ContainsKey(parent);
    }

    public List<Link> GetPageLinks(Link parent)
    {
        if (_linkLinks.ContainsKey(parent))
        {
            var links = _linkLinks[parent];
            return links.ToList();
        }

        return new List<Link>();
    }

    public List<string> GetNoteBookNoteIds(string noteBookId) {
        var keys = _linkLinks.Keys;
        var notebookNotes = new List <string>();
        foreach (var key in keys) {
            if (key.NoteBookId == noteBookId) {
                notebookNotes.Add(key.NoteId);
            }
        }
        return notebookNotes;
    }

    public void AddTag(Link parent, Tag tag)
    {
        if (!_tagLinks.ContainsKey(parent)) {
            _tagLinks[parent] = new HashSet<string>();
        }

        _tagLinks[parent].Add(tag.Id);
    }

    public void DeleteTag(Link parent, Tag tag)
    {
        if (_tagLinks.ContainsKey(parent))
        {
            _tagLinks[parent].Remove(tag.Id);
        }
    }

    public List<string> GetPageTagIds(Link parent)
    {
        if (_tagLinks.ContainsKey(parent))
        {
            var tags = _tagLinks[parent];
            return tags.ToList();
        }

        return new List<string>();
    }
}
