using Microsoft.AspNetCore.DataProtection;
using System;

namespace EmployeeSystemMangement.Common
{
    public class ConnectionStringProtector
    {
        private readonly IDataProtector _protector;

        public ConnectionStringProtector(IDataProtectionProvider dataProtectionProvider)
        {
            _protector = dataProtectionProvider.CreateProtector("ConnectionStringProtector");
        }

        public string Encrypt(string plainText)
        {
            return _protector.Protect(plainText);
        }

        public string Decrypt(string cipherText)
        {
            return _protector.Unprotect(cipherText);
        }
    }
}
