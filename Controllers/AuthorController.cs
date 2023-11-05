using Booker.Models;
using Booker.Services.Models;
using Microsoft.AspNetCore.Mvc;

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
                        error = ex.Message
                    });
            }
        }

        [HttpGet]
        public async Task<IActionResult> FindAll([FromQuery] int limit = 30, [FromQuery] int offset = 0)
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
    }
}
