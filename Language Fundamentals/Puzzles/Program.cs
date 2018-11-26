using System;
using System.Collections.Generic;

namespace Puzzles
{
    class Program
    {
        static void RandomArray() 
        {
            int[] randomArray = new int[10];
            int max = randomArray[0];
            int min = randomArray[0];
            int sum = 0;
            Random rand = new Random();
            for (int i = 0; i < 10; i++) {
                randomArray[i] = rand.Next(5,26);
                System.Console.WriteLine(randomArray[i]);
                sum += randomArray[i];
                if (randomArray[i] > max) {
                    max = randomArray[i];
                }
                if (randomArray[i] < min) {
                    min = randomArray[i];
                }
            }
            System.Console.WriteLine(max);
            System.Console.WriteLine(min);
            System.Console.WriteLine(sum);
            
        }

        static String CoinFlip()
        {
            Random rand = new Random();
            string result = "Heads";
            if (rand.Next(0,2) == 1)
            {
                result = "Tails";
            }
            Console.WriteLine("Result: " + result);
            return result;
        }

        static Double TossMultipleCoins(int num)
        {
            Double headsNum = 0;
            Random rand = new Random();
            for (int i = 0; i < num; ++i){
                if (CoinFlip() == "Heads")
                {
                    ++headsNum;
                }
            }
            Console.WriteLine($"Heads Ratio: {headsNum/num}");
            return headsNum/num;
        }

        static string[] Names() //no shuffle
        {
            string[] names = {"Todd", "Tiffany", "Charlie", "Geneva", "Sydney"};
            // shuffle
            Random rand = new Random();
            String temp = "";
            for (int i = 0; i < names.Length; ++i)
            {
                int index = rand.Next(i,names.Length);
                temp = names[i];
                names[i] = names[index];
                names[index] = temp;
                Console.WriteLine("Index {0} has {1}", i, names[i]);
            }
            List<string> longNamesList = new List<string>();
            for (int i = 0; i < names.Length; ++i)
            {
                if (names[i].Length > 5){
                    longNamesList.Add(names[i]);
                }
            }
            string[] longNames = longNamesList.ToArray();
            for (int i = 0; i < longNames.Length; ++i)
            {
                Console.WriteLine(longNames[i]);
            }
            return longNames;
        }
        static void Main(string[] args)
        {
            Names();
        }
    }
}
