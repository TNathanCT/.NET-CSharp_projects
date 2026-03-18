Exercise #3: Async pipeline with cancellation + bounded concurrency. Implement a service that downloads many URLs concurrently (max 8 at a time), computes SHA-256, and saves results. It must support cancellation properly.


  using System.Net;
using System.Security.Cryptography;

public sealed class FetchHasher
{
    private readonly HttpClient _http;

    public FetchHasher(HttpClient http) => _http = http;

    public async Task<IReadOnlyList<FetchResult>> FetchAndHashAsync(
        IEnumerable<Uri> uris,
        int maxConcurrency,
        CancellationToken cancellationToken)
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
                // 2) stream content, compute SHA256
                // 3) record byte count + status code
                // 4) handle errors -> results[index] = failed FetchResult
            }
            finally
            {
                semaphore.Release();
            }
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
