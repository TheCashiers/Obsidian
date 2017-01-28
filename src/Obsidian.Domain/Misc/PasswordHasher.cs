using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Obsidian.Domain.Misc
{
    internal class PasswordHasher
    {
        internal string HashPasword(string password)
        {
            using (var hash = SHA256.Create())
            {
                var buffer = Encoding.UTF8.GetBytes(password);
                hash.Initialize();
                var result = hash.ComputeHash(buffer);
                for (int i = 0; i < 5; i++)
                {
                    result = hash.ComputeHash(result);
                }
                return ByteToString(result);
            }
        }

        private string ByteToString(byte[] buffer) => string.Join("", buffer.Select(b => $"{b:X2}"));
    }
}