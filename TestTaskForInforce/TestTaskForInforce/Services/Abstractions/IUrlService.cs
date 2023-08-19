using TestTaskForInforce.Data.Entities;
using TestTaskForInforce.Models.Responses;

namespace TestTaskForInforce.Services.Abstractions
{
    public interface IUrlService
    {
        Task<int> CreateShortenedUrlAsync(string url, string email);

        Task<UrlResponse?> GetShortUrlByIdAsync(int id);

        Task<UrlEntity?> GetByShortenUrlAsync(string shortenUrl);

        Task<IEnumerable<UrlResponse>?> GetUrlsAsync();

        Task<bool> DeleteAsync(int id);
    }
}
