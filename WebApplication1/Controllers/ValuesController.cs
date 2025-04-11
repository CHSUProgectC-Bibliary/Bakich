using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookReviewAPI.Data;
using BookReviewAPI.Models;

namespace BookReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        // Получить все книги
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.Include(b => b.Reviews).ToListAsync();
        }

        // Получить книгу по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.Include(b => b.Reviews).FirstOrDefaultAsync(b => b.Id == id);
            if (book == null) return NotFound();
            return book;
        }

        // Добавить книгу
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // Добавить рецензию к книге
        [HttpPost("{bookId}/review")]
        public async Task<ActionResult> AddReview(int bookId, Review review)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null) return NotFound();

            review.BookId = bookId;
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Удалить книгу
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
