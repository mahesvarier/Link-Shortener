using System.Security.Cryptography;

namespace LinkShortener.Utilities
{
    public static class StaticUrlShortener
    {
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private const int ShortUrlLength = 4;

        public static string GenerateShortenedUrl()
        {
            var random = new char[ShortUrlLength];
            var buffer = new byte[ShortUrlLength];
            RandomNumberGenerator.Fill(buffer);
            for (int i = 0; i < random.Length; i++)
            {
                random[i] = Characters[buffer[i] % Characters.Length];
            }
            return new string(random);
        }
    }
}
