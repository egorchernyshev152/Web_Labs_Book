namespace BookStore.API.Contracts
{
    public record BooksRequest(string Title, string Author, decimal Price);
}
