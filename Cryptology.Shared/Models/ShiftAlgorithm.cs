namespace Cryptology.Shared.Models
{
    public class ShiftAlgorithm : CaesarCipher
    {
        public override string Name => "Shift Alogrithm";

        public ShiftAlgorithm(string text, uint key) : base(text)
        {
            _key = key;
        }
    }
}