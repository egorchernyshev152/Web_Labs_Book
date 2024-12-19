using BookStore.API.Contracts;

using BookStore.Core.Abstractions;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService) // Внедрение зависимости сервиса книг
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BooksResponse>>> GetBooks() // Получение списка всех книг
        {
            var books = await _booksService.GetAllBooks();
            var response = books.Select(b => new BooksResponse(b.Id, b.Title, b.Author, b.Price)); // Преобразование доменных моделей в DTO

            return Ok(response); 
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBook([FromBody] BooksRequest request) // Создание новой книги
        {
            var (book, error) = Book.Create(Guid.NewGuid(), request.Title, request.Author, request.Price); // Валидация и создание доменной модели

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error); 
            }

            var bookId = await _booksService.CreateBook(book); // Вызов метода сервиса для создания книги

            return Ok(bookId); 
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBooks(Guid id, [FromBody] BooksRequest request) // Обновление книги
        {
            var bookId = await _booksService.UpdateBook(id, request.Title, request.Author, request.Price); // Вызов метода сервиса для обновления

            return Ok(bookId); 
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBook(Guid id) // Удаление книги
        {
            return Ok(await _booksService.DeleteBook(id)); 
        }
    }
}
