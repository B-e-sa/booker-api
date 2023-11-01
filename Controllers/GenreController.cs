using Booker.Models;
using Booker.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booker.Controllers
{
    [Route("[controller]")]
    [ApiController]
    class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService) => _genreService = genreService;

        public async Task<IActionResult> FindAll(
            [FromQuery] int limit = 30,
            [FromQuery] int offset = 0
        )
        {
            if (limit <= 0 || offset < 0)
                return BadRequest(new { message = "Invalid result limit or page number" });

            int correctOffset = offset > 0 ? offset - 1 : 0;

            List<Genre> genres = await _genreService.FindAll(limit, correctOffset);

            return Ok(
                new
                {
                    genres,
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

                Genre? foundGenre = await _genreService.FindById(stringToGuid);

                if (foundGenre is null) return NotFound();

                return Ok(foundGenre);
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
        public async Task<IActionResult> Add([FromForm] Genre genre)
        {
            // TODO: IMPLEMENT AUTO BOOK NAMER
            Genre createdGenre = await _genreService.Add(genre);
            return Created(nameof(genre), createdGenre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                Guid stringToGuid = Guid.Parse(id);

                Genre? deletedGenre = await _genreService.Delete(stringToGuid);

                if (deletedGenre is null)
                    return NotFound(new { message = "Id not found" });

                return Ok(deletedGenre);
            }
            catch (FormatException)
            {
                return BadRequest(new { message = "The given id was not valid" });
            }
        }
    }
}
