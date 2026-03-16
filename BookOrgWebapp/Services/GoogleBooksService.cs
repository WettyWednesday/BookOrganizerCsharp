using System.Net.Http.Json;

public class GoogleBooksService
{
    private readonly HttpClient _client;

    public GoogleBooksService(HttpClient httpClient)
    {
        _client = httpClient;
    }

    public async Task<GoogleBooksResponse?> GetBookByISBN(string isbn)
    {
        var url = $"https://www.googleapis.com/books/v1/volumes?q=isbn:{isbn}";
        return await _client.GetFromJsonAsync<GoogleBooksResponse>(url);
    }
}