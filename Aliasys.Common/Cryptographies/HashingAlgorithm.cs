using System.Security.Cryptography;

namespace Aliasys.Common.Cryptographies
{
    public class HashingAlgorithm
    {
        public static string HashString(string text, string salt = "")
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            using (SHA256 sha = SHA256.Create())
            {
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);
                return BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }
        }
    }
}
