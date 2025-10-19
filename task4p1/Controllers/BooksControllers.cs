using Microsoft.AspNetCore.Mvc;
using task4p1.Models;
using task4p1.Repositories;

namespace task4p1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _repo;

        public BooksController(IBooksRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<List<Book>> GetAll()
        {
            return Ok(_repo.GetAllBooks());
        }

        [HttpGet("{id}", Name = "GetBookById")]
        public ActionResult<Book> GetById(int id)
        {
            var book = _repo.GetBookById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _repo.NewBook(book);
            return CreatedAtRoute("GetBookById", new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Book book)
        {
            if (id != book.Id) return BadRequest();
            var existing = _repo.GetBookById(id);
            if (existing == null) return NotFound();
            _repo.UpdateBook(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _repo.GetBookById(id);
            if (existing == null) return NotFound();
            _repo.DeleteBook(id);
            return NoContent();
        }
    }
}
