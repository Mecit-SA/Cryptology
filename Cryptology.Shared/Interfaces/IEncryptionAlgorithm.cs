namespace Cryptology.Shared.Interfaces
{
    public interface IEncryptionAlgorithm : IAlgorithm
    {
        string Encrypt();
        string Decrypt();
    }
}