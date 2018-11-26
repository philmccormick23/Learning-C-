using System;

namespace Deck_Of_Cards
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Deck myDeck = new Deck();
            // foreach (var card in myDeck.cards)
            // {
            //     System.Console.WriteLine(card.Value);
            // }
            myDeck.shuffle();
            myDeck.reset();
            myDeck.printOut();
            
            // myDeck.printOut();
            myDeck.shuffle();
            // foreach (Card card in myDeck.cards) {
            //     myDeck.printOut();
            // }
            // myDeck.printOut();
            myDeck.shuffle();
            //Console.WriteLine(myDeck.deal().StringVal);
            //Console.WriteLine(myDeck.deal().StringVal);
            // Console.WriteLine(myDeck.cards.Count);
            Player amanda = new Player("Amanda");
            amanda.draw(myDeck);
            amanda.draw(myDeck);
            amanda.draw(myDeck);
            amanda.draw(myDeck);
            amanda.draw(myDeck);
            //Console.WriteLine(amanda.discard(0).Value);
        }
    }
}
