using Cryptology.Shared.Interfaces;

namespace Cryptology.Shared.Models
{
    public class CaesarCipher : IEncryptionAlgorithm
    {
        public virtual string Name => "Caesar Cipher";
        protected uint _key = 3;
        string _text;
        string _output;

        public CaesarCipher(string text)
        {
            _text = text;
        }

        public string Encrypt()
        {
            _output = string.Empty;

            foreach (var character in _text)
            {
                _output += (char)((character + _key) % 127);
            }

            _text = _output;
            return _text;
        }
        public string Decrypt()
        {
            _output = string.Empty;

            foreach (var character in _text)
            {
                _output += (char)((character - _key) % 127);
            }

            _text = _output;
            return _text;
        }
    }
}