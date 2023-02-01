using Microsoft.AspNetCore.DataProtection;
using Net7WebApiTemplate.Application.Shared.Interface;

namespace Net7WebApiTemplate.Infrastructure.DataProtection
{
    public class EncryptionManager : IEncryptionManager
    {
        private readonly IDataProtector _dataProtector;

        public EncryptionManager(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtector = dataProtectionProvider.CreateProtector("secrettext");
        }

        public string Decrypt(string cipherText)
        {
            return _dataProtector.Unprotect(cipherText);
        }

        public string Encrypt(string plainText)
        {
            return _dataProtector.Protect(plainText);
        }
    }
}