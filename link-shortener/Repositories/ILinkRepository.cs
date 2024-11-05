using LinkShortener.Models;

public interface ILinkRepository
{
    Task<Link?> GetLinkByShortenedUrlAsync(string shortenedUrl);
    Task AddLinkAsync(Link link);
    Task<Link?> GetLinkByOriginalUrlAsync(string originalUrl);
    Task<List<Link>> GetAllLinks();
}