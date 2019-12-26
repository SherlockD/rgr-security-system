using System;
using System.Collections;
using RGR_securitySystem.Crypt.AES;
using RGR_securitySystem.Crypt.HammingCode;
using RGR_securitySystem.Crypt.LinearEncoding;

namespace RGR_securitySystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            AES aes = new AES();
            HammingCode hammingCode = new HammingCode();
            LinearEncoding linearEncoding = new LinearEncoding();

            Console.WriteLine("Please input string to send");
            string input = Console.ReadLine();

            byte[] aesResult = new byte[128];
            byte[] decryptResult = new byte[128];

            aes.Encrypt(input, ref aesResult);
            BitArray bitArray = new BitArray(aesResult);

            byte[] aesResultBit = bitArray.ConvertToBitByteArray();
            Console.WriteLine();
            Console.WriteLine("AES encode output");
            aesResult.Print();

            var hammingEncrypt = hammingCode.Encrypt(aesResultBit);
            Console.WriteLine();
            Console.WriteLine("Hamming encode output");
            hammingEncrypt.Print();

            var linearEncrypt = linearEncoding.Encoding(hammingEncrypt);
            Console.WriteLine();
            Console.WriteLine("Linear encode output");
            linearEncrypt.Print();

            linearEncoding.PrintGraphic(linearEncrypt);

            var linearDecrypt = linearEncoding.Decoding(linearEncrypt);
            Console.WriteLine();
            Console.WriteLine("Linear decode output");
            linearDecrypt.Print();

            var hammingDecrypt = hammingCode.Decrypt(linearDecrypt);
            Console.WriteLine();
            Console.WriteLine("Hamming decode output");
            hammingDecrypt.Print();

            aes.Decrypt(aesResult,ref decryptResult);
            Console.WriteLine();
            Console.WriteLine("AES Decrypt");
            decryptResult.Print();

            Console.WriteLine("\n\n\n");
            Console.WriteLine("Decrypting full result:");
            Console.WriteLine(input);
            /*AES aes = new AES();

            byte[] result = new byte[128];
            byte[] resultDecrypt = new byte[128];
            
            aes.Encrypt("World", ref result);
            aes.Decrypt(result, ref resultDecrypt);
            
            Console.WriteLine(Encoding.UTF8.GetString(result));
            Console.WriteLine(Encoding.UTF8.GetString(resultDecrypt));

            HammingCode hammingCode = new HammingCode();

            byte[] input = {1, 0, 0, 1, 0, 0, 1, 0, 1, 1, 1, 0, 0, 0, 1};
            
            var result = hammingCode.Encrypt(input);

            var decrypt = hammingCode.Decrypt(result).ToList();

            Console.WriteLine();
            decrypt.ForEach(node => Console.Write(node));

            LinearEncoding linearEncoding = new LinearEncoding();

            var result = linearEncoding.Encoding(input);

            result.ToList().ForEach(node => Console.Write(node+ " "));

            var decode = linearEncoding.Decoding(result).ToList();

            Console.WriteLine();

            decode.ForEach(node => Console.Write(node));*/
        }
    }
}