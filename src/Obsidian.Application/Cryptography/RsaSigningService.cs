using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Obsidian.Application.Cryptography
{
    public class RsaSigningService
    {
        private readonly RSA _privateKey;

        private readonly RSA _publicKey;

        public RsaSigningService()
        {
            const string fileName = "key.pfx";
            var cert = new X509Certificate2(fileName, "Obsidian.Cert.Pwd");
            _privateKey = cert.GetRSAPrivateKey();
            _publicKey = cert.GetRSAPublicKey();
        }

        public RSA GetPublicKey()
        {
            return _publicKey;
        }

        public RSA GetPrivateKey()
        {
            return _privateKey;
        }
    }
}