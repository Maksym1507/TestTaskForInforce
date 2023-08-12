using TestTaskForInforce.Data.Entities;

namespace TestTaskForInforce.Services.Abstractions
{
    public interface IUrlService
    {
        Task<int> CreateShortenedUrlAsync(string url);

        Task<UrlEntity?> GetByShortenUrlAsync(string shortenUrl);

        Task<IEnumerable<UrlEntity>?> GetUrlsAsync();
    }
}
