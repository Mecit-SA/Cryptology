using Cryptology.Shared.Interfaces;
using System.Collections.Generic;

namespace Cryptology.Shared.Models
{
    public class VigenereCipher : IEncryptionAlgorithm
    {
        public string Name => "Vigenere Cipher";

        const char randomChar = 'a';
        string _key;
        string _text;

        string _output;

        public VigenereCipher(string text, string key)
        {
            _key = key;
            _text = text;
        }

        public string Encrypt()
        {
            // Split
            IEnumerable<Block> blocks = Block.Split(_text, _key.Length);

            _output = string.Empty;

            // Encrypt and merge
            foreach (Block block in blocks)
            {
                _output += block.Encrypt(_key);
            }

            return _output;
        }

        public string Decrypt()
        {
            // Split
            IEnumerable<Block> blocks = Block.Split(_output, _key.Length);

            _output = string.Empty;

            // Decrypt and merge
            foreach (Block block in blocks)
            {
                _output += block.Decrypt(_key);
            }

            return _output.TrimEnd(randomChar);
        }


        class Block
        {
            string _content;

            public Block(char[] content)
            {
                _content = new string(content);
            }

            public string Encrypt(string key)
            {
                char[] contentArr = _content.ToCharArray();
                char[] keyArr = key.ToCharArray();

                for(int i = 0; i < contentArr.Length - 1; i++)
                {
                    contentArr[i] = (char)((contentArr[i] + keyArr[i]) % 127);
                }

                return new string(contentArr);
            }

            public string Decrypt(string key)
            {
                char[] contentArr = _content.ToCharArray();
                char[] keyArr = key.ToCharArray();

                for (int i = 0; i < contentArr.Length - 1; i++)
                {
                    if(contentArr[i] < keyArr[i])
                    {
                        contentArr[i] += (char)127;
                    }

                    contentArr[i] = (char)((contentArr[i] - keyArr[i]) % 127);
                }

                return new string(contentArr);
            }

            public static IEnumerable<Block> Split(string text, int blockLength)
            {
                while (text.Length % blockLength != 0)
                {
                    text += randomChar;
                }

                ICollection<Block> blocks = new List<Block>();

                for (int i = 0; i < text.Length - 1; i += blockLength)
                {
                    char[] arr = new char[blockLength];

                    for (int j = 0; j < blockLength; j++)
                    {
                        arr[j] = text[i + j];
                    }

                    blocks.Add(new Block(arr));
                }

                return blocks;
            }
        }
    }
}