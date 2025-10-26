using Microsoft.AspNetCore.Mvc;
using task4p1.Models;
using task4p1.Services;

namespace task4p1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _service;

        public AuthorsController(AuthorService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<AuthorDto>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}", Name = "GetAuthorById")]
        public ActionResult<AuthorDto> GetById(int id)
        {
            var author = _service.GetById(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public ActionResult<AuthorDto> Create(AuthorDto author)
        {
            var (success, error, created) = _service.Create(author);
            if (!success)
            {
                return BadRequest(new { error });
            }

            return CreatedAtRoute("GetAuthorById", new { id = created!.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, AuthorDto author)
        {
            var (found, success, error) = _service.Update(id, author);
            if (!found) return NotFound();
            if (!success) return BadRequest(new { error });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_service.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}