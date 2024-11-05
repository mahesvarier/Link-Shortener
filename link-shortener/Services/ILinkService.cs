

using LinkShortener.Models;

namespace LinkShortener.Service
{
    public interface ILinkService
    {
        Task<Link> CreateShortenedLinkAsync(CreateShortenedLinkRequest createShortenedLinkRequest);
        Task<string?> GetOriginalUrlAsync(string shortenedUrl);
        Task<List<string>> GetAllUrls();
    }
}