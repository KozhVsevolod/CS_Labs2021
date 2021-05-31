﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab1InfoResearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            string path = @"C:\Users\vsevo\Desktop\University\CS\Lab_1\";
            string textFile;
            int countSymbolsTotal;
            long sizeFile;
            double entropy, infoQuantity;
            Dictionary<char, double> symb = new Dictionary<char, double>();


            try
            {
                Console.Write("Введіть .txt файл: ");
                textFile = Console.ReadLine();
                Console.WriteLine();
                path += textFile;

                sizeFile = ReadFile(path, symb, out countSymbolsTotal);
                SymbolFrequency(symb, countSymbolsTotal);
                entropy = AvgEntropy(symb);
                infoQuantity = InfoQuantity(entropy, countSymbolsTotal);
                Print(symb, countSymbolsTotal, entropy, infoQuantity, sizeFile);
            }
            catch (FileNotFoundException fnfexc)
            {
                Console.WriteLine(fnfexc.Message);
            }
            catch (IOException ioexc)
            {
                Console.WriteLine(ioexc.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }



        public static void SymbolFrequency(Dictionary <char, double> symb, int totalCount)
        {
            //The number of keys in the dictionary
            int countKeysDict = symb.Keys.Count;
            char[] keysDict = new char[countKeysDict];
            symb.Keys.CopyTo(keysDict, 0);

            for (int iter = 0; iter < countKeysDict; iter++)
            {
                symb[keysDict[iter]] /= totalCount;
            }
        }

        public static double InfoQuantity(double entropy, int countSymb)
        {
            return entropy * countSymb;
        }

        public static double AvgEntropy(Dictionary<char, double> symb)
        {
            int symbCount = symb.Keys.Count;
            char[] keysDict = new char[symbCount];
            symb.Keys.CopyTo(keysDict, 0);
            double probab = 0, entropy = 0;

            for (int iter = 0; iter < symbCount; iter ++)
            {
                probab = symb[keysDict[iter]];
                entropy -= probab * Math.Log(probab, 2);
            }
            return entropy;
        }

        public static void Print(Dictionary<char, double> countsymb, int totalCount, double entropy, double infoQuant, long fileSize)
        {

            Console.WriteLine("Розмір файлу = {0} bytes", fileSize);
            Console.WriteLine("Info Quantity = {0} bytes", infoQuant / 8);
            Console.WriteLine("Info Quantity = {0} bits", infoQuant);
            Console.WriteLine("Entropy = {0}", entropy);
            Console.WriteLine("Загальна кіл-сть символів = {0}", totalCount);


            SortedDictionary<char, double> sortedDict = new SortedDictionary<char, double>(countsymb);
            foreach (KeyValuePair<char, double> kvp in sortedDict)
            {
                Console.WriteLine(kvp.Key + " - " + kvp.Value);
            }
        }

        public static long ReadFile(string pathFile, Dictionary <char, double> symb, out int totalCountSymb)
        {
            FileInfo fileSize = new FileInfo(pathFile);
            int i;
            
            double symbRec;
            totalCountSymb = 0;
            
            string allText = File.ReadAllText(pathFile);
            i = 0;
            while (i < allText.Length)
            {
                symbRec = 1;
                if (!symb.ContainsKey(allText[i]))
                {
                    symb.Add(allText[i], symbRec);
                }
                else
                    if (symb.ContainsKey(allText[i]))
                    {
                        symb[allText[i]]++;
                    }
                i++;
                totalCountSymb++;
            }
            return fileSize.Length;
        }

    }
}