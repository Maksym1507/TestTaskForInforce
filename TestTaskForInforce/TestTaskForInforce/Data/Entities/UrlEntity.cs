namespace TestTaskForInforce.Data.Entities
{
    public class UrlEntity
    {
        public int Id { get; set; }

        public string BaseUrl { get; set; } = null!;

        public string ShortenedUrl { get; set; } = null!;

        public UserEntity User { get; set; }

        public string UserId { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
