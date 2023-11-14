namespace UrlShortener_2_.Helpers
{
    public class CreateShortUrl
    {
        public string GenerateShortKey()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] shortKey = new char[6];
            var random = new Random();

            for (int i = 0; i < 6; i++)
            {
                shortKey[i] = chars[random.Next(chars.Length)];
            }

            return new string(shortKey);
        }
    }
}
