using TestTaskForInforce.Data.Entities;

namespace TestTaskForInforce.Repositories.Abstractions
{
    public interface IUrlRepository
    {
        Task<int> CreateShortenedUrlAsync(string url, string shortenedUrl, UserEntity user);

        Task<UrlEntity?> GetUrlByBaseUrlAsync(string baseUrl);

        Task<UrlEntity?> GetByShortenUrlAsync(string shortenUrl);

        Task<IEnumerable<UrlEntity>?> GetUrlsAsync();
    }
}
