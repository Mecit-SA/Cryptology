using Cryptology.Shared.Interfaces;
using Cryptology.Shared.Models;
using System;

namespace Cryptology.ConsoleApp
{
    class Program
    {
        /*
         * By : Mecit SARIGÜZEL
         * mecitsariguzel@gmail.com
         */
        static void Main(string[] args)
        {
            /*
             *  Uncomment one line which contains the algorithm you need to use
             */

            string input = "merhaba";

            IEncryptionAlgorithm algorithm;

             algorithm = new CaesarCipher(input);
            // algorithm = new ShiftAlgorithm(input, 5);
            // algorithm = new DisplacementAlgorithm(input);
            // algorithm = new PermutationAlgorithm(input, 5, new[] { 4, 1, 5, 2, 3 });

            /*
            input = "bilgisayarmuhendisligi";
            algorithm = new RouteAlgorithm(input, 5);
            */

            /*
            input = "bilgisayarmuhendisligi";
            algorithm = new ZigzagAlgorithm(input, 5);
            */

            /*
            input = "afyonkarahisar";
            algorithm = new VigenereCipher(input, "araba");
            */

            Console.WriteLine($"\n\t\t=> Algorithm Name : {algorithm.Name} <=");
            Console.WriteLine($"\n\t\t[ Input : {input} ]");
            Console.WriteLine($"\n\t\t-> Encryption result : {algorithm.Encrypt()}");
            Console.WriteLine($"\t\t-> Decryption result : {algorithm.Decrypt()}\n");
        }
    }
}