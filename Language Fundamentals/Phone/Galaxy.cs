using System;

namespace Phone {
    public class Galaxy : Phone, IRingable {
        public Galaxy (string versionNumber, int batteryPercentage, string carrier, string ringTone) : base (versionNumber, batteryPercentage, carrier, ringTone) { }
        public string Ring () {
            string returnRing = $"....{ringTone}....";
            return returnRing;
        }

        public string Unlock () {
            string returnUnlock = $"Galaxy {versionNumber} unlocked with passcode.";
            return returnUnlock;
        }
        public override void DisplayInfo () {
            // your code here  
            Console.WriteLine("#####################");
            Console.WriteLine($"{versionNumber}");
            Console.WriteLine($"{batteryPercentage}");
            Console.WriteLine($"{carrier}");
            Console.WriteLine($"{ringTone}");
            Console.WriteLine("#####################");          
        }
    }
}