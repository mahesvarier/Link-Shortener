using Xunit;
using Moq;
using LinkShortener.Service;
using LinkShortener.Models;

public class LinkServiceTests
{
    private readonly Mock<ILinkRepository> _linkRepositoryMock;
    private readonly ILinkService _linkService;

    public LinkServiceTests()
    {
        _linkRepositoryMock = new Mock<ILinkRepository>();
        _linkService = new LinkService(_linkRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateShortenedLinkAsync_ShouldReturnShortenedLink()
    {
        // Arrange
        var originalUrl = "https://example.com";

        // Act
        var result = await _linkService.CreateShortenedLinkAsync(originalUrl);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(originalUrl, result.OriginalUrl);
        Assert.NotEmpty(result.ShortenedUrl);
    }

    [Fact]
    public async Task GetOriginalUrlAsync_ShouldReturnOriginalUrl()
    {
        // Arrange
        var shortenedUrl = "short123";
        var originalUrl = "https://example.com";
        _linkRepositoryMock.Setup(repo => repo.GetLinkByShortenedUrlAsync(shortenedUrl))
            .ReturnsAsync(new Link { OriginalUrl = originalUrl });

        // Act
        var result = await _linkService.GetOriginalUrlAsync(shortenedUrl);

        // Assert
        Assert.Equal(originalUrl, result);
    }

    [Fact]
    public async Task GetOriginalUrlAsync_ShouldReturnNull_WhenShortenedUrlNotFound()
    {
        // Arrange
        var shortenedUrl = "short123";
        _linkRepositoryMock.Setup(repo => repo.GetLinkByShortenedUrlAsync(shortenedUrl))
            .ReturnsAsync((Link)null);

        // Act
        var result = await _linkService.GetOriginalUrlAsync(shortenedUrl);

        // Assert
        Assert.Null(result);
    }
}