using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjack.classes
{
    class Dealer
    {
        public List<Card> hand = new List<Card>();
        public int pointcounter;
        public int amount;
        public bool security;
        public bool blackjack = false;

        public Dealer()
        {
        }

        public void DealerClear()
        {

                this.hand.Clear();
                this.pointcounter = 0;
        
                blackjack = false;
            security = false;


        }

        public int DealerDebt(int roundsum, List<Player> players)
        {

            int dealerdebt = 0;
            var player = players;
            for (int x = 0; x < player.Count; x++)
            {
                switch (player[x].score)
                {
                    case 0:
                        dealerdebt = dealerdebt + player[x].bet;
                        break;
                    case 1:
                        break;
                    case 2:
                        dealerdebt = dealerdebt - player[x].bet;
                        break;
                    case 3:
                        dealerdebt = dealerdebt - ((player[x].bet * 3)/2);
                        break;
                       
                }

            }


            return 0;
        }
    }
}
