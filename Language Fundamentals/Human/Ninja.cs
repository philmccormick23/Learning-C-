namespace Human
{
    public class Ninja : Human
    {
        public Ninja (string name) : base(name)  
        {
            Name = name;
            Dexterity = 175;
        }

        public void Steal()
        {
            Health +=10;
        }

        public void Get_Away() {
            Health -= 15;
        }
    }
}