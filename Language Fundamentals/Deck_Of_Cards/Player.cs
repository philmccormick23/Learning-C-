
using System.Collections.Generic;

namespace Deck_Of_Cards
{
    public class Player
    {
        public string name;
        public List<Card> hand = new List<Card>();
        public Player(string newName)
    {
      name = newName;
    }
    public Card draw(Deck deck)
    {
      Card newCard = deck.deal();
      hand.Add(newCard);
      return newCard;
    }

    public Card discard(int index)
    {
      if (hand.Count > index)
      {
        Card discarded = hand[index];
        hand.RemoveAt(index);
        return discarded;
      }
      else {
        return null;
      }

    }

    } 
}