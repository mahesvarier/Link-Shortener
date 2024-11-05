using LinkShortener.Service;
using Microsoft.AspNetCore.Mvc;
using LinkShortener.Models;

namespace LinkShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LinkController : ControllerBase
    {
        private readonly ILinkService _linkService;
        private readonly ILogger<LinkController> _logger;
        public LinkController(ILinkService linkService, ILogger<LinkController> logger)
        {
            _linkService = linkService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShortenedLink([FromBody] CreateShortenedLinkRequest createShortenedLinkRequest)
        {
            try
            {
                var link = await _linkService.CreateShortenedLinkAsync(createShortenedLinkRequest);
                var response = new CreateShortenedLinkResponse { ShortenedUrl = link.ShortenedUrl };
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while creating the shortened link.");
                return StatusCode(500, "An error occurred while creating the shortened link.");
            }
        }

        [HttpGet("{shortenedUrl}")]
        public async Task<IActionResult> GetOriginalUrl(string shortenedUrl)
        {
            try
            {
                var originalUrl = await _linkService.GetOriginalUrlAsync(shortenedUrl);
                if (originalUrl == null)
                {
                    return NotFound();
                }

                return Ok(new { OriginalUrl = originalUrl });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while retrieving the original URL.");
                return StatusCode(500, "An error occurred while retrieving the original URL.");
            }
        }

        [HttpGet("GetAllUrls")]
        public async Task<IActionResult> GetAllUrls()
        {
            try
            {
                var originalUrl = await _linkService.GetAllUrls();
                if (originalUrl == null)
                {
                    return NotFound();
                }
                return Ok(originalUrl);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while retrieving the original URL.");
                return StatusCode(500, "An error occurred while retrieving the original URL.");
            }
        }
    }
}

