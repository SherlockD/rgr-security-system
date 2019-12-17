using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace RGR_securitySystem.Crypt.HammingCode
{
    public class HammingCode
    {
        private int controlBitsCount = 0;
        
        public byte[] Encrypt(byte[] input)
        {
            List<byte> informationWord = new List<byte>(input);

            List<List<byte>> matrix = new List<List<byte>>();
            
            List<byte> r = new List<byte>();
            
            controlBitsCount = (int)Math.Round(Math.Log(informationWord.Count, 2)) + 1;

            for (int i = 0; i < controlBitsCount; i++)
            {
                informationWord.Insert((int)Math.Pow(2,i) - 1, 0);
            }

            var columns = GetNumbersColumns(informationWord.Count);

            for (int i = 0; i < controlBitsCount; i++)
            {
                matrix.Add(new List<byte>());
            }
            
            for (int i = 0; i < controlBitsCount; i++)
            {
                foreach (var column in columns)
                {
                    byte value = (column.Count <= i) ? (byte) 0 : column[i];

                    matrix[i].Add(value);
                }

                Console.WriteLine();
                matrix[i].ForEach((node) => Console.Write(node));
            }
            
            for (int i = 0; i < controlBitsCount; i++)
            {
                int result = 0;
                
                for (int j = 0; j < columns.Count; j++)
                {                    
                    result += ((i >= columns[j].Count) ? 0 : columns[j][i]) * informationWord[j];
                }
                
                informationWord[(int) Math.Pow(2, i) - 1] = (byte)(result % 2);
            }

            return informationWord.ToArray();
        }

        public byte[] Decrypt(byte[] input)
        {
            /* Test */
            input[5] = 1;
            
            Console.WriteLine();
            Console.WriteLine();
            /* End Test */
            
            List<byte> informationWord = new List<byte>(input);

            List<List<byte>> matrix = new List<List<byte>>();
            
            List<byte> r = new List<byte>();

            string startRegister = "";
            string newRegister = "";
            string syndomRegister = "";
            
            for (int i = 0; i < controlBitsCount; i++)
            {
                startRegister += informationWord[(int) Math.Pow(2, i) - 1];
                
                informationWord.RemoveAt((int)Math.Pow(2, i) - 1);
                
                informationWord.Insert((int)Math.Pow(2, i) - 1, 0);
            }

            var columns = GetNumbersColumns(informationWord.Count);

            for (int i = 0; i < controlBitsCount; i++)
            {
                matrix.Add(new List<byte>());
            }
            
            for (int i = 0; i < controlBitsCount; i++)
            {
                foreach (var column in columns)
                {
                    byte value = (column.Count <= i) ? (byte) 0 : column[i];

                    matrix[i].Add(value);
                }
                               
                Console.WriteLine();
                matrix[i].ForEach((node) => Console.Write(node));
            }
            
            for (int i = 0; i < controlBitsCount; i++)
            {
                int result = 0;
                
                for (int j = 0; j < columns.Count; j++)
                {                    
                    result += ((i >= columns[j].Count) ? 0 : columns[j][i]) * informationWord[j];
                }
                
                newRegister += (byte)(result % 2);
            }

            for (int i = 0; i < startRegister.Length; i++)
            {
                syndomRegister += (startRegister[i] != newRegister[i]) ? 1 : 0;
            }
                      
            //Console.WriteLine();
            //Console.WriteLine(startRegister);
            //Console.WriteLine(newRegister);
            //Console.WriteLine(syndomRegister);

            //syndomRegister = syndomRegister.Reverse();

            int deletesCount = 0;

            //double registerWithError = String2To10(syndomRegister);

            if (syndomRegister.Contains('1'))
            {
                int registerWithError = String2To10(syndomRegister) - 1;
                informationWord[registerWithError] = (byte)((informationWord[registerWithError] == 1) ? 0 : 1);

                for (int i = 0; i < controlBitsCount; i++)
                {
                    var index = (int)Math.Pow(2, i) - 1 - deletesCount;

                    deletesCount++;

                    //Console.WriteLine(index);
                    informationWord.RemoveAt(index);
                }
            }

            return informationWord.ToArray();
        }
        
        private List<List<byte>> GetNumbersColumns(int number)
        {
            List<List<byte>> result = new List<List<byte>>();
            
            for (int i = 0; i < number; i++)
            {
                var newColumn = new List<byte>();
                
                var twoFormat = Convert.ToString(i + 1, 2);

                foreach (var sym in twoFormat)
                {
                    newColumn.Add((byte)Char.GetNumericValue(sym));
                }

                newColumn.Reverse();
                
                result.Add(newColumn);
            }

            return result;
        }

        private int String2To10(string doubleCode)
        {
            double result = 0;

            for(int i = 0;i< doubleCode.Length; i++)
            {
                if(doubleCode[i] == '1')
                {
                    result += Math.Pow(2, i);
                }
            }

            return (int)result;
        }
    }
}