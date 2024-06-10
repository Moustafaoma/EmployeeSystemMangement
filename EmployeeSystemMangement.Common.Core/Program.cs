using EmployeeSystemMangement.Common.Core.Encryption;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmployeeSystemMangement.Common.Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDataProtection();
            var services = serviceCollection.BuildServiceProvider();

            var protector = new ConnectionStringProtector(services.GetService<IDataProtectionProvider>());

            string connectionString = "Server=.;Database=EmployeeSystemMangementDB;Trusted_Connection=True;TrustServerCertificate=True;";
            string encryptedConnectionString = protector.Encrypt(connectionString);

            Console.WriteLine($"Encrypted Connection String: {encryptedConnectionString}");
        }
    }
}
