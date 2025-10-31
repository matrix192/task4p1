using System.Globalization;
using System.Linq;
using task4p1.Models;
using task4p1.Repositories;

namespace task4p1.Services
{
    public class AuthorService
    {
        private readonly IAuthorsRepository _repo;
        private const string DateFormat = "yyyy-MM-dd";

        public AuthorService(IAuthorsRepository repo)
        {
            _repo = repo;
        }

        public List<AuthorDto> GetAll()
        {
            return _repo.GetAllAuthors()
                        .Select(MapToDto)
                        .ToList();
        }

        public AuthorDto? GetById(int id)
        {
            var author = _repo.GetAuthorById(id);
            return author == null ? null : MapToDto(author);
        }

        public (bool Success, string? Error, AuthorDto? Created) Create(AuthorDto dto)
        {
            if (dto == null) return (false, "Body is required.", null);
            if (string.IsNullOrWhiteSpace(dto.Name)) return (false, "Name is required.", null);
            if (!TryParseDate(dto.DateOfBirth, out var date)) return (false, $"DateOfBirth must be in format {DateFormat}.", null);

            var entity = new Author
            {
                Name = dto.Name.Trim(),
                DateOfBirth = date
            };

            _repo.NewAuthor(entity);

            return (true, null, MapToDto(entity));
        }

        public (bool Found, bool Success, string? Error) Update(int id, AuthorDto dto)
        {
            if (dto == null) return (false, false, "Body is required.");
            if (id != dto.Id) return (false, false, "Id in URL and body do not match.");
            var existing = _repo.GetAuthorById(id);
            if (existing == null) return (false, false, null);
            if (string.IsNullOrWhiteSpace(dto.Name)) return (true, false, "Name is required.");
            if (!TryParseDate(dto.DateOfBirth, out var date)) return (true, false, $"DateOfBirth must be in format {DateFormat}.");

            existing.Name = dto.Name.Trim();
            existing.DateOfBirth = date;
            _repo.UpdateAuthor(existing);

            return (true, true, null);
        }

        public bool Delete(int id)
        {
            var existing = _repo.GetAuthorById(id);
            if (existing == null) return false;
            _repo.DeleteAuthor(id);
            return true;
        }

        private static AuthorDto MapToDto(Author a) =>
            new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                DateOfBirth = a.DateOfBirth.ToString(DateFormat, CultureInfo.InvariantCulture)
            };

        private static bool TryParseDate(string? s, out DateTime date)
        {
            date = default;
            if (string.IsNullOrWhiteSpace(s)) return false;

            var str = s.Trim();

            if (DateTime.TryParseExact(str, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                return true;

            return DateTime.TryParse(str, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
        }
    }
}