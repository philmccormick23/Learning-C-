using System;
using System.Collections.Generic; //needed for lists **

namespace CollectionsPractice
{
    class Program
    {
        static void Main(string[] args)
        {
           //Three Basic Arrays
            int[] intArray={1,2,3,4,5,6,7,8,9};
            string[] stringArray={"Tim", "Martin", "Nikki", "Sara"};
            bool[] boolArray = {true, false, true, false, true, false, true, false, true, false};
            
            
            //List of Flavors
            List<string> iceCream = new List<string>();
            iceCream.Add("Fragola");
            iceCream.Add("Chocolate");
            iceCream.Add("Vanilla");
            iceCream.Add("Cafe");
            iceCream.Add("Pistacio");
            iceCream.Add("Rocky Road");
            iceCream.Add("Dolche de Leche");
            iceCream.Add("Oreo");
            iceCream.Add("Mint Chocolate Chip");
            iceCream.Add("Taro");
            iceCream.RemoveAt(3);
            Console.WriteLine(iceCream.Count);


            //User Info Dictionary 
            Random rand = new Random();
            Dictionary<string,string> favorites = new Dictionary<string,string>();
            foreach (string name in stringArray){
                favorites[name] = iceCream[rand.Next(iceCream.Count)];
            }
            foreach (KeyValuePair<string,string> entry in favorites){
                Console.WriteLine(entry.Key + " - " + entry.Value);
            }
        }
    }
}
