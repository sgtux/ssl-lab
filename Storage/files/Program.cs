using System.Security.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Storage.Helpers;

namespace Storage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.ConfigureHttpsDefaults(configureOptions =>
                        {
                            configureOptions.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
                            configureOptions.ServerCertificate = CertificateHelper.GetCertificate();
                        });
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}