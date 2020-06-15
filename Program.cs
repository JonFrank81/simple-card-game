using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCardGame
{
    class Randomizing
    {


        //kod som skapar ett randomiserat värde.
        public static readonly Random getrandom = new Random();
        public int[] RandomNumberSpelaren(int numberOfDice)
        {
            int[] rolls = new int[numberOfDice];
            int diceNumber = 0;
            while (diceNumber < numberOfDice)
            {
                lock (getrandom)
                {
                    int hej = getrandom.Next(1, 6);
                    rolls[diceNumber] = hej;
                }
                diceNumber++;
            }
            return rolls;

        }

        public Randomizing()
        {
        }

    }

    class Game
    {
        // Metod som slår tärningarna
        public int[] Round(int NrOfDice)
        {

            Randomizing slump = new Randomizing();
            int[] slag;
            slag = slump.RandomNumberSpelaren(NrOfDice);
            int dieNr = 0;
            foreach (var item in slag)
            {
                dieNr++;
                Console.WriteLine("------------------");
                Console.WriteLine("Dice {0}: {1}", dieNr, item);
                Console.WriteLine("------------------");
            }
            return slag;

        }
        public Game()
        {
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            List<int> summering = new List<int>();

            while (true)
            {
                
                // Första omgången
                Console.WriteLine("Välkommen till Thirty, tryck enter för att starta den första rundan:");
                Console.ReadKey();
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Slag 1 - tryck enter för att påbörja");
                Console.WriteLine("---------------------------------------");
                Console.ReadKey();

                Game rOne = new Game();
                int NrOfDice = 6;
                int[] roundOne = rOne.Round(NrOfDice);

                Console.WriteLine("-----------------------------");
                Console.WriteLine("Vilka tärningar vill du behålla?");
                Console.WriteLine("-----------------------------");
                string userinputRone = Console.ReadLine();
                int k = 0;
                int[] insatsRone = new int[userinputRone.Length];
                while (k < userinputRone.Length)
                {
                    int insatsRonePick = Convert.ToInt32(userinputRone.Substring(k, 1));
                    insatsRone[k] = insatsRonePick;
                    //Console.WriteLine("--------------------");
                    //Console.WriteLine(insatsRone[k]);
                    //Console.WriteLine("--------------------");
                    k++;
                }

                int varre = 0;
                int[] DiceKeptOne = new int[insatsRone.Length];

                while (varre < (insatsRone.Length))
                { 
                  int y =(int)insatsRone.GetValue(varre)-1;
                    DiceKeptOne[varre]=(int)roundOne.GetValue(y);
                  varre++;
                }

                foreach (var j in DiceKeptOne)
                { summering.Add(j); }

                // Testar om det finns några tärningar kvar att slå
                if (NrOfDice == DiceKeptOne.Length)
                    break;

              
                // Eventuell andra omgången
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Slag 2 - {0} tärningar - tryck enter för att påbörja",6- DiceKeptOne.Length);
                Console.WriteLine("---------------------------------------");
                Console.ReadKey();

                Game rTwo = new Game();
                int NrOfDiceTwo = 6 - DiceKeptOne.Length;
                int[] roundTwo = rTwo.Round(NrOfDiceTwo);

                Console.WriteLine("-----------------------------");
                Console.WriteLine("Vilka tärningar vill du behålla?");
                Console.WriteLine("-----------------------------");
                string userinputRtwo = Console.ReadLine();
                int m = 0;
                int[] insatsTwo = new int[userinputRtwo.Length];
                while (m < userinputRtwo.Length)
                {
                    int insatsen = Convert.ToInt32(userinputRtwo.Substring(m, 1));
                    insatsTwo[m] = insatsen;
                    //Console.WriteLine("--------------------");
                    //Console.WriteLine(insatsTwo[m]);
                    //Console.WriteLine("--------------------");
                    m++;
                }
                int varre2 = 0;
                int[] DiceKeptTwo = new int[insatsTwo.Length];

                while (varre2 < (insatsTwo.Length))
                {
                    int y = (int)insatsTwo.GetValue(varre2) - 1;
                    DiceKeptTwo[varre2] = (int)roundTwo.GetValue(y);
                    varre2++;
                }

                foreach (var j in DiceKeptTwo)
                { summering.Add(j); }

                // Testar om det finns några tärningar kvar att slå
                if (NrOfDiceTwo == DiceKeptTwo.Length)
                    break;                            

                // Eventuell tredje omgång
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Slag 3 - {0} tärningar - tryck enter för att påbörja", NrOfDiceTwo - DiceKeptTwo.Length);
                Console.WriteLine("---------------------------------------");
                Console.ReadKey();

                Game rThree = new Game();
                int NrOfDiceThree = NrOfDiceTwo - DiceKeptTwo.Length;
                int[] roundThree = rThree.Round(NrOfDiceThree);                            

                foreach (var j in roundThree)
                { summering.Add(j); }

                break;            

            }
            Console.WriteLine("------------Spelet är över--------------");
            Console.WriteLine("Dina tärningar blev");
            summering.ForEach(Console.WriteLine);
            
            Console.ReadKey();

        }
    }
}

            