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
    class GenreController : ControllerBase
    {
        private readonly GenreService _genreService;

        public GenreController(GenreService genreService) => _genreService = genreService;

        [AllowAnonymous]
        public async Task<IActionResult> FindAll(
            [FromQuery] int limit = 30,
            [FromQuery] int offset = 0
        )
        {
            if (limit <= 0 || offset <= 0)
                return BadRequest(new { title = "Invalid result limit or page number" });

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

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { title = "Invalid id" });

            try
            {
                Guid stringToGuid = Guid.Parse(id);

                Genre? foundGenre = await _genreService.FindById(stringToGuid);

                if (foundGenre is null) return NotFound();

                return Ok(foundGenre);
            }
            catch (FormatException)
            {
                return BadRequest(new { title = "The given id was not valid" });
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

                Genre? genreToDelete = await _genreService.FindById(stringToGuid);

                if (genreToDelete is null) return NotFound();

                Genre deletedGenre = await _genreService.Delete(genreToDelete);

                return Ok(deletedGenre);
            }
            catch (FormatException)
            {
                return BadRequest(new { title = "The given id was not valid" });
            }
        }
    }
}
