using BooksAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private static List<Book> _books = new List<Book>
        {
            new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Isbn = "978-0743273565", PublicationDate = new DateTime(1925, 4, 10) },
            new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Isbn = "978-0061120084", PublicationDate = new DateTime(1960, 7, 11) },
            new Book { Id = 3, Title = "1984", Author = "George Orwell", Isbn = "978-0451524935", PublicationDate = new DateTime(1949, 6, 8) }
        };
        private static int _nextId = 4;

        [HttpGet]
        public ActionResult<List<Book>> GetAll()
        {
            return Ok(_books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetById(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            book.Id = _nextId++;
            _books.Add(book);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public ActionResult<Book> Update(int id, Book book)
        {
            var existing = _books.FirstOrDefault(b => b.Id == id);
            if (existing == null)
                return NotFound();

            existing.Title = book.Title;
            existing.Author = book.Author;
            existing.Isbn = book.Isbn;
            existing.PublicationDate = book.PublicationDate;
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();

            _books.Remove(book);
            return NoContent();
        }
    }
}
