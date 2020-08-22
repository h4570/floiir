using System.Security.Cryptography;
using System.Text;

namespace WebApi
{
    public static class Utilities
    {

        public static string ComputeSha256Hash(string value, string salt = "")
        {
            using SHA256 sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes($"{value}{salt}"));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
                builder.Append(bytes[i].ToString("x2"));
            return builder.ToString();
        }

    }
}
