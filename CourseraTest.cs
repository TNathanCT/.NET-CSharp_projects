public class Person
{
    public string username { get; set; }
    public List<string> booksborrowed = new List<string>();

    public Person(string name, List<string> borrowedList)
    {
        usersname = name;
        booksborrowed = borrowedList;
    }
}


public class Program
{
    public List<string> booktitleList  = new List<string>();
    bool available;
    public List<Person> users = new List<Person>();


    public static void Main(string[] args){
        Program program = new Program();
        bool runapp = true;


        while(runapp == true){
            if(runapp = false){
                break;
            }

       
        Console.WriteLine("Welcome to the library, What would you like to do? Add, Remove, Display, Search, Borrow, or Exit?");
        string result = Console.ReadLine()?.Trim().ToLower();

        switch(result){
            case "add":
                if(booktitleList.Count < 5){      
                    Start:
                    Console.WriteLine("Add the name of the Book Title: ");
                    result = Console.ReadLine();
                    if(string.IsNullOrEmpty(result)){
                        Console.WriteLine("Error, please add name of book");
                        goto Start;
                    }
                    program.Add(result);        
                }
                else{
                    Console.WriteLine("The Library is full");
                }
                break;
            
            case "remove":
                Start:
                Console.WriteLine("Remove the name of the Book Title:");
                result = Console.ReadLine();
                if(string.IsNullOrEmpty(result)){
                    Console.WriteLine("Error, please add name of book");
                    goto Start;
                }
                program.Remove(result);
                break;

            case "display":
                program.Display();
                break;


            case "exit":
                runapp = false;
                break;

            case "search":
                Start:
                Console.WriteLine("Give the name of the Book Title to search:");
                result = Console.ReadLine();
                if(string.IsNullOrEmpty(result)){
                    Console.WriteLine("Error, please add name of book");
                    goto Start;
                }
                available = SearchBook(result);
                break;

            case "borrow":
                Start:
                Console.WriteLine("Give the name of the Book Title to search:");
                result = Console.ReadLine();
                if(string.IsNullOrEmpty(result)){
                    Console.WriteLine("Error, please add name of book");
                    goto Start;
                }
                available = SearchBook(result);

                if(available){

                }
                
            default:
                Console.WriteLine("Improper input, please try again");
                break;

        }
        }
    }

    public void Add(string name){
        booktitleList.Add(name);
    }

    public void Remove(string name){
        if(booktitleList.Count == null){
            Console.WriteLine("Library is Empty");
            return;
        }


        for(int i = 0;i<booktitleList.Count-1; i++){
            if(booktitleList[i] == name){
                booktitleList.RemoveAt(i);
                Console.WriteLine("Book removed");
            }
            if(i == 5){
                Console.WriteLine("Book name not found in library");
            }
        }
    }
    public void Display(){
        for(int i = 0; i<booktitleList.Count; i++){
            Console.WriteLine($"{i}) {booktitleList[i]}");
        }
    }

    public bool SearchBook(string name){
        for(int i = 0;i<booktitleList.Count-1; i++){
            if(booktitleList[i] == name){
                Console.WriteLine("The Book does exist in the library");
                return true;
            }
            if(i == 5){
                Console.WriteLine("Book name not found in library");
                return false;
            }
        }
    }

    public void BorrowBook(string bookname){
        string name;
        List<string> bookborrowed = new List<string>();
        while(string.IsNullOrEmpty(name)){
            Console.WriteLine("Please give us your username");
            name = Console.ReadLine()?.Trim();
            if(!string.IsNullOrEmpty(name)){
                break;
            }
        }

        if(users.Count == null){
            bookborrowed.Add(bookname);
            Person newPerson = new Person(name, bookborrowed);
            users.Add(newPerson);
        }

        else{
            for(int i = 0; i < users.Count-1;i++){
                if(users[i].name == name){
                   bookborrowed = users[i].borrowedList;
                   bookborrowed.Add(bookname);
                   users[i].borrowedList = bookborrowed;
                }
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
