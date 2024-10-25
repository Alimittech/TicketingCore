using System.Security.Cryptography;
using System.Text;

namespace Aliasys.Common.Cryptographies
{
    public class EncodingAlgorithm
    {
        public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes, byte[] saltBytes, int keysize)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = keysize;
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 2000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    using (CryptoStream cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    return ms.ToArray();
                }
            }
        }

        public static string EncryptText(string text, string password, string salt)
        {
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(text);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes, saltBytes, 256);
            return Convert.ToBase64String(bytesEncrypted);
        }

        public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes, byte[] saltBytes, int keysize)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = keysize;
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 2000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    using (CryptoStream cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    return ms.ToArray();
                }
            }
        }

        public static string DecryptText(string decryptedText, string password, string salt)
        {
            byte[] bytesToBeDecrypted = Convert.FromBase64String(decryptedText);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes, saltBytes, 256);
            return Encoding.UTF8.GetString(bytesDecrypted);
        }

        public static string EncryptTextWithParameter(string Parameter)
        {
            string passKey = "Ali@sys#100200";
            return EncodingAlgorithm.EncryptText(Parameter, passKey, "{1,2,3,4,5,6,7,8}");
        }

        public static string DecryptTextWithParameter(string Parameter)
        {
            string passKey = "Ali@sys#100200";
            return EncodingAlgorithm.DecryptText(Parameter, passKey, "{1,2,3,4,5,6,7,8}");
        }
    }
}
