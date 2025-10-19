using task4p1.Models;

namespace task4p1.Repositories
{
    public interface IBooksRepository
    {
        void NewBook(Book book);
        void DeleteBook(int bookId);
        void UpdateBook(Book book);
        Book? GetBookById(int bookId);
        List<Book> GetAllBooks();
    }
}
