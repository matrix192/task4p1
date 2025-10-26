using System.Globalization;
using System.Linq;
using task4p1.Models;
using task4p1.Repositories;

namespace task4p1.Services
{
    public class BookService
    {
        private readonly IBooksRepository _repo;
        private readonly IAuthorsRepository _authorsRepo;

        public BookService(IBooksRepository repo, IAuthorsRepository authorsRepo)
        {
            _repo = repo;
            _authorsRepo = authorsRepo;
        }

        public List<BookDto> GetAll()
        {
            return _repo.GetAllBooks()
                        .Select(MapToDto)
                        .ToList();
        }

        public BookDto? GetById(int id)
        {
            var book = _repo.GetBookById(id);
            return book == null ? null : MapToDto(book);
        }

        public (bool Success, string? Error, BookDto? Created) Create(BookDto dto)
        {
            if (dto == null) return (false, "Body is required.", null);
            if (string.IsNullOrWhiteSpace(dto.Title)) return (false, "Title is required.", null);
            if (!IsPublishedYearValid(dto.PublishedYear)) return (false, "PublishedYear is invalid.", null);

            if (_authorsRepo.GetAuthorById(dto.AuthorId) == null)
            {
                return (false, "AuthorId does not reference an existing author.", null);
            }

            var entity = new Book
            {
                Title = dto.Title.Trim(),
                PublishedYear = dto.PublishedYear,
                AuthorId = dto.AuthorId
            };

            _repo.NewBook(entity);

            return (true, null, MapToDto(entity));
        }

        public (bool Found, bool Success, string? Error) Update(int id, BookDto dto)
        {
            if (dto == null) return (false, false, "Body is required.");
            if (id != dto.Id) return (false, false, "Id in URL and body do not match.");
            var existing = _repo.GetBookById(id);
            if (existing == null) return (false, false, null);
            if (string.IsNullOrWhiteSpace(dto.Title)) return (true, false, "Title is required.");
            if (!IsPublishedYearValid(dto.PublishedYear)) return (true, false, "PublishedYear is invalid.");
            if (_authorsRepo.GetAuthorById(dto.AuthorId) is null) return (true, false, "AuthorId does not reference an existing author.");

            existing.Title = dto.Title.Trim();
            existing.PublishedYear = dto.PublishedYear;
            existing.AuthorId = dto.AuthorId;
            _repo.UpdateBook(existing);

            return (true, true, null);
        }

        public bool Delete(int id)
        {
            var existing = _repo.GetBookById(id);
            if (existing == null) return false;
            _repo.DeleteBook(id);
            return true;
        }

        private static BookDto MapToDto(Book b) =>
            new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                PublishedYear = b.PublishedYear,
                AuthorId = b.AuthorId
            };

        private static bool IsPublishedYearValid(int year)
        {
            if (year <= 0) return false;
            var current = DateTime.UtcNow.Year;
            return year <= current + 1; // allow next-year publications if needed
        }
    }
}