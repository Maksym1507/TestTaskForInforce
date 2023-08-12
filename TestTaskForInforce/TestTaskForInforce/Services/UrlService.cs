using TestTaskForInforce.Data.Entities;
using TestTaskForInforce.Repositories.Abstractions;
using TestTaskForInforce.Services.Abstractions;

namespace TestTaskForInforce.Services
{
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository _urlRepository;
        private readonly ILogger<UrlService> _loggerService;

        public UrlService(
            IUrlRepository urlRepository,
            ILogger<UrlService> loggerService)
        {
            _urlRepository = urlRepository;
            _loggerService = loggerService;
        }

        public async Task<int> CreateShortenedUrlAsync(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var inputUrl))
            {
                _loggerService.LogError("Invalid format of url");
                throw new Exception("Invalid format of url");
            }

            var urlMatch = await _urlRepository.GetUrlByBaseUrlAsync(url);

            if (urlMatch != null)
            {
                _loggerService.LogError($"Shortened url with baseUrl = {url} has already existed");
                throw new Exception($"Shortened url with baseUrl = {url} has already existed");
            }

            var shortenedUrl = ShortUrlService.CreateShortUrlPath(url);

            var urls = await _urlRepository.CreateShortenedUrlAsync(url, shortenedUrl);

            return urls;
        }

        public async Task<UrlEntity?> GetByShortenUrlAsync(string shortenUrl)
        {
            var result = await _urlRepository.GetByShortenUrlAsync(shortenUrl);

            if (result == null)
            {
                _loggerService.LogWarning($"Not founded url with shorten url = {shortenUrl}");
                throw new Exception($"Not founded url with shorten url = {shortenUrl}");
            }

            return result;
        }

        public async Task<IEnumerable<UrlEntity>?> GetUrlsAsync()
        {
            var result = await _urlRepository.GetUrlsAsync();

            if (!result.Any())
            {
                _loggerService.LogWarning($"Not founded urls");
                return null;
            }

            return result;
        }
    }
}
