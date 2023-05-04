using Storage.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () =>  ( (new NoteDb()).GetDefault())).WithOpenApi(); 
app.MapGet("/notebooks", () =>  (new NoteDb()).GetNoteBooks());
app.MapGet("/notebooks/{noteBookid}", (string noteBookId) =>  (new NoteDb()).GetNoteBook(noteBookId) );

app.MapGet("/notebooks/{noteBookId}/notes", (string noteBookId) => (new NoteDb()).GetNotes(noteBookId));
app.MapGet("/notes", () =>  (new NoteDb()).GetNotes() );
app.MapGet("/notes/{id}", (string id) =>  (new NoteDb()).GetNote(id) );

app.MapGet("/tags", () =>  (new NoteDb()).GetTags() );
app.MapGet("/tags/{id}", (string id) =>  (new NoteDb()).GetTag(id) );

app.MapGet("/notebooks/{noteBookid}/notes/{noteId}", ( string noteBookId,string noteId) =>  (new NoteDb()).GetPage(noteBookId,noteId) );

app.MapGet("/search/{subString}", (string subString) => (new NoteDb()).SearchNotes(subString));
// Delete Pages. 
app.MapDelete("/notebooks/{noteBookId}/notes/{noteId}", ( string noteBookId,string noteId) =>  (new NoteDb()).DeletePage(noteBookId,noteId) );

// Create page
app.MapPost("/notebooks", (Page page) =>  (new NoteDb()).CreatePage(page) );
app.MapPost("/notebooks/{noteBookId}/notes/{noteId}", (string noteBookId,string noteId, Page page) =>  (new NoteDb()).CreatePage(noteBookId,noteId, page) );

app.Run();