using System;
using System.Collections.Generic;

namespace Deck_Of_Cards {
    public class Deck {
        public List<Card> cards = new List<Card> ();
        public string[] suits = { "Spades", "Hearts", "Diamonds", "Clubs" };
        public string[] ranks = { "Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King" };
        public Deck () {
            foreach (string suit in suits) {
                int value = 1;
                foreach (string rank in ranks) {
                    // cards.Add (new Card (rank, suit, ++value));
                    Card newCard = new Card(rank, suit, value++);
                    cards.Add(newCard);
                }
            }
        }


        public void printOut () {
            foreach (Card card in cards) {
                Console.WriteLine ($"{card.StringVal} of {card.Suit}, value is {card.Value}");
            }
        }

        public void shuffle () {
            Random rand = new Random ();
            for (int i = 0; i < cards.Count; i++)
            {
                int newIndx = rand.Next(cards.Count);
                Card temp = cards[i];
                cards[i]=cards[newIndx];
                cards[newIndx]=temp;
            }
        }

        public Card deal () {
            Card dealt = cards[0];
            cards.Remove(dealt);
            return dealt;
        }

        public void reset() {
            foreach (string suit in suits) {
                int value = 1;
                foreach (string rank in ranks) {
                    // cards.Add (new Card (rank, suit, ++value));
                    Card newCard = new Card(rank, suit, value++);
                    cards.Add(newCard);
                }
            }
        }
    }
}