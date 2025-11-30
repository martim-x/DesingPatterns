using System.Security.Cryptography;
using System.Text;

namespace Lab5Lib
{
    public class RSASignDecorator : Decorator
    {
        private readonly RSA rsa;
        private readonly HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA256;

        public RSASignDecorator(IWriter writer)
            : base(writer)
        {
            this.rsa = RSA.Create(2048);
        }

        // message : string = {message}{token}{RSAhash}{token}{publicKey}
        public override string? Save(string? message)
        {
            if (message == null)
                return null;

            var parts = message.Split(Constant.Token);
            if (parts.Length < 2) // uncorrect format
                return null;

            string text = parts[0];
            string hash = parts[1];

            // универсальная подпись
            byte[] signature = rsa.SignData(
                (byte[])Encoding.UTF8.GetBytes(hash),
                this.hashAlgorithmName, // MD5, SHA256, SHA512, …
                RSASignaturePadding.Pkcs1
            );

            string signatureBase64 = Convert.ToBase64String(signature);
            string publicKeyBase64 = Convert.ToBase64String(this.rsa.ExportRSAPublicKey());

            return base.Save(
                $"{text}{Constant.Token}{signatureBase64}{Constant.Token}{publicKeyBase64}"
            );
        }
    }
}
