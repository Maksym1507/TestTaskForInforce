using System.Security.Cryptography;
using System.Text;

namespace TestTaskForInforce.Services
{
    public class ShortUrlService
    {
        public static string CreateShortUrlPath(string url)
        {
            var asByteArray = Encoding.Default.GetBytes(url);
            var hashedPasswrod = SHA256.Create().ComputeHash(asByteArray);
            var convertedUrl = Convert.ToBase64String(hashedPasswrod);

            var rand = new Random();
            
            var shortPath = new string(Enumerable.Repeat(convertedUrl, 10).Select(s => s[rand.Next(s.Length)]).ToArray());

            return shortPath;
        }
    }
}
