/*using Microsoft.OpenApi.Models;
//using BestCricketers.Core.BL;  
//using BestCricketers.Models;  
using System;  
//using System.LINQ;
using System.Collections.Generic;  
using System.Net;  
using System.Net.Http;  
//using System.Web.Http;  

var builder = WebApplication.CreateBuilder(args);

// Enable Swagger for testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger UI (always on for learning)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// ====== In-memory “database” ======
List<User> users = new()
{
    new User { Id = 1, Name = "Alice", Age = 30 },
    new User { Id = 2, Name = "Bob", Age = 25 },
    new User { Id = 3, Name = "Charlie", Age = 28 }
};

// ====== API ENDPOINTS ======

// GET all users
app.MapGet("/users", () => users)
   .WithName("GetUsers")
   .WithOpenApi();

// GET a specific user
app.MapGet("/users/{id}", (int id) =>
{
    var user = users.FirstOrDefault(u => u.Id == id);
    return user is not null ? Results.Ok(user) : Results.NotFound();
})
.WithName("GetUserById")
.WithOpenApi();

// POST a new user
app.MapPost("/users", (User newUser) =>
{
    newUser.Id = users.Max(u => u.Id) + 1;
    users.Add(newUser);
    return Results.Created($"/users/{newUser.Id}", newUser);
})
.WithName("CreateUser")
.WithOpenApi();

app.Run();

// ====== Data model ======
record User
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
}


public struct UserClassStruct{
    public int Id { get; set;}
    public string Name { get; set; }
    public int Age { get; set; }


    public UserClassStruct(int id, string name, int age){
        Id = id;
        Name = name;
        Age = aging;
    }
}

*/


//TODO LIST
//-------------------------------------------------------------------------------------------
using System;  
using System.Collections.Generic;  
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;
using Newtonsoft.Json;

public class TodoClass{
    public bool completed;
    public string description;

    public TodoClass(bool receivecompleted, string receivedescription){
        this.completed = receivecompleted;
        this.description = receivedescription;
    }
    public bool GetCompleted(){
        return completed;
    }
    public string GetDescription(){
        return description;
    }
}

public class Program{
    
    static Dictionary<int,TodoClass> todoListDictionary = new Dictionary<int,TodoClass>();

    /*
        5) Delete things to do
        6) save in JSON
    */

    public static void Main(string[] args){
       //BackToStart:
        Console.WriteLine("What would you like to do? Add, Display, Change, Mark, Delete");
        string chosen = Console.ReadLine()?.Trim().ToLower();
        

        switch(chosen){
            case "add":
                Add();
                SaveToFile();
                //goto BackToStart;
                break;
            case "display":
                DisplayTasks();
                break;

            case "change":
                Change();
                SaveToFile();
                break;
            
            case "mark":
                MarkCompleted();
                SaveToFile();
                break;
            
            default:
                break;
        }

    }

    static void Add(){
        Start:
        Console.WriteLine("Add a description of the task in question please.");
        string descriptiongiven = Console.ReadLine();
        if(String.IsNullOrEmpty(descriptiongiven)){
            Console.WriteLine("You left it blank!");
            goto Start;
        }
        bool displaycompleted = false; //it always starts as not completed.
        
        TodoClass todoList = new TodoClass(displaycompleted, descriptiongiven);
        int numberOfKeys = todoListDictionary.Count;
        todoListDictionary.Add(numberOfKeys + 1 , todoList);
        DisplayTasks();

    }

    static void DisplayTasks(){
        int dictionaryLength = todoListDictionary.Count;

        if(dictionaryLength == 0){
            Console.WriteLine("The list is empty! Add something!");
            return;
        }

        for(int i = 1; i <= dictionaryLength; i++){
            Console.WriteLine($"Task {i}: {todoListDictionary[i].description} - { (!todoListDictionary[i].completed ? "[ ]" : "[X]")}");
       
        }
    }

    static void Change(){
        int result;
        bool successfullyParsed;
        Start:
        Console.WriteLine("Type the number of the task you wish to change: ");
        successfullyParsed = int.TryParse(Console.ReadLine(), out result);

        if(!successfullyParsed){
            Console.WriteLine("Please try again: ");
            goto Start;
        }
        
        Console.WriteLine("Write the new description:");
        todoListDictionary[result].description = Console.ReadLine();
        DisplayTasks();
    }

    static void MarkCompleted(){
        int result;
        bool successfullyParsed;
        Start:
        Console.WriteLine("Type the number of the task you wish to change: ");
        successfullyParsed = int.TryParse(Console.ReadLine(), out result);
        if(!successfullyParsed){
            Console.WriteLine("Please try again: ");
            goto Start;
        }

        todoListDictionary[result].completed = true;
        DisplayTasks();
    }

    static void SaveToFile(){
        string json = JsonConvert.SerializeObject(todoListDictionary);
        System.IO.File.WriteAllText("/Users/thomas/Dev/simplifiedbackend/data.txt",json);
    }



}
