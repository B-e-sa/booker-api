using Booker.Models;
using Booker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Identity;

namespace Booker.Controllers
{
    [Authorize(Policy=IdentityData.AdminUserPolicyName)]
    [Route("[controller]")]
    [ApiController]
    class PublisherController : ControllerBase
    {
        private readonly PublisherService _publisherService;
        private readonly AuthorService _authorService;
        private readonly BookService _bookService;

        public PublisherController(
            PublisherService publisherService,
            AuthorService authorService,
            BookService bookService
        )
        {
            _publisherService = publisherService;
            _authorService = authorService;
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Publisher publisher)
        {
            Publisher? createdPublisher = await _publisherService.Add(publisher);
            return Created(nameof(publisher), createdPublisher);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { title = "Invalid id" });

            try
            {
                Guid stringToGuid = Guid.Parse(id);

                Publisher? foundPublisher = await _publisherService.FindById(stringToGuid);

                if (foundPublisher is null) return NotFound();

                return Ok(foundPublisher);
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

            List<Publisher> publishers = await _publisherService.FindAll(limit, correctOffset);

            return Ok(
                new
                {
                    publishers,
                    page = correctOffset,
                    limit,
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id)
        {
            Publisher? publisherToUpdate = await _publisherService.FindById(id);

            if (publisherToUpdate is null) return NotFound();

            Publisher updatedPublisher = await _publisherService.Update(publisherToUpdate);

            return Ok(updatedPublisher);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                Guid stringToGuid = Guid.Parse(id);

                Publisher? authorToDelete = await _publisherService.FindById(stringToGuid);

                if (authorToDelete is null) return NotFound();

                Publisher? deletedAuthor = await _publisherService.Delete(authorToDelete);

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

        [HttpPut("associations/{publisherId}/books/{bookId}")]
        public async Task<IActionResult> AssociateBook(
            [FromForm] Guid bookId,
            [FromForm] Guid genreId
        )
        {
            Publisher? publisherToRelate = await _publisherService.FindById(genreId);

            if (publisherToRelate is null)
                return NotFound(new { title = "Publisher not found" });

            Book? bookToRelate = await _bookService.FindById(bookId);

            if (bookToRelate is null)
                return NotFound(new { title = "Book not found" });

            publisherToRelate.Books ??= new List<Book> { bookToRelate };

            publisherToRelate.Books.Add(bookToRelate);

            await _bookService.Update(bookToRelate);

            return Ok(bookToRelate);
        }

        [HttpPut("associations/{publisherId}/author/{authorId}")]
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