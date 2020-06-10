using System;
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
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Vilka tärningar vill du behålla?");
            string userinput13 = Console.ReadLine();
            int k = 0;
            while (k< userinput13.Length)
            {
                int[] insats2 = new int[userinput13.Length];
                int insats3= Convert.ToInt32(userinput13.Substring(k,1));
                insats2[k] = insats3;
               Console.WriteLine("--------------------");
                Console.WriteLine(insats2[k]);
               Console.WriteLine("--------------------");
                k++;
            }
                       
            int hejsan = userinput13.Length;
            Console.WriteLine("Antal bokstäver; {0}", hejsan);

            
            Console.WriteLine("Hur många tärningar vill du slå?");
            string userinput = Console.ReadLine();
            int  f = Convert.ToInt32(userinput);
            
            Randomizing rarre = new Randomizing();
            
            int[] rirre;
            rirre = rarre.RandomNumberSpelaren(f);
            foreach (var item in rirre)
            {
                Console.WriteLine("------------------");
                Console.WriteLine("Dice: {0}",item);
                Console.WriteLine("------------------");
            }
            Console.ReadKey();

        }
    }
}
