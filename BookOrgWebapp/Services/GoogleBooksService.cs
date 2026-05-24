using System.Net;
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

    public virtual async Task<(GoogleBooksResponse? Data, HttpStatusCode StatusCode)> GetBookByISBN(string isbn)
    {
        var apiKey = _configuration["GoogleBooks:ApiKey"];
        var url = $"https://www.googleapis.com/books/v1/volumes?q=isbn:{isbn}&key={apiKey}";

        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<GoogleBooksResponse>();
            return (data, response.StatusCode);
        }

        return (null, response.StatusCode);
    }
}
