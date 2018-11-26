namespace Deck_Of_Cards
{
    public class Card
    {
        public string StringVal { get; set; }
        public string Suit { get; set; }
        public int Value { get; set; }

        public Card(string newStringVal, string newSuit, int newVal) 
        {
            StringVal = newStringVal;
            Suit = newSuit;
            Value = newVal;
        }
    }
}