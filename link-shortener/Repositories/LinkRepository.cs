using LinkShortener.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Service
{
    public class LinkRepository : ILinkRepository
    {
        private readonly ApplicationDbContext _context;

        public LinkRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Link> GetLinkByIdAsync(int id)
        {
            return await _context.Links.FirstAsync(l => l.Id == id);
        }

        public async Task<Link?> GetLinkByShortenedUrlAsync(string shortenedUrl)
        {
            return await _context.Links.FirstOrDefaultAsync(l => l.ShortenedUrl == shortenedUrl);
        }

        public async Task AddLinkAsync(Link link)
        {
            await _context.Links.AddAsync(link);
            await _context.SaveChangesAsync();
        }

        public async Task<Link?> GetLinkByOriginalUrlAsync(string originalUrl)
        {
            return await _context.Links.FirstOrDefaultAsync(l => l.OriginalUrl == originalUrl);
        }

        public async Task<List<Link>> GetAllLinks()
        {
            return await _context.Links.Where(l => l.ExpiresAt > DateTime.UtcNow).ToListAsync();
        }
    }
}
