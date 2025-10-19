using Microsoft.AspNetCore.Mvc;
using task4p1.Models;
using task4p1.Repositories;

namespace task4p1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsRepository _repo;

        public AuthorsController(IAuthorsRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<List<Author>> GetAll()
        {
            return Ok(_repo.GetAllAuthors());
        }

        [HttpGet("{id}", Name = "GetAuthorById")]
        public ActionResult<Author> GetById(int id)
        {
            var author = _repo.GetAuthorById(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public ActionResult<Author> Create(Author author)
        {
            _repo.NewAuthor(author);
            return CreatedAtRoute("GetAuthorById", new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Author author)
        {
            if (id != author.Id) return BadRequest();
            var existing = _repo.GetAuthorById(id);
            if (existing == null) return NotFound();
            _repo.UpdateAuthor(author);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _repo.GetAuthorById(id);
            if (existing == null) return NotFound();
            _repo.DeleteAuthor(id);
            return NoContent();
        }
    }
}
