using System.Net;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices.Swift;
using System.Reflection.Metadata;

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



    //-------------------------------------------------------------------




public class Datafiltering
{

    List<int> numList = new List<int>{5, 12, 7, 20, 3, 18};

    List<int> OrganisedList(List<int> numbers)
    {

            return numbers.Where(n => n > 10)
            .OrderBy(n => n)
            .ToList();
        /*
        numbers.OrderBy(i => i).ToList();
        for(int i = 0; i<= numbers.Count-1; i++)
        {
            if(numbers[i] < 10)
            {
                numbers.RemoveAt(i);
            }
        }
        return numbers;
        */

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

    User GetUserByName(string name)
}

public class User{
        public string Name;
        public int Age;
}

