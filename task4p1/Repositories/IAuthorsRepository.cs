using task4p1.Models;

namespace task4p1.Repositories
{
    public interface IAuthorsRepository
    {
        void NewAuthor(Author author);
        void DeleteAuthor(int authorId);
        void UpdateAuthor(Author author);
        Author? GetAuthorById(int authorId);
        List<Author> GetAllAuthors();
    }
}
