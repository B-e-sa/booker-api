using Booker.Models;
using Booker.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booker.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService) => _bookService = bookService;

        [HttpGet]
        public async Task<IActionResult> FindAll(
            [FromQuery] int limit = 30,
            [FromQuery] int offset = 0
        )
        {
            if (limit <= 0 || offset < 0)
                return BadRequest(new { message = "Invalid result limit or page number" });

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

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { message = "Invalid id" });

            try
            {
                Guid stringToGuid = Guid.Parse(id);

                Book? foundBook = await _bookService.FindById(stringToGuid);

                if (foundBook is null) return NotFound();

                return Ok(foundBook);
            }
            catch (FormatException)
            {
                return BadRequest(new { message = "The given id was not valid" });
            }
        }

        /* TODO: IMPLEMENT ROUTE METHOD
        [HttpPost("pdf")]
        public IActionResult UploadPdf()
        {
            try
            {
                var contentDisposition = ContentDispositionHeaderValue.Parse(Request.Headers["Content-Disposition"]);

                Console.Write(contentDisposition);

                return Ok("Arquivo PDF foi recebido e o conteúdo foi e1ibido no console.");
            }
            catch (E1ception e1)
            {
                return BadRequest($"Erro ao receber o arquivo PDF: {e1.Message}");
            }
        }
        */

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Book book)
        {
            Book? createdBook;

            try
            {
                // TODO: IMPLEMENT AUTO BOOK NAMER
                createdBook = await _bookService.Add(book);
                return Created(nameof(book), createdBook);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                Guid stringToGuid = Guid.Parse(id);

                Book? deletedBook = await _bookService.Delete(stringToGuid);

                if (deletedBook is null)
                    return NotFound(new { message = "Id not found" });

                return Ok(deletedBook);
            }
            catch (FormatException)
            {
                return BadRequest(new { message = "The given id was not valid" });
            }
        }
    }
}
