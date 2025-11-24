using System;
using System.Collections.Generic;
using System.Linq;

public class Person
{
    public string username { get; set; }
    public List<string> booksborrowed = new List<string>();

    public Person(string name, List<string> borrowedList)
    {
        username = name;
        booksborrowed = borrowedList;
    }
}

public class Program
{
    public static List<string> booktitleList  = new List<string>();
    static bool available;
    public static List<Person> users = new List<Person>();

    public static void Main(string[] args)
    {
        Program program = new Program();
        bool runapp = true;

        while (runapp)
        {
            Console.WriteLine("Welcome to the library, What would you like to do? Add, Remove, Display, Search, Borrow, Return or Exit?");
            string result = Console.ReadLine()?.Trim().ToLower();

            switch (result)
            {
                case "add":
                    if (booktitleList.Count < 5){
                        result = Prompt("Add the name of the Book Title: ");
                        program.Add(result);
                    }
                    else{
                        Console.WriteLine("The Library is full");
                    }
                    break;

                case "remove":
                    result = Prompt("Remove the name of the Book Title:");
                    program.Remove(result);
                    break;

                case "display":
                    program.Display();
                    break;

                case "exit":
                    runapp = false;
                    break;

                case "search":
                    result = Prompt("Give the name of the Book Title to search:");
                    available = program.SearchBook(result);
                    break;

                case "borrow":
                    result = Prompt("Give the name of the Book Title to borrow:");
                    available = program.SearchBook(result);
                    if (available){
                        program.BorrowBook(result);
                    }
                    else{
                        Console.WriteLine("Error, book missing from collection, please add name of book");
                    }
                    break;

                case "return":
                        string name = Prompt("Please give us your username:");
                        program.CheckInBook(name);
                        break;

                default:
                    Console.WriteLine("Improper input, please try again");
                    break;
            }
        }
    }

    static string Prompt(string message){
        string s = "";
        while (string.IsNullOrWhiteSpace(s)){
            Console.WriteLine(message);
            s = Console.ReadLine()?.Trim();
        }
        return s;
    }

    public void CheckInBook(string name){
        foreach (Person user in users){
            if (user.username == name){
                string booktitle = "";
                while (string.IsNullOrEmpty(booktitle)){
                    Console.WriteLine("Please tell us the book you wish to return");
                    booktitle = Console.ReadLine()?.Trim();

                    if (user.booksborrowed.Contains(booktitle, StringComparer.OrdinalIgnoreCase)){
                        user.booksborrowed.Remove(booktitle);
                        booktitleList.Add(booktitle);
                        Console.WriteLine("Book checked in");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You don't have that book borrowed. Try again.");
                        booktitle = "";
                    }
                }
            }
        }
    }

    public void Add(string name)
    {
        booktitleList.Add(name);
    }

    public void Remove(string name)
    {
        if (booktitleList.Count == 0)
        {
            Console.WriteLine("Library is Empty");
            return;
        }

        for (int i = 0; i < booktitleList.Count; i++)
        {
            if (string.Equals(booktitleList[i], name, StringComparison.OrdinalIgnoreCase))
            {
                booktitleList.RemoveAt(i);
                Console.WriteLine("Book removed");
                return;
            }
        }

        Console.WriteLine("Book name not found in library");
    }

    public void Display()
    {
        if (booktitleList.Count == 0)
        {
            Console.WriteLine("Library is Empty");
            return;
        }

        for (int i = 0; i < booktitleList.Count; i++)
        {
            Console.WriteLine($"{i}) {booktitleList[i]}");
        }
    }

    public bool SearchBook(string name)
    {
        foreach (string title in booktitleList)
        {
            if (string.Equals(title, name, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("The Book is available");
                return true;
            }
        }

        Console.WriteLine("Book is not available.");
        return false;
    }

    public void BorrowBook(string bookname)
    {
        string name = "";
        List<string> bookborrowed = new List<string>();

        while (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Please give us your username");
            name = Console.ReadLine()?.Trim();
        }

        // find existing user if any
        Person user = users.FirstOrDefault(u => u.username.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (user == null)
        {
            user = new Person(name, new List<string>());
            users.Add(user);
        }

        if (user.booksborrowed.Count >= 3)
        {
            Console.WriteLine("This user cannot borrow more books.");
            return;
        }

        // remove book from library and add to user
        int index = booktitleList.FindIndex(b => b.Equals(bookname, StringComparison.OrdinalIgnoreCase));
        if (index >= 0)
        {
            booktitleList.RemoveAt(index);
            user.booksborrowed.Add(bookname);
            Console.WriteLine("Book checked out");
        }
        else
        {
            Console.WriteLine("Book is not available!");
        }
    }
}





    /*
    // Asynchronous method to fetch product data
    public async Task<List<Product>> FetchProductsAsync()
    {
        await Task.Delay(2000); // Simulating a 2-second delay for data fetching
        return new List<Product>
        {
            new Product("Eco Bag"),
            new Product("Reusable Straw")
        };
    }



    // Asynchronous method to display product data
    public async Task DisplayProductsAsync()
    {
        List<Product> products = await FetchProductsAsync();
        foreach (Product product in products)
        {
            Console.WriteLine(product.Name);
        }
    }


    

    // Main entry point
    public static async Task Main(string[] args)
    {
        // Calling the asynchronous method
        Program program = new Program();
        await program.DisplayProductsAsync();
    }
    */



}
