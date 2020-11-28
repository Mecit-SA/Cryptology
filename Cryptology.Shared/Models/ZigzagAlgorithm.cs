using Cryptology.Shared.Interfaces;
using System.Linq;

namespace Cryptology.Shared.Models
{
    public class ZigzagAlgorithm : IEncryptionAlgorithm
    {
        public string Name => "Zigzag Algorithm";

        readonly int _key;
        int rowCount;
        int columnCount;
        
        string _text;
        char[,] _matrix;

        string _output;

        public ZigzagAlgorithm(string text, int key)
        {
            _text = text;
            _key = key;
            rowCount = _key;
            columnCount = _text.Count();
            _matrix = new char[rowCount, columnCount];
            FillMatrix();
        }

        public string Encrypt()
        {
            _output = string.Empty;

            for (int i = 0; i < rowCount; i++)
            {
                for(int j=0;j< columnCount; j++)
                {
                    char value = _matrix[i, j];

                    if(value == '\0')
                    {
                        continue;
                    }

                    _output += value;
                }
            }

            return _output;
        }

        public string Decrypt()
        {
            _output = string.Empty;

            int index = 0;
            int i = 0, j = 0;

            while (index < _text.Length - 1)
            {
                for (i = 0, j = index; i < rowCount - 1; i++, j++)
                {
                    _output += _matrix[i, j];

                    if (index == _text.Length - 1)
                    {
                        break;
                    }

                    index++;
                }

                for (i = rowCount - 1, j = index; i > 0; i--, j++)
                {
                    _output += _matrix[i, j];

                    if (index == _text.Length - 1)
                    {
                        break;
                    }

                    index++;
                }
            }

            return _output;
        }

        void FillMatrix()
        {
            int index = 0;
            int i = 0, j = 0;

            while(index < _text.Length - 1)
            {
                for (i = 0, j = index; i < rowCount - 1; i++, j++)
                {
                    _matrix[i, j] = _text[index];

                    if (index == _text.Length - 1)
                    {
                        break;
                    }

                    index++;
                }

                for (i = rowCount - 1, j = index; i > 0; i--, j++)
                {
                    _matrix[i, j] = _text[index];

                    if (index == _text.Length - 1)
                    {
                        break;
                    }

                    index++;
                }
            }
        }

    }
}