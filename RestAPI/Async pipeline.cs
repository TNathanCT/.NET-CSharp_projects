using System.Net;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;

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
