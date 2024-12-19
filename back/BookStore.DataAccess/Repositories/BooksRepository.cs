using BookStore.Core.Abstractions;
using BookStore.Core.Models;

using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BookStoreDbContext _context;

        public BooksRepository(BookStoreDbContext context) // Внедрение зависимости контекста базы данных
        {
            _context = context;
        }

        public async Task<List<Book>> Get() // Получение списка всех книг
        {
            var bookEntities = await _context.Books
                                             .AsNoTracking() 
                                             .ToListAsync();
            var books = bookEntities
                .Select(b => Book.Create(b.Id, b.Title, b.Author, b.Price).Book) // Преобразование сущностей в доменные модели
                .ToList();

            return books;
        }

        public async Task<Guid> Create(Book book) // Создание новой книги
        {
            var bookEntity = new BookEntity
            {
                // Преобразование доменной модели в сущность
                Id = book.Id, 
                Title = book.Title,
                Author = book.Author,
                Price = book.Price,
            };

            await _context.Books.AddAsync(bookEntity); // Добавление сущности в контекст
            await _context.SaveChangesAsync(); // Сохранение изменений в базе данных

            return bookEntity.Id; // Возврат идентификатора созданной книги
        }

        public async Task<Guid> Update(Guid id, string title, string author, decimal price) // Обновление данных книги
        {
            await _context.Books
                 .Where(b => b.Id == id) 
                 .ExecuteUpdateAsync(s => s
                     .SetProperty(b => b.Title, b => title) 
                     .SetProperty(b => b.Author, b => author) 
                     .SetProperty(b => b.Price, b => price)); 

            return id; // Возврат идентификатора обновленной книги
        }

        public async Task<Guid> Delete(Guid id) // Удаление книги
        {
            await _context.Books
                .Where(b => b.Id == id) 
                .ExecuteDeleteAsync(); 

            return id; // Возврат идентификатора удаленной книги
        }
    }
}
