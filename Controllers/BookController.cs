using Booker.Identity;
using Booker.Models;
using Booker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booker.Controllers
{
    [Authorize]
    [RequiresClaim(IdentityData.AdminUserClaimName, "true")]
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly GenreService _genreService;
        private readonly AuthorService _authorService;
        private readonly PublisherService _publisherService;

        public BookController(
            BookService bookService,
            GenreService genreService,
            AuthorService authorService,
            PublisherService publisherService
        )
        {
            _bookService = bookService;
            _genreService = genreService;
            _authorService = authorService;
            _publisherService = publisherService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> FindAll(
            [FromQuery] int limit = 30,
            [FromQuery] int offset = 0
        )
        {
            if (limit <= 0 || offset < 0)
                return BadRequest(new { title = "Invalid result limit or page number" });

            int correctOffset = offset > 0 ? offset - 1 : 0;

            List<Book> books = await _bookService.FindAll(limit, correctOffset);

            return Ok(
                new
                {
                    books,
                    page = correctOffset,
                    limit,
                });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { title = "Invalid id" });

            try
            {
                Guid stringToGuid = Guid.Parse(id);

                Book? foundBook = await _bookService.FindById(stringToGuid);

                if (foundBook is null) return NotFound();

                return Ok(foundBook);
            }
            catch (FormatException)
            {
                return BadRequest(new { title = "The given id was not valid" });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Book book)
        {
            // Author will never be null
            Author? author = await _authorService.FindById((Guid)book.AuthorId!);

            if (author is null) return NotFound(new { title = "Author was not found" });

            Publisher? publisher = await _publisherService.FindById((Guid)book.PublisherId!);

            if (publisher is null) return NotFound(new { title = "Publisher was not found" });

            Book createdBook = await _bookService.Add(book);
            return Created(nameof(book), createdBook);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                Guid stringToGuid = Guid.Parse(id);

                Book? bookToDelete = await _bookService.FindById(stringToGuid);

                if (bookToDelete is null)
                    return NotFound(new { title = "Id not found" });

                Book deletedBook = await _bookService.Delete(bookToDelete);

                return Ok(deletedBook);
            }
            catch (FormatException)
            {
                return BadRequest(new { title = "The given id was not valid" });
            }
        }

        [HttpPut("associations/{bookId}/genre/{genreId}")]
        public async Task<IActionResult> AssociateGenre(
            [FromForm] Guid bookId,
            [FromForm] Guid genreId
        )
        {
            Genre? genreToRelate = await _genreService.FindById(genreId);

            if (genreToRelate is null)
                return NotFound(new { title = "Genre not found" });

            Book? bookToRelate = await _bookService.FindById(bookId);

            if (bookToRelate is null)
                return NotFound(new { title = "Book not found" });

            bookToRelate.Genres ??= new List<Genre> { genreToRelate };

            bookToRelate.Genres.Add(genreToRelate);

            await _bookService.Update(bookToRelate);

            return Ok(bookToRelate);
        }

        [HttpPut("associations/{bookId}/author/{authorId}")]
        public async Task<IActionResult> AssociateAuthor(
            [FromForm] Guid bookId,
            [FromForm] Guid authorId
        )
        {
            Author? authorToRelate = await _authorService.FindById(authorId);

            if (authorToRelate is null)
                return NotFound(new { title = "Author not found" });

            Book? bookToRelate = await _bookService.FindById(bookId);

            if (bookToRelate is null)
                return NotFound(new { title = "Book not found" });

            bookToRelate.AuthorId = authorToRelate.Id;

            await _bookService.Update(bookToRelate);

            return Ok(bookToRelate);
        }
    }
}
