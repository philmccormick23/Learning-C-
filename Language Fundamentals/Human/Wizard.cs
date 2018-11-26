using System;

namespace Human
{
    public class Wizard : Human
    {
                                    //base(name) calls the Human constructor. otherwise, there is error because human parameters haven't been met 
        public Wizard (string name) : base(name)  
        {
            Name = name;
            Intelligence = 25;
            Health = 40;
        }

        public void Heal() {
           Health += (Intelligence * 10); 
        }

        public void Fireball(Human human) {
            Random rand = new Random();
            human.Health -= rand.Next(20, 50);
            
        }
    }
}