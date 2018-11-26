namespace Human
{
    public class Samurai : Human
    {
        public Samurai (string name) : base(name)  
        {
            Name = name;
            Health = 200;
        }

        public void Death_Blow(Human human) 
        {
            if (human.Health <50) {
                human.Health = 0;
            }
        }

        public void Meditate()
        {
            Health = 200;
        }
    }
}