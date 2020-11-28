using Cryptology.Shared.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Cryptology.Shared.Models
{
    public class DisplacementAlgorithm : IEncryptionAlgorithm
    {
        public string Name => "Displacement Algorithm";
        Dictionary<char, char> _dictionary;
        string _text;
        string _output;

        public DisplacementAlgorithm(string text)
        {
            _text = text;
            FillDictionary();
        }


        public string Encrypt()
        {
            _output = string.Empty;

            foreach (var character in _text)
            {
                _output += _dictionary.GetValueOrDefault(character);
            }

            _text = _output.Replace('\0', '?');
            return _text;
        }

        public string Decrypt()
        {
            _output = string.Empty;

            foreach (var character in _text)
            {
                _output += _dictionary.FirstOrDefault(d => d.Value.Equals(character)).Key;
            }

            _text = _output.Replace('\0', '?');
            return _text;
        }

        void FillDictionary()
        {
            _dictionary = new Dictionary<char, char>();
            _dictionary.Add('a', 'b');
            _dictionary.Add('b', 'c');
            _dictionary.Add('e', 'f');
            _dictionary.Add('h', 'i');
            _dictionary.Add('m', 'n');
            _dictionary.Add('r', 's');
        }
    }
}