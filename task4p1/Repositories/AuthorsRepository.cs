using task4p1.Models;
using System.Linq;

namespace task4p1.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly List<Author> authors = new();
        private int nextId = 1;

        public void NewAuthor(Author author)
        {
            author.Id = nextId++;
            authors.Add(new Author
            {
                Id = author.Id,
                Name = author.Name,
                DateOfBirth = author.DateOfBirth
            });
        }

        public List<Author> GetAllAuthors()
        {
            return authors;
        }

        public void DeleteAuthor(int authorId)
        {
            var authorToDelete = authors.FirstOrDefault(a => a.Id == authorId);
            if (authorToDelete != null)
            {
                authors.Remove(authorToDelete);
            }
        }

        public Author? GetAuthorById(int authorId)
        {
            return authors.FirstOrDefault(a => a.Id == authorId);
        }

        public void UpdateAuthor(Author author)
        {
            var authorToUpdate = authors.FirstOrDefault(a => a.Id == author.Id);
            if (authorToUpdate != null)
            {
                authorToUpdate.Name = author.Name;
                authorToUpdate.DateOfBirth = author.DateOfBirth;
            }
        }
    }
}
