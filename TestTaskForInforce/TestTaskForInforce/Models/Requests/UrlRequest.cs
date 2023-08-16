using System.ComponentModel.DataAnnotations;

namespace TestTaskForInforce.Models.Requests
{
    public class UrlRequest
    {
        [Required]
        public string Url { get; set; } = null!;
    }
}
