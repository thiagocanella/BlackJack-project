using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjack.classes
{
    public class Cardset 
    {
        public List<Card> doubledeck = new List<Card>();
        public List<Card> cardset = new List<Card>();
        List<Card> doubledk = new List<Card>();
        List<Card> mixed = new List<Card>();
        public Cardset(bool start)
        {
            if (start == true)
            { 
                //Build a cardset

            var mixed2 = mixed;
            var cardset2 = cardset;
            for (int y = 1; y < 5; y++)
            {
                for (int x = 1; x < 14; x++)
                {
                    cardset.Add(new Card(x, y));

                }
            }
            for (int x = 0; x < cardset.Count; x++)
            {
                cardset[x].Reorder();
            }//Name cards for display



            //shuffle two cardset

            Random r = new Random();
            int randomIndex = 0;
            while (cardset.Count > 0)
            {

                randomIndex = r.Next(0, cardset.Count); //Choose a random object in the list
                mixed.Add(cardset[randomIndex]); //add it to the new, random list
                cardset.RemoveAt(randomIndex); //prevent to repeat a card
            }
                randomIndex = 0;
            while (cardset2.Count > 0)
            {

                randomIndex = r.Next(0, cardset2.Count); //Choose a random object in the list
                mixed2.Add(cardset[randomIndex]); //add it to the new, random list
                cardset2.RemoveAt(randomIndex); //prevent to repeat a card
            }

            randomIndex = 0;
            while (randomIndex < mixed.Count)
            {
                doubledk.Add(mixed[randomIndex]);
                doubledk.Add(mixed2[randomIndex]);
                randomIndex++;

            }
                randomIndex = 0;
                while (doubledk.Count > 0)
                {

                    randomIndex = r.Next(0, doubledk.Count); //Choose a random object in the list
                    cardset.Add(doubledk[randomIndex]); //add it to the new, random list
                    doubledk.RemoveAt(randomIndex); //prevent to repeat a card
                }
                randomIndex = 0;

                doubledeck = cardset;

            }
        }

    }
}
