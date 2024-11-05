using LinkShortener.Service;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LinkController : ControllerBase
    {
        private readonly ILinkService _linkService;

        public LinkController(ILinkService linkService)
        {
            _linkService = linkService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShortenedLink([FromBody] string originalUrl)
        {
            var link = await _linkService.CreateShortenedLinkAsync(originalUrl);
            return Ok(link.ShortenedUrl);
        }

        [HttpGet("{shortenedUrl}")]
        public async Task<IActionResult> GetOriginalUrl(string shortenedUrl)
        {
            var originalUrl = await _linkService.GetOriginalUrlAsync(shortenedUrl);
            if (originalUrl == null)
            {
                return NotFound();
            }
            return Ok(originalUrl);
        }
    }
}

