using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Storage.Helpers
{
    public class CertificateHelper
    {
        public static X509Certificate2 GetCertificate()
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var publicCertPath = Path.Join(assemblyPath, "cert.pem");
            var privateCertPath = Path.Join(assemblyPath, "key.pem");
            var exists = File.Exists(privateCertPath);
            X509Certificate2 sslCert = CreateFromPublicPrivateKey(publicCertPath, privateCertPath);
            return new X509Certificate2(sslCert.Export(X509ContentType.Pkcs12));
        }

        private static X509Certificate2 CreateFromPublicPrivateKey(string publicCertPath, string privateCertPath)
        {
            byte[] publicPemBytes = File.ReadAllBytes(publicCertPath);
            using var publicX509 = new X509Certificate2(publicPemBytes);
            var privateKeyText = File.ReadAllText(privateCertPath);
            var privateKeyBlocks = privateKeyText.Split("-", System.StringSplitOptions.RemoveEmptyEntries);
            var privateKeyBytes = Convert.FromBase64String(privateKeyBlocks[1]);

            using RSA rsa = RSA.Create();
            if (privateKeyBlocks[0] == "BEGIN PRIVATE KEY")
            {
                rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);
            }
            else if (privateKeyBlocks[0] == "BEGIN RSA PRIVATE KEY")
            {
                rsa.ImportRSAPrivateKey(privateKeyBytes, out _);
            }
            X509Certificate2 keyPair = publicX509.CopyWithPrivateKey(rsa);
            return keyPair;
        }
    }
}