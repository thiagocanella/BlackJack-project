using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjack.classes
{
    class Player
    {
        public List<Card> hand = new List<Card>();
        public int pointcounter;
        public int bet;
        public int amount = 0;
        public string Name;
        public bool makebet = false;
        public bool blackjack = false;
        public bool insurance = false;
        public bool loose = false;
        public bool doubledown = false;
        public int score = 1;
        
        public Player(string Nam)
        {
            Name = Nam;

        }

        public void PlayerHand(Card card1,Card card2, int money)
        {
            hand.Add(card1);
            hand.Add(card2);
            bet = money;
            Name = "";
            return;
        }

        public void HandClear()
        {
            this.hand.Clear();
            this.pointcounter = 0;
            bet = 0;
            loose = false;
            makebet = false;
            doubledown = false;
            score = 1;
            blackjack = false;
            insurance = false;
        }
    }
}
