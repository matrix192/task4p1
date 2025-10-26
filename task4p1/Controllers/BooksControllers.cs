using Microsoft.AspNetCore.Mvc;
using task4p1.Models;
using task4p1.Services;

namespace task4p1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _service;

        public BooksController(BookService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<BookDto>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}", Name = "GetBookById")]
        public ActionResult<BookDto> GetById(int id)
        {
            var book = _service.GetById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<BookDto> Create(BookDto book)
        {
            var (success, error, created) = _service.Create(book);
            if (!success)
            {
                return BadRequest(new { error });
            }

            return CreatedAtRoute("GetBookById", new { id = created!.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BookDto book)
        {
            var (found, success, error) = _service.Update(id, book);
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