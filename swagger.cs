using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.Routing;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var habits = new List<Habit>()
{
    new Habit(0, "Brush your teeth", 2),
    new Habit(1, "Floss", 1),
    new Habit(2, "Mouthwash", 1)
};



//I never used IResult before. I had to look it up on learn.Microsoft
app.MapGet("/Habit/{id}", IResult (int id) =>
{
    var habit = habits.FirstOrDefault(h => h.id == id);
    if(habit is null)
    {
        Console.WriteLine("400 Error");
        return Results.NotFound(); //404 error
    }
    Console.WriteLine("200 Success");
    return Results.Ok(habit);
});

//I didn't know this. I had to look it up on learn.Microsoft
//Cannot implicitly convert type 'Microsoft.AspNetCore.Http.IResult' to 'int' -> I was stuck on that issue, and I had to use some chatgpt to help
//what is commented out was the original idea -> use the Console.Readline() to get user input and then use post
app.MapPost("/Habit", (HabitCreateRequest request) =>
{
    int newid = habits.Count == 0 ? 1 : habits.Max(h => h.id)+1;
    var newhabit = new Habit(newid, request.taskname, request.times);
    Console.WriteLine("201 Success : " + newhabit.task + " created");
    return Results.Created($"/Habit/{newhabit.id}", newhabit);
   
//tryagain:
//    Console.WriteLine("\n", "Please add the task: ");
//    string taskname = Console.ReadLine();
//    Console.WriteLine("\n", "Please say how many time: ");

//    int times;
//    times = Int32.TryParse(Console.ReadLine(), out times) ? times : 0;
    
//    int newid = habits.Count + 1;
//    if (string.IsNullOrEmpty(taskname))
//    {
//        goto tryagain;
//    }
//  var newhabit = new Habit(newid, taskname, times);
//    Uri url = new Uri($"/Habit/{newhabit.id}");
//    return Results.Created(url, newhabit);
});


app.MapPut("/Habit/{id}", IResult (int id, HabitCreateRequest request) =>
{
    var index = habits.FindIndex(h => h.id == id);
    if(index == -1)
    {
        Console.WriteLine("404 Error - does not exist");
        return Results.NotFound(); //404 error
    }
    habits[index] = habits[index] with {task = request.taskname, timesperday = request.times};
    return Results.Ok( habits[id]);
});

app.MapDelete("/Habit/{id}", IResult(int id) =>
{
    
    var removed = habits.RemoveAll(h => h.id == id);
    return removed == 0 ? Results.NotFound() : Results.NoContent();
});


app.Run();

record Habit(int id, string task, int timesperday);
public record HabitCreateRequest(int id, string taskname,int times);

