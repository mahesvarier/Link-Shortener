using LinkShortener.Models;

public interface ILinkRepository
{
    Task<Link> GetLinkByIdAsync(int id);
    Task<Link?> GetLinkByShortenedUrlAsync(string shortenedUrl);
    Task AddLinkAsync(Link link);
    Task<Link?> GetLinkByOriginalUrlAsync(string originalUrl);
}