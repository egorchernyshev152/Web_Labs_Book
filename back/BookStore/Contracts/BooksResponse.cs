namespace BookStore.API.Contracts
{
    public record BooksResponse(Guid Id, string Title, string Author, decimal Price);
}
