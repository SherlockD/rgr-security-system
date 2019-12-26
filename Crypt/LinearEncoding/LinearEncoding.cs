using System;
using System.Collections.Generic;
using System.Linq;

namespace RGR_securitySystem.Crypt.LinearEncoding
{
    public class LinearEncoding
    {
        public string[] Encoding(byte[] input)
        {
            List<string> result = new List<string>();

            for(int i = 0; i < input.Length; i+=2)
            {
                string codeGroup = "";

                codeGroup += input[i];
                if (i + 1 < input.Length)
                {
                    codeGroup += input[i + 1];
                }
                else
                {
                    codeGroup = codeGroup.Insert(0, "0");
                }

                switch (codeGroup)
                {
                    case "00":
                        result.Add("-2,5 V");
                        break;
                    case "01":
                        result.Add("-0,833 V");
                        break;
                    case "10":
                        result.Add("+2,5 V");
                        break;
                    case "11":
                        result.Add("+0,833 V");
                        break;
                    default:
                        throw new Exception("Bad input");
                }
            }

            return result.ToArray();
        }

        public byte[] Decoding(string[] input)
        {
            List<byte> result = new List<byte>();

            foreach (var node in input)
            {
                switch (node)
                {
                    case "-2,5 V":
                        result.Add(0);
                        result.Add(0);
                        break;
                    case "-0,833 V":
                        result.Add(0);
                        result.Add(1);
                        break;
                    case "+2,5 V":
                        result.Add(1);
                        result.Add(0);
                        break;
                    case "+0,833 V":
                        result.Add(1);
                        result.Add(1);
                        break;
                }
            }

            return result.ToArray();
        }

        public void PrintGraphic(string[] input)
        {
            Console.WriteLine();

            string f_String =    "+2,5V  ";
            string s_String =    "+0,833V";
            string th_String =   "-0,833V";
            string four_String = "-2,5V  ";

            input = input.ToList().GetRange(0, 80).ToArray();

            foreach (var node in input)
            {
                switch (node)
                {
                    case "-2,5 V":
                        four_String += "_";
                        f_String += " ";
                        th_String += " ";
                        s_String += " ";
                        break;
                    case "-0,833 V":
                        four_String += " ";
                        f_String += " ";
                        th_String += "_";
                        s_String += " ";
                        break;
                    case "+2,5 V":
                        four_String += " ";
                        f_String += "_";
                        th_String += " ";
                        s_String += " ";
                        break;
                    case "+0,833 V":
                        four_String += " ";
                        f_String += " ";
                        th_String += " ";
                        s_String += "_";
                        break;
                }
            }

            Console.WriteLine(f_String);
            Console.WriteLine(s_String);
            Console.WriteLine(th_String);
            Console.WriteLine(four_String);
        }
    }
}
