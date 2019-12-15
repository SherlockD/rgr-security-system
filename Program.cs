using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RGR_securitySystem.Crypt.AES;
using RGR_securitySystem.Crypt.HammingCode;

namespace RGR_securitySystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /*AES aes = new AES();

            byte[] result = new byte[128];
            byte[] resultDecrypt = new byte[128];
            
            aes.Encrypt("World", ref result);
            aes.Decrypt(result, ref resultDecrypt);
            
            Console.WriteLine(Encoding.UTF8.GetString(result));
            Console.WriteLine(Encoding.UTF8.GetString(resultDecrypt));*/
            
            HammingCode hammingCode = new HammingCode();

            byte[] input = {1, 0, 0, 1, 0, 0, 1, 0, 1, 1, 1, 0, 0, 0, 1};
            
            var result = hammingCode.Encrypt(input);

            var decrypt = hammingCode.Decrypt(result).ToList();

            Console.WriteLine();
            decrypt.ForEach(node => Console.Write(node));
        }
    }
}