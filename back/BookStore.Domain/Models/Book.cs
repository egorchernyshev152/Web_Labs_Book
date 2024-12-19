namespace BookStore.Core.Models
{
    public class Book
    {
        public const int MAX_TITLE_LENGTH = 250; 

        private Book(Guid id, string title, string author, decimal price) // Приватный конструктор для контроля создания экземпляров
        {
            Id = id;
            Title = title;
            Author = author;
            Price = price;
        }

        public Guid Id { get; } // Уникальный идентификатор книги 
        public string Title { get; } = string.Empty; // Название книги 
        public string Author { get; } = string.Empty; // Описание книги 
        public decimal Price { get; } // Цена книги 

        public static (Book Book, string Error) Create(Guid id, string title, string author, decimal price)
        {
            var error = string.Empty;

            // Проверка длины и пустоты названия книги
            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
            {
                error = "Название должено быть не более 250 символов"; 
            }

            var book = new Book(id, title, author, price); // Создание объекта через приватный конструктор

            return (book, error); // Возврат объекта и ошибки (если есть)
        }
    }
}


