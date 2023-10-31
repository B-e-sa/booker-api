using Booker.Models;
using Booker.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booker.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService) => _authorService = authorService;

        [HttpPost]
        public async Task<IActionResult> Add(Author author)
        {
            Author? createdAuthor;

            try
            {
                // TODO: IMPLEMENT AUTO BOOK NAMER
                createdAuthor = await _authorService.Add(author);
                return Created(nameof(author), createdAuthor);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(
                    new
                    {
                        message = "Book ISBN already exists ",
                        error = ex.Message,
                    });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { message = "Invalid id" });

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
                        message = "The given id was not valid",
                        error = ex.Message
                    });
            }
        }

        [HttpGet]
        public async Task<IActionResult> FindAll([FromQuery] int limit = 30, [FromQuery] int offset = 0)
        {
            if (limit <= 0 || offset < 0)
                return BadRequest(new { message = "Invalid result limit or page number" });

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

        /*
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id)
        {

        }
        */

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                Guid stringToGuid = Guid.Parse(id);

                Author? deletedAuthor = await _authorService.Delete(stringToGuid);

                if (deletedAuthor is null) return NotFound();

                return Ok(deletedAuthor);
            }
            catch (FormatException ex)
            {
                return BadRequest(
                    new
                    {
                        message = "The given id was not valid",
                        error = ex.Message
                    });
            }
        }
    }
}
