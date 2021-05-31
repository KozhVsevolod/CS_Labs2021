using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1c
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the first signed number");
            int multiplicand = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the second signed number");
            int multiplier = int.Parse(Console.ReadLine());
            Evaluate(multiplicand, multiplier);
            Console.ReadKey();
        }

        public static void Evaluate(int multiplicand, int multiplier)
        {
            Int64 P = 0;
            multiplicand <<= 16;
            string A_bits = ToBinary(multiplicand), B_bits = ToBinary(multiplier);
            Console.WriteLine("\tMultiplicand: {0}", A_bits);
            Console.WriteLine("\tMultiplier: {0}", B_bits);


            Console.WriteLine("Multiply :");
            for (int i = 1; i < 17; ++i)
            {
                Console.WriteLine(" Step " + i + ":");
                if ((multiplier & 1) == 1)
                {
                    Console.WriteLine("  \tAdd Multiplicand:\t{0}\n\tTo Product:\t\t{1}", A_bits, ToBinary(P));
                    P += multiplicand;

                }
                P >>= 1;
                Console.WriteLine("  \tProduct :\t\t{0}", ToBinary(P));
                Console.WriteLine("  \tShift Multiplier right:\t" + ToBinary(multiplier));
                multiplier >>= 1;
                Console.WriteLine("  \t\t\t " + ToBinary(multiplier));
            }
            Console.WriteLine("  Answer is:\n\tIn decimal: {0}\n\tIn binary: {1}", P, ToBinary(P));
        }

        public static string ToBinary(Int64 num)
        {
            string binary = string.Empty;
            for (int i = 1; i < 33; ++i)
            {
                binary = (i % 4 == 0 ? " " : "") + (num & 1) + binary;
                num >>= 1;
            }
            return binary;
        }

    }
}