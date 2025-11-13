using Microsoft.OpenApi.Models;
using System;  
using System.Collections.Generic;  
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;






//TODO LIST
//-------------------------------------------------------------------------------------------


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
        return $"{(completed ? "[X]" : "[ ]")} {description} - it is blank";
    }
}

public class TodoDate : TodoItem{
    public DateTime dueDate;

    public TodoDate(int receiveid, bool receivecompleted, string receivedescription, DateTime receivedduedate) : base(receiveid, receivecompleted, receivedescription){
        this.dueDate = receivedduedate;
    }
    //The base proves that this constructor is derived of the todoitem

    public override string Render(){
        string status = $"{(completed ? "[X]" : "[ ]")} {description}";
        string overdue = (!completed && dueDate < DateTime.Today) ? " OVERDUE" : " You still have time";

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
        bool successfullyConverted = false;

        todoListDictionary.Add(numberOfKeys + 1 , todoitem);
        Console.WriteLine("Do you want to add a Due date?");
        string answer = Console.ReadLine()?.Trim()?.ToLower();
        if(answer == "yes"){
            DateTime due;
            while(successfullyConverted == false){
                try{
                    Console.WriteLine("Choose your due date. Set the format to yyyy-MM-dd please.");
                    answer = Console.ReadLine();
                    due = ConvertToDate(answer);

                    todoListDictionary[numberOfKeys+1] = new TodoDate(todoListDictionary[numberOfKeys+1].id, todoListDictionary[numberOfKeys+1].completed, todoListDictionary[numberOfKeys+1].description, due);
                    successfullyConverted = true;

                }catch(FormatException){
                    Console.WriteLine("Wrong format, please try again.");
                }
            }
        }
        Console.WriteLine("Displaying Tasks");
        DisplayTasks();
    }

    
    public void DisplayTasks(){
        foreach(var item in todoListDictionary){
            //All ToDoItems will have blank 
            //all ToDoDates will have notes
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
    Essentially this contains the function, but we can edit it in different classes.
    Use virtual methods - do not need to add details and an excellent example of polymophism
}
*/
    

public class Program{
    

    public TodoManager manager = new TodoManager();
    


    public static async Task Main(string[] args){

        

        using HttpClient client = new(); 
        //clear the current header of all default options
        client.DefaultRequestHeaders.Accept.Clear();
        //Add the "I want the response in GitHub’s v3 API JSON format" header
        //MediaTypeWithQualityHeaderValue is a helper class representing a single media type like application/json
        //client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json")); 
        //So github requires a user agent is all API requests, the next line identifies your app or script
        //client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("User-Agent", "Swagger Todo List");
        //Await here will pause the execution until the GetDataFromAPI is complete
        await GetDataFromBackEnd(client);


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


     //async makes it clear it is asynchronous - it will run in background while frontend works.
    //The Task is the return type
    static async Task GetDataFromBackEnd(HttpClient client){
       
       // var data = await GetDataFromAPI();
        string stringRequest = "http://localhost:5184/todos";
        //Send a GET request to the specified URI and return the response body as a string in an asynchronous operation.
        var jsondata = await client.GetStringAsync(stringRequest);

        Console.Write(jsondata);
    }
}
