using task4p1.Models;
using System.Linq;

namespace task4p1.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly List<Book> books = new();
        private int nextId = 1;

        public void NewBook(Book book)
        {
            book.Id = nextId++;
            books.Add(new Book
            {
                Id = book.Id,
                Title = book.Title,
                PublishedYear = book.PublishedYear,
                AuthorId = book.AuthorId
            });
        }

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public void DeleteBook(int bookId)
        {
            var bookToDelete = books.FirstOrDefault(b => b.Id == bookId);
            if (bookToDelete != null)
            {
                books.Remove(bookToDelete);
            }
        }

        public Book? GetBookById(int bookId)
        {
            return books.FirstOrDefault(b => b.Id == bookId);
        }

        public void UpdateBook(Book book)
        {
            var bookToUpdate = books.FirstOrDefault(b => b.Id == book.Id);
            if (bookToUpdate != null)
            {
                bookToUpdate.Title = book.Title;
                bookToUpdate.PublishedYear = book.PublishedYear;
                bookToUpdate.AuthorId = book.AuthorId;
            }
        }
    }
}
