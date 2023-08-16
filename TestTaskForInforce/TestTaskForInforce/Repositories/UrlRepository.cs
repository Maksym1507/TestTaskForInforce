using Microsoft.EntityFrameworkCore;
using TestTaskForInforce.Data;
using TestTaskForInforce.Data.Entities;
using TestTaskForInforce.Repositories.Abstractions;

namespace TestTaskForInforce.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UrlRepository(
            ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateShortenedUrlAsync(string url, string shortenedUrl, UserEntity user)
        {
            var urlToCreate = new UrlEntity
            {
                BaseUrl = url,
                ShortenedUrl = shortenedUrl,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
            };

            var item = await _dbContext.AddAsync(urlToCreate);

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<UrlEntity?> GetUrlByBaseUrlAsync(string baseUrl)
        {
            return await _dbContext.Urls.FirstOrDefaultAsync(f => f.BaseUrl == baseUrl);
        }

        public async Task<UrlEntity?> GetByShortenUrlAsync(string shortenUrl)
        {
            return await _dbContext.Urls.FirstOrDefaultAsync(f => f.ShortenedUrl == shortenUrl);
        }

        public async Task<IEnumerable<UrlEntity>?> GetUrlsAsync()
        {
            return await _dbContext.Urls.Include(i=>i.User).ToListAsync();
        }
    }
}
