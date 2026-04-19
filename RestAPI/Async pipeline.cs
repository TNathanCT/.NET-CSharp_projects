using System.Net;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices.Swift;
using System.Reflection.Metadata;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
/*
public sealed class FetchHasher
{
    private readonly HttpClient _http; // - only one at a time

    public FetchHasher(HttpClient http) => _http = http;

    public async Task<IReadOnlyList<FetchResult>> FetchAndHashAsync(
        IEnumerable<Uri> uris,
        int maxConcurrency, // - downloads in flight = 8 max at a time
        CancellationToken cancellationToken) // - cancellationToken is cancelled, stop starting new work quickly
    {
        if (maxConcurrency <= 0) throw new ArgumentOutOfRangeException(nameof(maxConcurrency));

        var uriList = uris.ToList();
        var results = new FetchResult[uriList.Count];

        using var semaphore = new SemaphoreSlim(maxConcurrency);

        var tasks = uriList.Select((uri, index) => ProcessOneAsync(uri, index)).ToArray();
        await Task.WhenAll(tasks);

        return results;

        async Task ProcessOneAsync(Uri uri, int index)
        {
            await semaphore.WaitAsync(cancellationToken);
            try
            {
                // TODO:
                // 1) send request (GetAsync with HttpCompletionOption.ResponseHeadersRead)
                //ResponseHeadersRead forces the streaming - it prevents data from being saved to memory.
                // 2) stream content, compute SHA256
                // 3) record byte count + status code


                using (var response = await _http.GetAsync("http://example.com", HttpCompletionOption.ResponseHeadersRead)){
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Error: {response.IsSuccessStatusCode}");    
                    }
                    else
                    {
                        Console.WriteLine($"Response is : {response.IsSuccessStatusCode}");
                        using var stream = await response.Content.ReadAsStreamAsync();

                        byte[] buffer = new byte[9000];
                        int bytesread;
                        long totalbyte = 0;

                        while((bytesread = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0){
                            totalbyte += bytesread;
                        }

                        GetHash256(stream.ToString());
                    }    
                }
                
                
               
                
                // 4) handle errors -> results[index] = failed FetchResult
            }
            finally
            {
                semaphore.Release();
            }
        }
    }

    public string GetHash256(string text)
    {
        using(SHA256 hash = SHA256.Create()){
            byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder builder = new StringBuilder();
            for(int i = 0 ; i <bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            } 
            return builder.ToString();
        }
    }
}

public sealed record FetchResult(
    Uri Uri,
    bool Success,
    string? Sha256Hex,
    int? ByteCount,
    int? StatusCode,
    string? Error);


*/
//-------------------------------------------------------------------


/*

public class Datafiltering
{

    List<int> numList = new List<int>{5, 12, 7, 20, 3, 18};

    List<int> OrganisedList(List<int> numbers)
    {

            return numbers.Where(n => n > 10)
            .OrderBy(n => n)
            .ToList();

    }

    string GetUserRole(int age)
    {  

        switch (age)
        {
            case <= 0:
                return "Invalid age";

            case int i when i > 0 && i < 13:
                return "Child";

            case int i when i >= 13 && i <= 17:
                return "Teenager";

            case int i when i >= 18 && i <= 64:
                return "Adult";

            case int i when i > 64:
                return "Senior";
            default:
                return "error, wrong input";
        }
    }

    List<User> users = new List<User>(){
        new User { Name = "Alice", Age = 30 },
        new User { Name = "Bob", Age = 15 },
        new User { Name = "Charlie", Age = 70 } 
    };

    User? GetUserByName(string? nam)
    {
        if (String.IsNullOrEmpty(nam))
        {
            Console.WriteLine("Enter a name please");
            return null;
        }

        string name = nam.Trim();
        for (int i = 0; i <= users.Count-1; i++)
        {
            if(String.Equals(users[i].Name, name, StringComparison.OrdinalIgnoreCase))
            {
                return users[i];
            }
        }
        return null;
    }


    string RegisterUser(List<User> users, string name, string email, int age, string password)
    {
        if(String.IsNullOrEmpty(name))
        {
            return "Name is required";
        }
        
        if(!IsValidEmail(email))
        {
            return "Email is invalid";
        }

        if(GetUserRole(age) != "Senior" && GetUserRole(age) != "Adult")
        {
            return "User must be at least 18";
        }

        if(EmailExists(users, email))
        {
            return "Email already exists";
        }


    
        users.Add(new User{Name = name.Trim(), Age = age, Email = email.Trim(), Passwordhash = HashPassword(password)});
        UserDto? dto = GetUserDtoByEmail(users, email);
        
        if (dto != null)
        {
            Console.WriteLine(dto.Name);
            Console.WriteLine(dto.Email);
        }


        return "User registered";
    }

    public string HashPassword(string pass)
    {
        using(SHA256 hash = SHA256.Create())
        {
            byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(pass));
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i< bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }



    UserDto? GetUserDtoByEmail(List<User> users, string email)
    {
        if(EmailExists(users, email) == false)
        {
            return null;
        }
        else
        {
            for(int i = 0; i <= users.Count-1; i++)
            {
                if(users[i].Email == email)
                {
                    UserDto newDTO = new UserDto{Name =users[i].Name, Email =users[i].Email};
                    return newDTO;
                }
            }
            return null;
        }
        
    }




    public bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)){
            return false;
        }

        return email.Contains("@") && email.Contains(".") && !email.Any(char.IsWhiteSpace);
    }

    public bool EmailExists(List<User> users, string email){
    for (int i = 0; i < users.Count; i++)
    {
        if (string.Equals(users[i].Email, email, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
    }

    return false;
}
}

public class User{
        public string Name;
        public int Age;
        public string Email;
        public string Passwordhash;
}

public class UserDto
{
    public string Name;
    public string Email;
}  
*/











public class DTOExercise
{
    List<User> users = new List<User>();

    public string RegisterUser(List<User> users, string name, string email, int age, string password)
    {
        User newUser = new User(name, email, age, HashPassword(password));
        users.Add(newUser);
        Console.WriteLine("User registered");
        return "Registration Successful!";
    }
    public string ValidateName()
    {
        Console.WriteLine("Please type in your name: ");
        Start:
        string username = Console.ReadLine();
        if (string.IsNullOrEmpty(username))
        {
            Console.WriteLine("Name cannot be null or empty. Please try again:");
            goto Start;
        }
        Console.WriteLine("Name Accepted!");
        return username;
    }
    public string ValidateEmail()
    {
       
        bool isemailcorrect = false;
        while (isemailcorrect == false)
        {
            Console.WriteLine("Please type in your email: ");
            string email = Console.ReadLine();
            if (string.IsNullOrEmpty(email) || email.Any(x => Char.IsWhiteSpace(x)))
            {
                Console.WriteLine("Email cannot be null or have spaces");
                continue;
            }
            if (!email.Contains("@"))
            {
                Console.WriteLine("Email Invalid");
                continue;
            }
            if (!email.Contains("."))
            {
                Console.WriteLine("Email Invalid");
                continue;
            }
            if(!EmailIsAvailable(users, email))
            {
                Console.WriteLine("Email in use");
                continue;
            }
           
            return email;
        }  
    }
    public int ValidateAge()
    {
        begin:
        Console.WriteLine("How old are you: ");
        int age = int.Parse(Console.ReadLine());
        switch (age)
        {
            case <= 17:
                Console.WriteLine("You must be 18 or older to enter the website.");
                goto  begin;
            case int i when i > 17 && i < 64:
                Console.WriteLine("You are an adult");
                break;
            case int i when i > 63:
                Console.WriteLine("You are senior");
                break;
        }
        return age;
    }
    public string ValidatePassword()
    {
        StartPass:
        Console.WriteLine("Please write your passeword: ");
        string password = Console.ReadLine();
        if(string.IsNullOrEmpty(password) || password.Any(x => Char.IsWhiteSpace(x)))
        {
            Console.WriteLine("Password is invalid. Please do not have any spaces");
            goto StartPass;
        }  
        return password;      

    }
    public bool EmailIsAvailable(List<User> users, string email)
    {
        for(int i = 0; i<= users.Count-1; i++)
        {
            if(String.Equals(users[i].Email, email, StringComparison.OrdinalIgnoreCase)){
                Console.WriteLine("Email is already in use");
                return false;
            }
        }
        return true;
    }
    public void RegisterPage()
    {
        string name = ValidateName();
        string email = ValidateEmail();
        int age = ValidateAge();
        string password = ValidatePassword();
        RegisterUser(users, name, email, age, password);
    }
    public string HashPassword(string password)
    {
        using (SHA256 hash = SHA256.Create()){
            byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i<bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    UserDto? GetUserDtoByEmail(List<User> users, string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            Console.WriteLine("The email you put in is empty");
            return null;    
        }
        for(int i = 0; i <= users.Count-1; i++)
        {
            if(string.Equals(users[i].Email, email, StringComparison.OrdinalIgnoreCase))
            {
                UserDto userdto = new UserDto(users[i].Name ,users[i].Email);
                return userdto;
            }
        }
        return null;
    }

   

   
}

public class User
{
    public string Name;
    public string Email;
    public int Age;
    public string PasswordHash;
     public User (string name, string email, int age, string passhas)
    {
        Name = name;
        Email = email;
        Age = age;
        PasswordHash = passhas;
    }
}

public class UserDto
{
    public string Name;
    public string Email;
     public UserDto(string name, string email)
    {
        Name = name;
        Email = email;
    }
}


