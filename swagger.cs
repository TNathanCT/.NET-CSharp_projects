var builder = WebApplication.CreateBuilder(args);

// Add services for minimal API + Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger UI always (for learning)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// =============== In-memory "database" ===============

var todos = new List<TodoItem>
{
    new TodoItem { Id = 1, Description = "Learn .NET minimal APIs", Completed = false, DueDate = DateTime.Today.AddDays(3) },
    new TodoItem { Id = 2, Description = "Hook up console app to this API", Completed = false },
    new TodoItem { Id = 3, Description = "Play with HTTP clients", Completed = false, DueDate = DateTime.Today.AddDays(7) }
};

int NextId() => todos.Count == 0 ? 1 : todos.Max(t => t.Id) + 1;

// GET /todos
app.MapGet("/todos", () => Results.Ok(todos));

// GET /todos/{id}
app.MapGet("/todos/{id:int}", (int id) =>
{
    var todo = todos.FirstOrDefault(t => t.Id == id);
    return todo is null ? Results.NotFound() : Results.Ok(todo);
});

// POST /todos
app.MapPost("/todos", (TodoItem input) =>
{
    input.Id = NextId();
    todos.Add(input);
    return Results.Created($"/todos/{input.Id}", input);
});

// PUT /todos/{id}
app.MapPut("/todos/{id:int}", (int id, TodoItem input) =>
{
    var existing = todos.FirstOrDefault(t => t.Id == id);
    if (existing is null)
        return Results.NotFound();

    existing.Description = input.Description;
    existing.Completed   = input.Completed;
    existing.DueDate     = input.DueDate;

    return Results.Ok(existing);
});

// POST /todos/{id}/complete
app.MapPost("/todos/{id:int}/complete", (int id) =>
{
    var todo = todos.FirstOrDefault(t => t.Id == id);
    if (todo is null)
        return Results.NotFound();

    todo.Completed = true;
    return Results.Ok(todo);
});

// DELETE /todos/{id}
app.MapDelete("/todos/{id:int}", (int id) =>
{
    var todo = todos.FirstOrDefault(t => t.Id == id);
    if (todo is null)
        return Results.NotFound();

    todos.Remove(todo);
    return Results.NoContent();
});

app.Run();

// =============== Model ===============

public class TodoItem
{
    public int Id { get; set; }
    public string Description { get; set; } = "";
    public bool Completed { get; set; }
    public DateTime? DueDate { get; set; }
}
