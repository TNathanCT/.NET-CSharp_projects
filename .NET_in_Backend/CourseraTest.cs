
using System.Text.Json.Serialization;
using System.Xml;
using Newtonsoft.Json;

public class Product
{
    public string name {get ; set; }
    public float price { get; set; }
    public List<string> tags {get; set; }
}

public class Program
{
    public static void Main()
    {
        Product newproduct = new Product
        {
            name = "SmartPhone",
            price = 966.33,
            tags = new List<string> {"Electronics", "Mobile"},
        };

        //We use try/catch to throw out an exceptio if needed
        try
        {
            string newJson = JsonConvert.SerializeObject(newproduct, Formatting.Indented);
            Console.WriteLine($"Serialised json:\n{newJson}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine("Serialization failed:");
            Console.WriteLine(ex.Message);
        }
    }
}



/*
using Newtonsoft.Json;

public class Person{
    public required string name {get; set; }
    public required int age {get; set; }
    public required List<string> nationality {get; set;}
}

public class Program{
    static void Main(){
        Person person = new Person{
            name = "Thomas",
            age = 30,
            nationality = new List<string>{"British", "French"},
        };
        string newJson = JsonConvert.SerializeObject(person, Formatting.Indented);
        Console.WriteLine($"Serialised: \n {newJson}");
    }
}*/
