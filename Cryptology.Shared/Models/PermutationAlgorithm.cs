using Cryptology.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cryptology.Shared.Models
{
    public class PermutationAlgorithm : IEncryptionAlgorithm
    {
        public string Name => "Permutation Algorithm";

        string _text;
        const char randomChar = '?';
        readonly int _key;
        readonly IEnumerable<int> _order;

        public PermutationAlgorithm(string text, int key, int[] order)
        {
            if(key > order.Length)
            {
                throw new Exception("array elements count must be equal to key");
            }

            _text = text;
            _key = key;
            _order = order.Take(_key);
        }

        public string Encrypt()
        {
            // Fill with random character to be able to split into blocks
            while(_text.Length % _key != 0)
            {
                _text += randomChar;
            }

            // Split
            IEnumerable<Block> blocks = Block.Split(_text, _key);

            string output = string.Empty;

            // Encrypt & merge
            foreach (Block block in blocks)
            {
                output += block.Encrypt(_order);
            }

            _text = output;
            return _text;
        }

        public string Decrypt()
        {
            // Split
            IEnumerable<Block> blocks = Block.Split(_text, _key);

            string output = string.Empty;

            // Decrypt & merge
            foreach (Block block in blocks)
            {
                output += block.Decrypt(_order);
            }

            // Remove random character
            output = output.TrimEnd(randomChar);

            _text = output;
            return _text;
        }


        public class Block
        {
            readonly string _content;

            public Block(params char[] characters)
            {
                _content = new string(characters);
            }

            public string Encrypt(IEnumerable<int> order)
            {
                string output = string.Empty;

                foreach (var no in order)
                {
                    output += _content[no - 1];
                }

                return output;
            }

            public string Decrypt(IEnumerable<int> order)
            {
                char[] output = new char[order.Count()];

                int i = 0;

                foreach (var no in order)
                {
                    output[no - 1] = _content[i++];
                }

                return new string(output);
            }

            public static IEnumerable<Block> Split(string content, int key)
            {
                ICollection<Block> output = new List<Block>();

                for (int i = 0; i < content.Length - 1; i += key)
                {
                    char[] arr = new char[key];

                    for(int j = 0; j < key; j++)
                    {
                        arr[j] = content[i + j];
                    }

                    output.Add(new Block(arr));
                }

                return output;
            }
        }
    }
}