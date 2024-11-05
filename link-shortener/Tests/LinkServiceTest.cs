using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Models;
using LinkShortener.Service;
using LinkShortener.Utilities;
using Moq;
using Xunit;

namespace LinkShortener.Tests
{
    public class LinkServiceTest
    {
        private readonly Mock<ILinkRepository> _linkRepositoryMock;
        private readonly LinkService _linkService;

        public LinkServiceTest()
        {
            _linkRepositoryMock = new Mock<ILinkRepository>();
            _linkService = new LinkService(_linkRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateShortenedLinkAsync_OriginalUrlExists_ReturnsExistingLink()
        {
            // Arrange
            var originalUrl = "http://example.com";
            var existingLink = new Link { OriginalUrl = originalUrl, ShortenedUrl = "abc123" };
            _linkRepositoryMock.Setup(repo => repo.GetLinkByOriginalUrlAsync(originalUrl))
                .ReturnsAsync(existingLink);

            var request = new CreateShortenedLinkRequest { OriginalUrl = originalUrl };

            // Act
            var result = await _linkService.CreateShortenedLinkAsync(request);

            // Assert
            Assert.Equal(existingLink, result);
        }

        [Fact]
        public async Task CreateShortenedLinkAsync_OriginalUrlDoesNotExist_CreatesNewShortenedLink()
        {
            // Arrange
            var originalUrl = "http://example.com";
            _linkRepositoryMock.Setup(repo => repo.GetLinkByOriginalUrlAsync(originalUrl))
                .ReturnsAsync((Link)null);
            _linkRepositoryMock.Setup(repo => repo.GetLinkByShortenedUrlAsync(It.IsAny<string>()))
                .ReturnsAsync((Link)null);
            _linkRepositoryMock.Setup(repo => repo.AddLinkAsync(It.IsAny<Link>()))
                .Returns(Task.CompletedTask);

            var request = new CreateShortenedLinkRequest { OriginalUrl = originalUrl };

            // Act
            var result = await _linkService.CreateShortenedLinkAsync(request);

            // Assert
            Assert.Equal(originalUrl, result.OriginalUrl);
            Assert.NotNull(result.ShortenedUrl);
            Assert.Equal(DateTime.UtcNow.Date, result.CreatedAt.Date);
            Assert.Equal(DateTime.UtcNow.AddYears(1).Date, result.ExpiresAt.Date);
        }

        [Fact]
        public async Task GetOriginalUrlAsync_ShortenedUrlExists_ReturnsOriginalUrl()
        {
            // Arrange
            var shortenedUrl = "abc123";
            var link = new Link { OriginalUrl = "http://example.com", ShortenedUrl = shortenedUrl };
            _linkRepositoryMock.Setup(repo => repo.GetLinkByShortenedUrlAsync(shortenedUrl))
                .ReturnsAsync(link);

            // Act
            var result = await _linkService.GetOriginalUrlAsync(shortenedUrl);

            // Assert
            Assert.Equal(link.OriginalUrl, result);
        }

        [Fact]
        public async Task GetOriginalUrlAsync_ShortenedUrlDoesNotExist_ReturnsNull()
        {
            // Arrange
            var shortenedUrl = "abc123";
            _linkRepositoryMock.Setup(repo => repo.GetLinkByShortenedUrlAsync(shortenedUrl))
                .ReturnsAsync((Link)null);

            // Act
            var result = await _linkService.GetOriginalUrlAsync(shortenedUrl);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllUrls_LinksExist_ReturnsShortenedUrls()
        {
            // Arrange
            var links = new List<Link>
            {
                new Link { ShortenedUrl = "abc123" },
                new Link { ShortenedUrl = "def456" }
            };
            _linkRepositoryMock.Setup(repo => repo.GetAllLinks())
                .ReturnsAsync(links);

            // Act
            var result = await _linkService.GetAllUrls();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains("abc123", result);
            Assert.Contains("def456", result);
        }

        [Fact]
        public async Task GetAllUrls_NoLinksExist_ReturnsEmptyList()
        {
            // Arrange
            _linkRepositoryMock.Setup(repo => repo.GetAllLinks())
                .ReturnsAsync(new List<Link>());

            // Act
            var result = await _linkService.GetAllUrls();

            // Assert
            Assert.Empty(result);
        }
    }
}