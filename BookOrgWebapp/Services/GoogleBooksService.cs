using System.Net.Http.Json;

public class GoogleBooksService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;

    public GoogleBooksService(HttpClient httpClient, IConfiguration configuration)
    {
        _client = httpClient;
        _configuration = configuration;
    }

    public async Task<GoogleBooksResponse?> GetBookByISBN(string isbn)
    {
        var apiKey = _configuration["GoogleBooks:ApiKey"];
        var url = $"https://www.googleapis.com/books/v1/volumes?q=isbn:{isbn}&key={apiKey}";

        return await _client.GetFromJsonAsync<GoogleBooksResponse>(url);
    }
}
