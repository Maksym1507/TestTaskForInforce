namespace TestTaskForInforce.Models.Responses
{
    public class UrlResponse
    {
        public int Id { get; set; }

        public string BaseUrl { get; set; } = null!;

        public UserResponse User { get; set; } = null!;

        public string ShortenedUrl { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
