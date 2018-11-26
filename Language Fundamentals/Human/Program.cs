using System;

namespace Human
{
    class Program
    {
        static void Main(string[] args)
        {
            ///////////////////////////  Human Tests //////////////////////////////
            Human phil = new Human("Phil");
            //System.Console.WriteLine(phil.Name);

            Human sam = new Human("Sam", 5, 7, 5, 120);
            //Console.WriteLine("Character Name: " + sam.Name + " Health: " + sam.Health + " Intelligence: " + sam.Intelligence + " Dexterity: " + sam.Dexterity + " Strength: " + sam.Strength);
            phil.Attack(sam);
            //Console.WriteLine("Character Name: " + sam.Name + " Health: " + sam.Health + " Intelligence: " + sam.Intelligence + " Dexterity: " + sam.Dexterity + " Strength: " + sam.Strength);



            ///////////////////////////  Wizard Tests //////////////////////////////
            Wizard gandolf = new Wizard("Gandolf");
            //System.Console.WriteLine(gandolf.Dexterity);
            //Console.WriteLine("Character Name: " + sam.Name + " Health: " + sam.Health + " Intelligence: " + sam.Intelligence + " Dexterity: " + sam.Dexterity + " Strength: " + sam.Strength);
            gandolf.Heal();
            //System.Console.WriteLine(gandolf.Health);
            //Console.WriteLine("Character Name: " + sam.Name + " Health: " + sam.Health + " Intelligence: " + sam.Intelligence + " Dexterity: " + sam.Dexterity + " Strength: " + sam.Strength);
            gandolf.Fireball(sam);
            // Console.WriteLine("Character Name: " + sam.Name + " Health: " + sam.Health + " Intelligence: " + sam.Intelligence + " Dexterity: " + sam.Dexterity + " Strength: " + sam.Strength);




            ///////////////////////////  Ninja Tests //////////////////////////////

            Ninja dude =new Ninja("dude");
            //Console.WriteLine("Character Name: " + dude.Name + " Health: " + dude.Health + " Intelligence: " + dude.Intelligence + " Dexterity: " + dude.Dexterity + " Strength: " + dude.Strength);

            dude.Steal();
            dude.Get_Away();
            //Console.WriteLine("Character Name: " + dude.Name + " Health: " + dude.Health + " Intelligence: " + dude.Intelligence + " Dexterity: " + dude.Dexterity + " Strength: " + dude.Strength);





            ///////////////////////////  Samurai Tests //////////////////////////////
            Samurai joe = new Samurai("joe");
            Wizard test = new Wizard("test");
            joe.Death_Blow(test);
            //System.Console.WriteLine(test.Health);
            
            phil.Attack(joe);
            //System.Console.WriteLine(joe.Health);
            joe.Meditate();
            //System.Console.WriteLine(joe.Health);


            

        }
    }
}
