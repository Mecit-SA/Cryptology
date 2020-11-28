using Cryptology.Shared.Interfaces;
using System;

namespace Cryptology.Shared.Models
{
    public class RouteAlgorithm : IEncryptionAlgorithm
    {
        public string Name => "Route Algorithm";

        const char _randomChar = '?';

        readonly int _key;
        int _rowCount;
        int _columnCount;

        string _text;
        char[,] _matrix;

        string _output;

        public RouteAlgorithm(string text, int key)
        {
            _key = key;
            _text = text;
            _columnCount = _key;
            _rowCount = (int)Math.Ceiling((decimal)_text.Length / _key);
            _matrix = new char[_rowCount, _columnCount];
            FillToMatrix(_text);
        }

        public string Encrypt()
        {
            _output = string.Empty;

            int layer = 0;

            while(_output.Length < _rowCount * _columnCount)
            {
                for (int i = _rowCount - layer - 1; i > layer; i--)
                {
                    _output += _matrix[i, layer];
                }

                for (int i = layer; i < _columnCount - layer - 1; i++)
                {
                    _output += _matrix[layer, i];
                }

                for (int i = layer; i < _rowCount - layer - 1; i++)
                {
                    _output += _matrix[i, _rowCount - layer - 1];
                }

                for (int i = _columnCount - layer - 1; i > layer; i--)
                {
                    _output += _matrix[_rowCount - layer - 1, i];
                }

                layer++;

                if (_output.Length + 1 == _rowCount * _columnCount)
                {
                    _output += _matrix[layer, layer];
                    break;
                }
            }

            return _output;
        }

        public string Decrypt()
        {
            _output = string.Empty;

            for(int i = 0; i < _rowCount; i++)
            {
                for(int j = 0; j < _columnCount; j++)
                {
                    _output += _matrix[i, j];
                }
            }

            return _output.TrimEnd(_randomChar);
        }

        void FillToMatrix(string text)
        {
            // Fill with random character to match matrix size
            while(text.Length < _rowCount * _columnCount)
            {
                text += _randomChar;
            }

            int index = 0;

            for(int i = 0; i < _rowCount; i++)
            {
                for(int j = 0; j < _columnCount; j++)
                {
                    _matrix[i, j] = text[index++];
                }
            }
        }
    }
}