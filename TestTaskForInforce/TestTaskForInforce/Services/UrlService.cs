using AutoMapper;
using TestTaskForInforce.Data.Entities;
using TestTaskForInforce.Models.Responses;
using TestTaskForInforce.Repositories.Abstractions;
using TestTaskForInforce.Services.Abstractions;

namespace TestTaskForInforce.Services
{
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UrlService> _loggerService;
        private readonly IMapper _mapper;

        public UrlService(
            IUrlRepository urlRepository,
            IUserRepository userRepository,
            ILogger<UrlService> loggerService, IMapper mapper)
        {
            _urlRepository = urlRepository;
            _userRepository = userRepository;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<int> CreateShortenedUrlAsync(string url, string email)
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

            var user = await _userRepository.GetByEmailAsync(email);

            return await _urlRepository.CreateShortenedUrlAsync(url, shortenedUrl, user!); ;
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

        public async Task<IEnumerable<UrlResponse>?> GetUrlsAsync()
        {
            var result = await _urlRepository.GetUrlsAsync();

            if (result!.Count() == 0)
            {
                _loggerService.LogWarning($"Not founded urls");
                return new List<UrlResponse>();
            }

            var a = result!.Select(s => _mapper.Map<UrlResponse>(s));

            return result!.Select(s => _mapper.Map<UrlResponse>(s));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var urlToDelete = await _urlRepository.GetUrlByIdAsync(id);

            if (urlToDelete == null)
            {
                _loggerService.LogWarning($"Url with {id} hasn't founded");
                return false;
            }

            var isDeleted = await _urlRepository.DeleteAsync(urlToDelete);
            _loggerService.LogInformation($"Removed url with id = {id}");
            return isDeleted;
        }
    }
}
