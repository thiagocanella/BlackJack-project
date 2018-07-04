using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjack.classes
{
    public class Card
    {
        public int suitnum;
        public string name;
        public string suit;
        public int number;
        public bool royal = false;

        public Card(int a , int b)
        {
            number = a;
            suitnum = b;
            suit = "";
            name = "";
        }



        public void Reorder()
        {
            switch (suitnum)
            {
                case 1:
                    this.suit = "D";
                    break;
                case 2:
                    this.suit = "C";
                    break;
                case 3:
                    this.suit = "H";
                    break;
                case 4:
                    this.suit = "S";
                    break;
            }

            switch (number)
            {
                case 1:
                    this.name = "ACE  ";
                    break;
                case 2:
                    this.name = "2    ";
                    break;
                case 3:
                    this.name = "3    ";
                    break;
                case 4:
                    this.name = "4    ";
                    break;
                case 5:
                    this.name = "5    ";
                    break;
                case 6:
                    this.name = "6    ";
                    break;
                case 7:
                    this.name = "7    ";
                    break;
                case 8:
                    this.name = "8    ";
                    break;
                case 9:
                    this.name = "9    ";
                    break;
                case 10:
                    this.name = "10   ";
                    break;
                case 11:
                    this.name = "Jack ";
                    this.royal = true;
                    break;
                case 12:
                    this.name = "Queen";
                    this.royal = true;
                    break;
                case 13:
                    this.name = "King ";
                    this.royal = true;
                    break;
            }
        }


    }
}
