using Booker.Models;
using Booker.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booker.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;
        private readonly GenreService _genreService;
        private readonly BookService _bookService;

        public AuthorController(
            AuthorService authorService,
            GenreService genreService,
            BookService bookService
        )
        {
            _authorService = authorService;
            _genreService = genreService;
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Author author)
        {
            Author createdAuthor = await _authorService.Add(author);
            return Created(nameof(author), createdAuthor);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { title = "Invalid id" });

            try
            {
                Guid stringToGuid = Guid.Parse(id);

                Author? foundAuthor = await _authorService.FindById(stringToGuid);

                if (foundAuthor is null) return NotFound();

                return Ok(foundAuthor);
            }
            catch (FormatException ex)
            {
                return BadRequest(
                    new
                    {
                        title = "The given id was not valid",
                        errors = ex.Message
                    });
            }
        }

        [HttpGet]
        public async Task<IActionResult> FindAll(
            [FromQuery] int limit = 30,
            [FromQuery] int offset = 0
        )
        {
            if (limit <= 0 || offset < 0)
                return BadRequest(new { title = "Invalid result limit or page number" });

            int correctOffset = offset > 0 ? offset - 1 : 0;

            List<Author> authors = await _authorService.FindAll(limit, correctOffset);

            return Ok(
                new
                {
                    authors,
                    page = correctOffset,
                    limit,
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id)
        {
            Author? authorToUpdate = await _authorService.FindById(id);

            if (authorToUpdate is null) return NotFound();

            Author updatedAuthor = await _authorService.Update(authorToUpdate);

            return Ok(updatedAuthor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                Guid stringToGuid = Guid.Parse(id);

                Author? authorToDelete = await _authorService.FindById(stringToGuid);

                if (authorToDelete is null) return NotFound();

                Author? deletedAuthor = await _authorService.Delete(authorToDelete);

                return Ok(deletedAuthor);
            }
            catch (FormatException ex)
            {
                return BadRequest(
                    new
                    {
                        title = "The given id was not valid",
                        error = ex.Message
                    });
            }
        }

        [HttpPut("associations/{authorId}/genre/{genreId}")]
        public async Task<IActionResult> AssociateGenre(
            [FromForm] Guid authorId,
            [FromForm] Guid genreId
        )
        {
            Author? authorToRelate = await _authorService.FindById(genreId);

            if (authorToRelate is null)
                return NotFound(new { title = "Author not found" });

            Genre? genreToRelate = await _genreService.FindById(authorId);

            if (genreToRelate is null)
                return NotFound(new { title = "Genre not found" });

            authorToRelate.Genres ??= new List<Genre> { genreToRelate };

            authorToRelate.Genres.Add(genreToRelate);

            await _authorService.Update(authorToRelate);

            return Ok(authorToRelate);
        }

        [HttpPut("associations/{authorId}/book/{bookId}")]
        public async Task<IActionResult> AssociateBook(
            [FromForm] Guid authorId,
            [FromForm] Guid bookId
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
