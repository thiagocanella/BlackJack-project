using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjack.classes
{
    public class Counters
    {
        public List<Card> entering = new List<Card>();
        public Counters()
        {

        }
        public int PointCounter(List<Card> entering)
        {
            int pts = 0;
            var entry = entering;
            int isace = 0;
            bool acedeprecated = false;
            for (int x = 0; x < entry.Count; x++)
            {
                if (entry[x].number == 1)
                {
                    isace++;
                }
            }

            if (isace > 1)
            {
                acedeprecated = true;
            }

            for (int x = 0; x < entry.Count; x++)
            {
                switch (entry[x].number)
                {
                    case 1:
                        if (acedeprecated == false) { pts += 11; }
                        else { pts += 1; }

                        break;
                    case 2:
                        pts += 2;
                        break;
                    case 3:
                        pts += 3;
                        break;
                    case 4:
                        pts += 4;
                        break;
                    case 5:
                        pts += 5;
                        break;
                    case 6:
                        pts += 6;
                        break;
                    case 7:
                        pts += 7;
                        break;
                    case 8:
                        pts += 8;
                        break;
                    case 9:
                        pts += 9;
                        break;
                    case 10:
                        pts += 10;
                        break;
                    case 11:
                        pts += 10;
                        break;
                    case 12:
                        pts += 10;
                        break;
                    case 13:
                        pts += 10;
                        break;


                }



            }
            if (pts > 21)
            {
                if (isace > 0)
                {
                    if (acedeprecated == false)
                    {
                        pts -= 10 * isace;
                    }

                }
            
            }
            if (pts > 21)
            {
                pts = 0;
            }
            return pts;
        }

        public int DealerDeal(int dealerpoints, bool dealerblackjack, int actualplayerpoints, bool actualplayerblackjack)
        {
            int score = 0;
            var a = dealerpoints;
            var amax = dealerblackjack;
            var b = actualplayerpoints;
            var bmax = actualplayerblackjack;
            if (a == b)
            {
                score = 1;
                if (amax == true && bmax == false)
                {
                    score = 0;
                }
                if (amax == false && bmax == true)
                {
                    score = 3;
                }
            }
            if (a > b)
            {
                score = 0;
            }
            if (a < b)
            {
                score = 2;
            }


            return score;
        }
    }
}

