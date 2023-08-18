using System.Security.Cryptography;
using System.Text;

namespace TestTaskForInforce.Services
{
    public class ShortUrlService
    {
        const string EnglishAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string CreateShortUrlPath(string url)
        {
            var asByteArray = Encoding.Default.GetBytes(url);
            var hashedPasswrod = SHA256.Create().ComputeHash(asByteArray);
            StringBuilder convertedUrl = new StringBuilder(Convert.ToBase64String(hashedPasswrod));          

            var rand = new Random();

            if (convertedUrl.ToString().Contains('/'))
            {
                convertedUrl.Replace('/', EnglishAlphabet[rand.Next(EnglishAlphabet.Length)]);
            }

            var shortPath = new string(Enumerable.Repeat(convertedUrl, 10).Select(s => s[rand.Next(s.Length)]).ToArray());

            return shortPath;
        }
    }
}
