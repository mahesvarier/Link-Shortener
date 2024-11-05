using LinkShortener.Utilities;
using LinkShortener.Models;

namespace LinkShortener.Service
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository _linkRepository;

        public LinkService(ILinkRepository linkRepository)
        {
            _linkRepository = linkRepository;
        }

        public async Task<Models.Link> CreateShortenedLinkAsync(string originalUrl)
        {
            var existingLink = await _linkRepository.GetLinkByOriginalUrlAsync(originalUrl);
            if (existingLink != null)
            {
                return existingLink;
            }
            string shortenedUrl;
            do
            {
                shortenedUrl = StaticUrlShortener.GenerateShortenedUrl();
            } while (await _linkRepository.GetLinkByShortenedUrlAsync(shortenedUrl) != null);

            var link = new Link
            {
                OriginalUrl = originalUrl,
                ShortenedUrl = shortenedUrl,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddYears(1)
            };

            await _linkRepository.AddLinkAsync(link);
            return link;
        }

        public async Task<string?> GetOriginalUrlAsync(string shortenedUrl)
        {
            var link = await _linkRepository.GetLinkByShortenedUrlAsync(shortenedUrl);
            if (link == null)
            {
                return null;
            }
            return link.OriginalUrl;
        }
    }
}
