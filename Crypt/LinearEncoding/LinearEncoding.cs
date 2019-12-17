using System;
using System.Collections.Generic;

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
    }
}
