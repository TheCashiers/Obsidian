using System.IO;
using System.Security.Cryptography;

namespace Obsidian.Application.Cryptography
{
    public class RsaSigningService
    {
        private static readonly RSACryptoServiceProvider _provider;

        static RsaSigningService()
        {
            const string fileName = "key.bin";
            if (!File.Exists(fileName))
            {
                using (var fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write))
                {
                    using (var writer = new BinaryWriter(fs))
                    {
                        var rsa = GenerateNew();
                        var data = rsa.ExportCspBlob(true);
                        writer.Write(data);
                        _provider = rsa;
                    }
                }
            }
            else
            {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (var ms = new MemoryStream())
                    {
                        fs.CopyTo(ms);
                        var data = ms.ToArray();
                        var rsa = GenerateNew();
                        rsa.ImportCspBlob(data);
                        _provider = rsa;
                    }
                }
            }
        }

        private static RSACryptoServiceProvider GenerateNew()
        {
            return new RSACryptoServiceProvider(2048);
        }

        public RSA GetPublicKey()
        {
            var publicParameter = _provider.ExportParameters(false);
            var result = new RSACryptoServiceProvider();
            result.ImportParameters(publicParameter);
            return result;
        }

        public RSA GetPublicAndPrivateKey()
        {
            var privateAndPublicParameter = _provider.ExportParameters(true);
            var result = new RSACryptoServiceProvider();
            result.ImportParameters(privateAndPublicParameter);
            return result;
        }
    }
}