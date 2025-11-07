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

public class TodoItem{
    public bool completed;
    public string description;
    public int id;

    public TodoItem(int receiveid, bool receivecompleted, string receivedescription){
        this.id = receiveid;
        this.completed = receivecompleted;
        this.description = receivedescription;        
    }

    public virtual void SetCompleted(){
        this.completed = true;
    }
    public virtual string Render(){
        return $"{(completed ? "[X]" : "[ ]")} {description}";
    }
}

public class TodoDate : TodoItem{
    public DateTime dueDate;

    public TodoDate(int receiveid, bool receivecompleted, string receivedescription, DateTime receivedduedate) : base(receiveid, receivecompleted, receivedescription){
        this.dueDate = receivedduedate;
    }

    public override string Render(){
        string status = $"{(completed ? "[X]" : "[ ]")} {description}";
        string overdue = (!completed && dueDate < DateTime.Today) ? " OVERDUE" : "";

        return $@"{status} {description} - due {dueDate:yyyy-MM-dd}{overdue}";
    }
}





public class TodoManager{
    public Dictionary<int,TodoItem> todoListDictionary = new Dictionary<int,TodoItem>();
    
    public void Add(){
        Start:
        Console.WriteLine("Add a description of the task in question please.");
        string descriptiongiven = Console.ReadLine();
        if(String.IsNullOrEmpty(descriptiongiven)){
            Console.WriteLine("You left it blank!");
            goto Start;
        }
        bool displaycompleted = false; //it always starts as not completed.
        
        TodoItem todoitem = new TodoItem(1, displaycompleted, descriptiongiven);
        int numberOfKeys = todoListDictionary.Count;
        todoListDictionary.Add(numberOfKeys + 1 , todoitem);


        Console.WriteLine("Do you want to add a Due date?");
        string answer = Console.ReadLine()?.Trim()?.ToLower();
        if(answer == "yes"){
            DateTime due;
            bool successfullyConverted = false;

            while(successfullyConverted == false){
                try{
                    Console.WriteLine("Choose your due date. Set the format to yyyy-MM-dd please.");
                    answer = Console.ReadLine();
                    due = ConvertToDate(answer);

                    todoListDictionary[result] = new TodoItem(todoListDictionary[result].id, todoListDictionary[result].completed, todoListDictionary[result].description, due);
                    successfullyconverted = true;

                }catch(FormatException){
                    Console.WriteLine("Wrong format, please try again.");
                }
            }
        Console.WriteLine("Displaying Tasks");
        DisplayTasks();
    }

    
    public void DisplayTasks(){

        /*
        int dictionaryLength = todoListDictionary.Count;

        if(dictionaryLength == 0){
            Console.WriteLine("The list is empty! Add something!");
            return;
        }
        */
        foreach(var item in todoListDictionary){
            //Console.WriteLine($"Task {i}: {todoListDictionary[i].description} - { (!todoListDictionary[i].completed ? "[ ]" : "[X]")}");
            Console.WriteLine($"{item.Key}: {item.Value.Render()}");
        }
    }


    public void Change(){
        int result;
        bool successfullyParsed;
        DateTime due;
        Start:
        Console.WriteLine("Type the number of the task you wish to change: ");
        successfullyParsed = int.TryParse(Console.ReadLine(), out result);

        if(!successfullyParsed){
            Console.WriteLine("Please try again: ");
            goto Start;
        }
        
        Console.WriteLine("Do you wish to change the due date: Yes?");
        string answer = Console.ReadLine()?.Trim()?.ToLower();

        bool successfullyconverted = false;

        
        if(answer == "yes"){
            while(successfullyconverted == false){
                try{
                    Console.WriteLine("Set the date yyyy-MM-dd please");
                    answer = Console.ReadLine();

                    due = ConvertToDate(answer);
                    todoListDictionary[result] = new TodoDate(todoListDictionary[result].id, todoListDictionary[result].completed, todoListDictionary[result].description, due);
                    successfullyconverted = true;

                }catch(FormatException){
                    Console.WriteLine("Wrong format, please try again.");
                }
            }
        }else{
            Console.WriteLine("Do you want to change the description?");
            answer = Console.ReadLine()?.Trim()?.ToLower();

            if(answer == "yes"){
                answer = Console.ReadLine();
                todoListDictionary[result].description = answer;
            }
            else{

            }
        }
        
        Console.WriteLine("Displaying Tasks");
        DisplayTasks();
    }

    public DateTime ConvertToDate(string date){
        string[] format = {"yyyy-MM-dd"}; 
        return DateTime.ParseExact(date, format, null, System.Globalization.DateTimeStyles.None);
    }


    


    public void MarkCompleted(){
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
}

/*public interface SavingTask{
    void CheckTaskDescription();
}
*/



public class Program{
    

    public TodoManager manager = new TodoManager();

    public static void Main(string[] args){
        Program pro = new Program();
        pro.RunProgram();       
    }

    void RunProgram(){
        BackToStart:
        Console.WriteLine("What would you like to do? Add, Display, Change, Mark, Delete");
        string chosen = Console.ReadLine()?.Trim().ToLower();
        

        switch(chosen){
            case "add":
                manager.Add();
                SaveToFile();
                break;
            case "display":
                manager.DisplayTasks();
                break;

            case "change":
                manager.Change();
                SaveToFile();
                break;
            
            case "mark":
                manager.MarkCompleted();
                SaveToFile();
                break;
            
            default:
                break;
        }
        goto BackToStart;

    }

    
    void SaveToFile(){
        string json = JsonConvert.SerializeObject(manager.todoListDictionary);
        System.IO.File.WriteAllText("/Users/thomas/Dev/simplifiedbackend/data.txt",json);
    }



}
