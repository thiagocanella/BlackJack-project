using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjack.classes
{
    class Game
    {

        public List<Player> players = new List<Player>();
        public Dealer dealer = new Dealer();
        public Counters counter = new Counters();
        public Cardset mont = new Cardset(true);
        public int numofplayers = 0;

        public Game()
        {

            int x = mont.doubledeck.Count;

        }

        public void Start()
        {






            // Pergunta qauntidade de jogadores
            Console.WriteLine("Wellcome to BlackJack\nTwo cardsets are used in this table\n\nYou Must Chose Between 3 and 12 players \nHow many Players will play?");
            numofplayers = Int32.Parse(Console.ReadLine());
            System.Threading.Thread.Sleep(1000);



            //Força que seja entre 3 e 12 jogadores
            while (numofplayers < 3 || numofplayers > 12)
            {
                Console.Clear();
                Console.WriteLine("You Must Chose Between 3 and 12 players \nHow many Players will play?");
                numofplayers = Int32.Parse(Console.ReadLine());
                System.Threading.Thread.Sleep(1000);
            }
            //Monta Lista de jogadores com seus nomes

            for (int x = 1; x <= numofplayers; x++)
            {
                Console.Clear();
                Console.WriteLine($"Enter your name, Player {x}");
                players.Add(new Player(Console.ReadLine()));

                System.Threading.Thread.Sleep(1000);


            }

        }
        public void Rounds()
        {
            bool gameon = true;

            while (gameon == true)
            {


                for (int x = 1; x <= numofplayers; x++)
                {
                    Console.Clear();
                    //BETS
                    do
                    {
                        Console.WriteLine($"Max BET: $300\nMin BET:5\n {players[x - 1].Name}, enter your bet please");
                        players[x - 1].bet = Int32.Parse(Console.ReadLine());

                    }
                    while (players[x - 1].bet < 5 || players[x - 1].bet > 300);

                    if (players[x - 1].bet > 0)
                    {
                        players[x - 1].makebet = true;

                    }

                    else
                    { players[x - 1].makebet = false; }
                    System.Threading.Thread.Sleep(1000);
                    //END BETS

                }
                int countdeck = mont.doubledeck.Count - 1;
                dealer.DealerClear();
                dealer.hand.Add(mont.doubledeck[countdeck]);
                mont.doubledeck.RemoveAt(countdeck);
                countdeck--;

                dealer.hand.Add(mont.doubledeck[countdeck]);
                mont.doubledeck.RemoveAt(countdeck);
                countdeck--;
                dealer.pointcounter = counter.PointCounter(dealer.hand);
                //INSURANCE TRIGGER
                if (dealer.hand[0].name == "ACE")
                {
                    dealer.security = true;
                }
                if (dealer.pointcounter == 21)
                {
                    if (dealer.hand[0].royal == true || dealer.hand[1].royal == true)
                    {
                        dealer.blackjack = true;
                    }
                }
                // PLAYERS CARD PLACEMENT
                for (int x = 0; x < players.Count; x++)
                {
                    if (players[x].makebet == true)
                    {




                        players[x].hand.Add(mont.doubledeck[countdeck]);
                        mont.doubledeck.RemoveAt(countdeck);
                        countdeck--;
                        players[x].hand.Add(mont.doubledeck[countdeck]);
                        mont.doubledeck.RemoveAt(countdeck);
                        players[x].pointcounter = counter.PointCounter(players[x].hand);
                        countdeck--;
                    }
                }

                //SHOW TABLE AFTER STARTING
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine($"ROUND'S HANDS: \n Dealer ({dealer.hand[0].suit} - {dealer.hand[0].name}  ,  X-X)\n");
                for (int x = 0; x < players.Count; x++)
                {

                    string blackjack = "";
                    if (players[x].pointcounter == 21 && (players[x].hand[0].royal == true || players[x].hand[1].royal == true))
                    {
                        blackjack = " BLACKJACK!";
                        players[x].blackjack = true;
                    }
                    if (players[x].makebet == true)
                    {
                        Console.Write($"Player {players[x].Name}'s hand: ({players[x].hand[0].suit} - {players[x].hand[0].name} , {players[x].hand[1].suit} - {players[x].hand[1].name}) (Points: {players[x].pointcounter}{blackjack}) (Bet: ${players[x].bet}) \n");
                    }
                }
                Console.WriteLine("Presse any key to continue...");


                Console.ReadKey();
                //END SHOW TABLE AFTER STARING




                //BUY, DOUBLEDOWN, INSURANCE
                Console.Clear();



                for (int x = 0; x < players.Count; x++)
                {

                    if (players[x].makebet == true && players[x].blackjack == false)
                    {

                        switch (dealer.security)
                        {
                            case true:
                                Console.WriteLine($"Dealer ({dealer.hand[0].suit} - {dealer.hand[0].name}  ,  X-X)\n");

                                Console.WriteLine($"Player {players[x].Name}, do you wan to make an Insurance?\nfor YES type 1 ---  for NO type 0");
                                int yesorno = 5;
                                do
                                {
                                    yesorno = Int32.Parse(Console.ReadLine());

                                }
                                while (yesorno < 0 && yesorno > 1);
                                if (yesorno == 1)
                                {
                                    players[x].insurance = true;
                                }
                                break;

                        }
                        if (players[x].insurance == false && players[x].pointcounter < 21)
                        {
                            Console.WriteLine($"Dealer ({dealer.hand[0].suit} - {dealer.hand[0].name}  ,  X-X)\n");
                            Console.WriteLine($"  Player {players[x].Name} you hand is: ({players[x].hand[0].suit} - {players[x].hand[0].name} , {players[x].hand[1].suit} - {players[x].hand[1].name}) \nDo you want to DOUBLE?\nfor YES type 1 ---  for NO type 0");
                            int yesorno = 5;
                            do
                            {
                                yesorno = Int32.Parse(Console.ReadLine());

                            }
                            while (yesorno < 0 || yesorno > 1);
                            switch (yesorno)
                            {
                                case 1:
                                    players[x].hand.Add(mont.doubledeck[countdeck]);
                                    mont.doubledeck.RemoveAt(countdeck);
                                    countdeck--;
                                    players[x].doubledown = true;
                                    players[x].pointcounter = counter.PointCounter(players[x].hand);
                                    Console.Write("Now your hand is (");

                                    for (int y = 0; y < players[x].hand.Count; y++)
                                    {

                                        Console.Write($" {players[x].hand[y].suit} - {players[x].hand[y].name} ");
                                        if (y < players[x].hand.Count - 1)
                                        {
                                            Console.Write(",");
                                        }
                                    }
                                    string lloose = "";
                                    if (players[x].pointcounter == 0)
                                    {
                                        lloose = "YOU BUST";
                                    }
                                    Console.Write($") (Points = {players[x].pointcounter} {lloose})\n");
                                    Console.WriteLine("\nPresse any key to continue...");
                                    Console.ReadKey();


                                    break;

                                case 0:

                                    if (players[x].pointcounter == 0)
                                    {
                                        players[x].loose = true;

                                        Console.WriteLine("YOU BUST\n\n");
                                        Console.WriteLine("\nPresse any key to continue...");
                                        Console.ReadKey();


                                    }
                                    else
                                    {
                                        if (players[x].pointcounter < 21)
                                        {
                                            int yn = 5;


                                            do
                                            {
                                                yn = 5;
                                                Console.WriteLine("Do you want to HIT? \nfor YES type 1 ---  for NO type 0");


                                                yn = Int32.Parse(Console.ReadLine());

                                                switch (yn)
                                                {
                                                    case 1:
                                                        players[x].hand.Add(mont.doubledeck[countdeck]);
                                                        mont.doubledeck.RemoveAt(countdeck);
                                                        countdeck--;
                                                        players[x].pointcounter = counter.PointCounter(players[x].hand);
                                                        if (players[x].pointcounter == 0)
                                                        {
                                                            players[x].loose = true;
                                                        }
                                                        Console.Write("Now your hand is (");
                                                        for (int y = 0; y < players[x].hand.Count; y++)
                                                        {

                                                            Console.Write($" {players[x].hand[y].suit} - {players[x].hand[y].name} ");
                                                            if (y < players[x].hand.Count - 1)
                                                            {
                                                                Console.Write(",");
                                                            }
                                                        }
                                                        string loose = "";
                                                        if (players[x].loose == true)
                                                        {
                                                            loose = "YOU BUST";
                                                        }
                                                        Console.Write($") (Points = {players[x].pointcounter} {loose})\n");
                                                        if (players[x].loose == true)
                                                        {
                                                            Console.WriteLine("\nPresse any key to continue...");
                                                            Console.ReadKey();
                                                        }

                                                        break;
                                                    case 0:
                                                        break;
                                                }

                                            } while (yn != 0 && players[x].loose == false);
                                        }








                                    }
                                    break;
                            }

                        }
                    }





                    Console.Clear();
                }
                // END BUY , DOUBLEDOWN, INSURANCE


                Console.Clear();
                //COUNTERS AND DEALER DECISION
                int scoresum = 0;
                int players_ontable = 0;
                for (int x = 0; x < players.Count; x++)
                {
                    players[x].score = counter.DealerDeal(dealer.pointcounter, dealer.blackjack, players[x].pointcounter, players[x].blackjack);

                    if (players[x].makebet == true)
                    {
                        players_ontable++; scoresum += players[x].score;
                    }

                }


                //DEALER THINK IF SHOULD BUY A CARD
                if (dealer.blackjack == false)
                {
                    Console.Write("Dealer is wondering if should HIT");
                    System.Threading.Thread.Sleep(700);
                    Console.Write(" . ");
                    System.Threading.Thread.Sleep(700);
                    Console.Write(" . ");
                    System.Threading.Thread.Sleep(700);
                    Console.Write(" . ");
                    System.Threading.Thread.Sleep(700);

                    //int dealerdebt = 0;
                    bool shouldbuy = true;

                    while (shouldbuy == true && dealer.pointcounter != 0)
                    {
                        scoresum = 0;

                        for (int x = 0; x < players.Count; x++)
                        {
                            players[x].score = counter.DealerDeal(dealer.pointcounter, dealer.blackjack, players[x].pointcounter, players[x].blackjack);
                            scoresum += players[x].score;
                        }

                        shouldbuy = Estatistic(scoresum, dealer.pointcounter, players_ontable);


                        dealer.pointcounter = counter.PointCounter(dealer.hand);
                        if (shouldbuy == true)
                        {
                            dealer.hand.Add(mont.doubledeck[countdeck]);
                            mont.doubledeck.RemoveAt(countdeck);
                            countdeck--;
                            dealer.pointcounter = counter.PointCounter(dealer.hand);
                            string dlhandtext = "( ";

                            Console.WriteLine("\nDealer HIT !!!!");
                            for (int x = 0; x < dealer.hand.Count; x++)
                            {
                                dlhandtext += $"|  {dealer.hand[x].suit},{dealer.hand[x].name}  |";
                            }
                            dlhandtext += " )";
                            string lloose = "";
                            if (dealer.pointcounter == 0)
                            {
                                lloose = " DEALER BUST";
                            }
                            Console.WriteLine($"\nDealer Hand: \n{dlhandtext} (Points: {dealer.pointcounter}{lloose})");
                            System.Threading.Thread.Sleep(1000);
                        }

                    }
                    for (int x = 0; x < players.Count; x++)
                    {
                        players[x].score = counter.DealerDeal(dealer.pointcounter, dealer.blackjack, players[x].pointcounter, players[x].blackjack);
                        scoresum += players[x].score;
                    }
                    Console.WriteLine("\nPresse any key to continue...");
                    Console.ReadKey();
                    Console.Clear();

                }
                //END COUNTERS AND DEALER DECISIONS



                // ROUND RESULTS
                string dealerhandtext = "( ";

                for (int x = 0; x < dealer.hand.Count; x++)
                {
                    dealerhandtext += $"|  {dealer.hand[x].suit},{dealer.hand[x].name}  |";
                }
                dealerhandtext += " )";
                string isblackjack = "";
                if (dealer.blackjack == true) { isblackjack = " BLACKJACK!!!"; }
                Console.WriteLine($"ROUND RESULTS:\n Dealer Hand:\n {dealerhandtext} (Points: {dealer.pointcounter}{isblackjack})");


                for (int x = 0; x < players.Count; x++)
                {
                    if (players[x].makebet == true)
                    {
                        string playerhandtext = "( ";
                        for (int y = 0; y < players[x].hand.Count; y++)
                        {
                            playerhandtext += $"|   {players[x].hand[y].suit} , {players[x].hand[y].name}   |";
                        }
                        playerhandtext += " )";
                        string bj = "";
                        string insu = "";
                        if (players[x].blackjack == true) { bj = " BLACKJACK!!!"; }
                        if (players[x].insurance == true) { insu = " WITH INSURANCE"; }

                        string wintext = "";
                        switch (players[x].score)
                        {
                            case 0:
                                wintext = "LOOSE !!";
                                break;
                            case 1:
                                wintext = "DRAW !!";
                                break;
                            case 2:
                                wintext = "WON !!";
                                break;
                            case 3:
                                wintext = "WON !!";
                                break;
                        }

                        Console.WriteLine($"\n\n Player {players[x].Name} Hand:\n {playerhandtext} (Points: {players[x].pointcounter}{bj}) (BET:{players[x].bet}{insu}){wintext}");




                    }
                }
                Console.WriteLine("\nPresse any key to continue...");
                Console.ReadKey();
                //****END ROUND RESULTS

                //PROFITSSHOW
                MoneyCounter();
                Console.Clear();
                Console.WriteLine($"Dealer profit = {dealer.amount}\n\n");
                for (int x = 0; x < players.Count; x++)
                {
                    Console.WriteLine($"Player {players[x].Name} profit = {players[x].amount}\n");
                }


                Console.WriteLine("\nPresse any key to continue...");
                Console.ReadKey();
                //END PROFITS SHOW

                //NEW GAME?
                Console.Clear();


                Console.WriteLine($"\n There is still {mont.doubledeck.Count} cards on deck");
                Console.WriteLine("\n\n Want an another round? \nfor YES type 1 ---  for NO type 0");
                int conti_nue = 5;

                do
                {
                    Console.Clear();


                    Console.WriteLine("\n\n Want an another round? \nfor YES type 1 ---  for NO type 0");
                    conti_nue = Int32.Parse(Console.ReadLine());


                }
                while (conti_nue < 0 && conti_nue > 1);

                switch (conti_nue)
                {
                    case 1:
                        if (mont.doubledeck.Count > ((players.Count * 4) + 2))
                        {
                            Console.Clear();
                            Console.Write("NEW ROUND WILL START IN");
                            System.Threading.Thread.Sleep(1000);
                            Console.Write(" 3 ");
                            System.Threading.Thread.Sleep(1000);
                            Console.Write(" 2 ");
                            System.Threading.Thread.Sleep(1000);
                            Console.Write(" 1 ");
                            System.Threading.Thread.Sleep(1000);

                            Console.Clear();
                            System.Threading.Thread.Sleep(500);

                            Console.Write("\n\nSTART");
                            System.Threading.Thread.Sleep(500);

                            Console.Clear();
                            Console.Write("\n\nSTART");
                            System.Threading.Thread.Sleep(500);

                            Console.Clear();
                            System.Threading.Thread.Sleep(500);

                            Console.Write("\n\nSTART");
                            System.Threading.Thread.Sleep(500);

                            Console.Clear();
                            System.Threading.Thread.Sleep(500);

                            Console.Write("\n\nSTART");
                            System.Threading.Thread.Sleep(500);

                            Console.Clear();
                            System.Threading.Thread.Sleep(500);

                            Console.Write("\n\nSTART");
                            System.Threading.Thread.Sleep(500);
                        }
                        else
                        {
                            Console.Clear();
                            Console.Write("Insufficient cards for a new round\n\nMixing cards");
                            System.Threading.Thread.Sleep(1000);
                            Console.Write(" . ");
                            System.Threading.Thread.Sleep(1000);
                            Console.Write(" . ");
                            System.Threading.Thread.Sleep(1000);
                            Console.Write(" . ");
                            System.Threading.Thread.Sleep(1000);
                            Console.Write(" . ");
                            System.Threading.Thread.Sleep(1000);
                            Console.Write(" . ");
                            System.Threading.Thread.Sleep(1000);
                            Console.Write(" . ");
                            System.Threading.Thread.Sleep(1000);
                            mont = new Cardset(true);

                        }
                        break;

                    case 0:
                        Console.Clear();
                        Console.Write($"\n\n\n\n\n     Thank You \n\n");
                        for (int x = 0; x < players.Count; x++)
                        {
                            Console.Write($" {players[x].Name} ");
                        }
                        Console.WriteLine("For Playing!");
                        gameon = false;
                        Console.WriteLine("\nPresse any key to continue...");
                        Console.ReadKey();
                        break;
                }




                //END NEW GAME

                //CLEAR
                RoundClear();

            }
        }
        bool Estatistic(int sumres, int dealerpts, int sumplayer_onround)
        {
            bool shouldbuy = true;
            float target = 21 - dealerpts;
            float coef = ((100 / 2) * (sumres / sumplayer_onround)) / ((100 / 13) * (13 - target));
            if (target > 9f)
            {
                shouldbuy = true;
            }
            else
            {
                if (coef < 1f)
                {
                    shouldbuy = false;
                }
            }



            return shouldbuy;
        }
        void MoneyCounter()
        {
            int cartridge = 0;
            for (int x = 0; x < players.Count; x++)
            {
                cartridge = 0;
                if (dealer.blackjack == false && players[x].insurance == true)
                {
                    cartridge -= (players[x].bet / 2);
                }
                if (dealer.blackjack == true && players[x].insurance == true)
                {
                    cartridge += (players[x].bet / 2);
                }
                if (players[x].score == 3)
                {
                    cartridge += (players[x].bet * 3) / 2;
                }
                if (players[x].score == 2)
                {
                    cartridge += players[x].bet;
                }
                if (players[x].score == 0)
                {
                    cartridge -= players[x].bet;
                }

                players[x].amount = players[x].amount + cartridge;
                dealer.amount = dealer.amount - cartridge;



            }

        }

        void RoundClear()
        {
            dealer.DealerClear();
            for (int x = 0; x < players.Count; x++)
            {
                players[x].HandClear();
            }
        }
    }
}
