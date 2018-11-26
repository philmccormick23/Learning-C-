using System;
using System.Collections.Generic;

namespace Basic13
{
    class Program
    {
        
        static void Print255() 
        {
            for (int i = 1; i < 256; i++)
            {
                Console.WriteLine(i);
            }
        }

        static void PrintOdd()
        {
            for (int i = 1; i < 256; i+=2)
            {
                Console.WriteLine(i);
            }
        }

        static void PrintSum()
        {
            int sum = 0;
            for (int i = 0; i < 256; i++)
            {
                sum +=i;
                Console.WriteLine($"New Number: {i}, Sum: {sum}");
            }
        }

        static void iterateArray(int[] arr) 
        {
            foreach (var idx in arr)
            {
                Console.WriteLine(idx);
            }
        }

        static void FindMax(int[] arr)
        {
            int max = arr[0];
            foreach (var num in arr)
            {
                if (num > max) {
                    max = num;
                }
            }
            Console.WriteLine(max);
        }

        static void Average(int[] arr) {
            int sum = 0;
            for(int q = 0; q < arr.Length; q++){
                sum += arr[q];
            }
            double avg = (double)sum / arr.Length;
            System.Console.WriteLine(avg);
        }


        static List<int> ArrayOddNums() {
            List<int> odds = new List<int>();
            for (int i = 1; i < 256; i+=2)
            {
                odds.Add(i);
            }
            return odds;
        }

        static void GreaterThanY(int[] arr, int Y) {
            int count=0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > Y) {
                    count +=1;
                }
            }
            System.Console.WriteLine(count);
        }

        static void SquareVals(int[] arr) {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i]=arr[i]*arr[i];
                System.Console.WriteLine(arr[i]);
            }
        }

        static void eliminateNegNums(int[] arr) {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i]<0) {
                    arr[i]=0;
                }
                System.Console.WriteLine(arr[i]);
            }
        }

        static void MinMaxAvg(int[] arr) {
            int min = arr[0];
            int max = arr[0];
            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i]>max) {
                    max = arr[i];
                }
                if (arr[i]<min) {
                    min = arr[i];
                }
                sum +=arr[i];
            }
            double avg = (double)sum / arr.Length;
            System.Console.WriteLine(max);
            System.Console.WriteLine(min);
            System.Console.WriteLine(avg);

        }
        static void ShiftArr(int[] arr){
            for( int n =0; n < arr.Length; n++){
                if (n < arr.Length - 1){
                    arr[n] = arr[n + 1];
                }
                else{
                    arr[n] = 0;
                }
            }
            System.Console.WriteLine(string.Join(", ", arr)); //This should be written down *******
        }

        static void NumToString(object[] arr){
            for(int i = 0; i < arr.Length; i++)
            {
                if((int)arr[i] < 0)
                {
                    arr[i] = "Dojo";
                }
                System.Console.WriteLine(arr[i]);
            }

        }



        static void Main(string[] args)
        {


            // List<int> odds = ArrayOddNums();
            // foreach (var item in odds)
            // {
            //     System.Console.WriteLine(item);
            // }

            //FindMax(new int[] {1,2,3,4});
            NumToString(new object[] {1,2,-3,4});


        }

        



    }
}
