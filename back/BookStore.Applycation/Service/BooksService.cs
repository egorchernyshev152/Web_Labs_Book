

using BookStore.Core.Abstractions;
using BookStore.Core.Models;

namespace BookStore.Application.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;

        public BooksService(IBooksRepository booksRepository) // Внедрение зависимости репозитория книг
        {
            _booksRepository = booksRepository;
        }

        public async Task<List<Book>> GetAllBooks() // Получение списка всех книг
        {
            return await _booksRepository.Get(); // Делегирование операции репозиторию
        }

        public async Task<Guid> CreateBook(Book book) // Создание книги
        {
            return await _booksRepository.Create(book); // Делегирование операции репозиторию
        }

        public async Task<Guid> UpdateBook(Guid id, string title, string author, decimal price) // Обновление книги
        {
            return await _booksRepository.Update(id, title, author, price); // Делегирование операции репозиторию
        }

        public async Task<Guid> DeleteBook(Guid id) // Удаление книги
        {
            return await _booksRepository.Delete(id); // Делегирование операции репозиторию
        }
    }
}
