

using LinkShortener.Models;

namespace LinkShortener.Service
{
    public interface ILinkService
    {
        Task<Link> CreateShortenedLinkAsync(string originalUrl);
        Task<string?> GetOriginalUrlAsync(string shortenedUrl);
    }
}