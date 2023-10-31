using Booker.Models;
using Booker.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booker.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService) => _bookService = bookService;

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            List<Book> books = await _bookService.FindAll();

            if (!books.Any())
                return Ok(new { message = "There are any books yet, add some!" });

            return Ok(books);
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

                if (foundBook is null)
                    return NotFound(new { message = "Resource not found" });

                return Ok(foundBook);
            }
            catch (FormatException err)
            {
                return BadRequest(
                    new
                    {
                        message = "The given id was not valid",
                        error = err.Message
                    });
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
                createdBook = await _bookService.Add(book);

                return Created(nameof(book), createdBook);
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                return BadRequest(error);
            }
        }
    }
}