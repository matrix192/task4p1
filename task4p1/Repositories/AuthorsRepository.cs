using Microsoft.EntityFrameworkCore;
using task4p1.Data;
using task4p1.Models;

namespace task4p1.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly LibraryContext _context;

        public AuthorsRepository(LibraryContext context)
        {
            _context = context;
        }

        public void NewAuthor(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public List<Author> GetAllAuthors()
        {
            return _context.Authors
                           .Include(a => a.Books)
                           .ToList();
        }

        public void DeleteAuthor(int authorId)
        {
            var authorToDelete = _context.Authors.Find(authorId);
            if (authorToDelete != null)
            {
                _context.Authors.Remove(authorToDelete);
                _context.SaveChanges();
            }
        }

        public Author? GetAuthorById(int authorId)
        {
            return _context.Authors
                           .Include(a => a.Books)
                           .FirstOrDefault(a => a.Id == authorId);
        }

        public void UpdateAuthor(Author author)
        {
            var existing = _context.Authors.Find(author.Id);
            if (existing != null)
            {
                existing.Name = author.Name;
                existing.DateOfBirth = author.DateOfBirth;
                _context.SaveChanges();
            }
        }
    }
}