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
        public int[] RollDice(int NrOfDice)
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

        public int[] DicesToKeep(int[] Dices)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Vilka tärningar vill du behålla? - Tryck enter om du vill behålla alla");
            Console.WriteLine("-----------------------------");
            string userinput = Console.ReadLine();
            int k = 0;
            int[] ChooseDice = new int[userinput.Length];
            while (k < userinput.Length)
            {
                int ChooseDicePick = Convert.ToInt32(userinput.Substring(k, 1));
                ChooseDice[k] = ChooseDicePick;
                //Console.WriteLine("--------------------");
                //Console.WriteLine(insatsRone[k]);
                //Console.WriteLine("--------------------");
                k++;
            }

            int varre = 0;
            int[] DiceKept = new int[ChooseDice.Length];

            while (varre < (ChooseDice.Length))
            {
                int y = (int)ChooseDice.GetValue(varre) - 1;
                DiceKept[varre] = (int)Dices.GetValue(y);
                varre++;
            }
            return DiceKept; }

        public int Points(List<int> die)
        {
            List<int> totalSum = new List<int>();
            int sumOfPoints = 0;
            while (true)
            {
                int[] diepoints = die.ToArray();
                Console.WriteLine("Vilka tärningar vill du kombinera? - Tryck K när du är klar ");
                string userinputpoints = Console.ReadLine();
                
                if (userinputpoints == "k"|| userinputpoints ==  "K")
                    break;

                int k = 0;
                int[] keep = new int[userinputpoints.Length];
                while (k < userinputpoints.Length)
                {
                    int keepPick = Convert.ToInt32(userinputpoints.Substring(k, 1));
                    keep[k] = keepPick;
                    //Console.WriteLine("--------------------");
                    //Console.WriteLine(insatsRone[k]);
                    //Console.WriteLine("--------------------");
                    k++;
                }

                int count = 0;
                int[] points = new int[userinputpoints.Length];

                while (count < keep.Length)
                {
                    int y = (int)keep.GetValue(count) - 1;
                    points[count] = (int)diepoints.GetValue(y);
                    count++;
                }

                int pointsSum = points.Sum();
                totalSum.Add(pointsSum);
                sumOfPoints = totalSum.AsQueryable().Sum();
            }
            return sumOfPoints;
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
            int round = 0;
            int[] totalPoints = new int[10];
            //int pointsSum = 0;
            while (round<10)
            {
                summering.Clear();
                round++;
                while (true)
                {
                    if (round == 1)                    
                    Console.WriteLine("---------------------Välkommen till Thirty--------------------------");
                                       
                    else                                    
                    Console.WriteLine("---------------------Omgång {0} av 10-------------------------------", round); 
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine("Slag 1 - tryck enter för att påbörja");
                    Console.WriteLine("---------------------------------------");
                    Console.ReadKey();

                // Skapar ett objekt av klassen game
                Game rOne = new Game();
                int NrOfDice = 6;
                // Slår ett tärningsslag
                int[] roundOne = rOne.RollDice(NrOfDice);
                // Bestämmer vilka tärningar som skall behållas
                int[] DiceKeptOne = rOne.DicesToKeep(roundOne);

                // Lägger till de tärningar som behölls i en lista som sedan används för att summera poängen i slutet av omgången
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
                int[] roundTwo = rTwo.RollDice(NrOfDiceTwo);
                int[] DiceKeptTwo = rTwo.DicesToKeep(roundTwo);

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
                int[] roundThree = rThree.RollDice(NrOfDiceThree);
                
                // Notera att man, vid det sista slaget, inte får välja vilka tärningar man vill behålla

                foreach (var j in roundThree)
                { summering.Add(j); }

                break;            

            }
            Console.WriteLine("------------Omgången är över--------------");
            Console.WriteLine("Dina tärningar blev");
            summering.ForEach(Console.WriteLine);
           
            // Skapar ett objekt av klassen game
            Game pointsGet = new Game();
            
            // Räknar ut totalsumman genom att använda metoden points i klassen game
            int points = pointsGet.Points(summering);
            Console.WriteLine("Din totalsumma i denna omgång blev:");
            Console.WriteLine(points);
            Console.WriteLine("-----------------------------------");
            
            totalPoints[round-1] = points;
            int totalPointsSum = totalPoints.AsQueryable().Sum();
            Console.WriteLine("Din poäng efter {0} omgångar är: {1} - tryck enter för att starta nästa omgång", round, totalPointsSum);
            Console.ReadKey();
            Console.Clear();
            
            }
            
        }
    }
}

// KOD SOM INTE ANVÄNDS
//Console.WriteLine("-----------------------------");
//Console.WriteLine("Vilka tärningar vill du behålla? - Tryck enter om du vill behålla alla");
//Console.WriteLine("-----------------------------");
//string userinputRone = Console.ReadLine();
//int k = 0;
//int[] insatsRone = new int[userinputRone.Length];
//while (k < userinputRone.Length)
//{
//    int insatsRonePick = Convert.ToInt32(userinputRone.Substring(k, 1));
//    insatsRone[k] = insatsRonePick;
//    //Console.WriteLine("--------------------");
//    //Console.WriteLine(insatsRone[k]);
//    //Console.WriteLine("--------------------");
//    k++;
//}

//int varre = 0;
//int[] DiceKeptOne = new int[insatsRone.Length];

//while (varre < (insatsRone.Length))
//{ 
//  int y =(int)insatsRone.GetValue(varre)-1;
//    DiceKeptOne[varre]=(int)roundOne.GetValue(y);
//  varre++;
//}

//Console.WriteLine("-----------------------------");
//Console.WriteLine("Vilka tärningar vill du behålla? - Tryck enter om du vill behålla alla");
//Console.WriteLine("-----------------------------");
//string userinputRtwo = Console.ReadLine();
//int m = 0;
//int[] insatsTwo = new int[userinputRtwo.Length];
//while (m < userinputRtwo.Length)
//{
//    int insatsen = Convert.ToInt32(userinputRtwo.Substring(m, 1));
//    insatsTwo[m] = insatsen;
//    //Console.WriteLine("--------------------");
//    //Console.WriteLine(insatsTwo[m]);
//    //Console.WriteLine("--------------------");
//    m++;
//}
//int varre2 = 0;
//int[] DiceKeptTwo = new int[insatsTwo.Length];

//while (varre2 < (insatsTwo.Length))
//{
//    int y = (int)insatsTwo.GetValue(varre2) - 1;
//    DiceKeptTwo[varre2] = (int)roundTwo.GetValue(y);
//    varre2++;
//}