namespace Net7WebApiTemplate.Application.Shared.Interface
{
    public interface IEncryptionManager
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}