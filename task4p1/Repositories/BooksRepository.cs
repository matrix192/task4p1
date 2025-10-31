using Microsoft.EntityFrameworkCore;
using task4p1.Data;
using task4p1.Models;

namespace task4p1.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly LibraryContext _context;

        public BooksRepository(LibraryContext context)
        {
            _context = context;
        }

        public void NewBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books
                           .Include(b => b.Author)
                           .ToList();
        }

        public void DeleteBook(int bookId)
        {
            var bookToDelete = _context.Books.Find(bookId);
            if (bookToDelete != null)
            {
                _context.Books.Remove(bookToDelete);
                _context.SaveChanges();
            }
        }

        public Book? GetBookById(int bookId)
        {
            return _context.Books
                           .Include(b => b.Author)
                           .FirstOrDefault(b => b.Id == bookId);
        }

        public void UpdateBook(Book book)
        {
            var existing = _context.Books.Find(book.Id);
            if (existing != null)
            {
                existing.Title = book.Title;
                existing.PublishedYear = book.PublishedYear;
                existing.AuthorId = book.AuthorId;
                _context.SaveChanges();
            }
        }
    }
}