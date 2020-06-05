using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_P1
{
    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine("Include twins? (Y/y)");
            string answ = Console.ReadLine();
            bool twins = false;
            if (answ == "y" || answ == "Y")
                twins = true;
            int[] testSet = new int[] { 50, 100, 250, 1000 }; //different values of n
            float[] totalAvgList = new float[testSet.Length]; 

            for (int i = 0; i < testSet.Length; i++)
            {
                int[][][] overlaps = BirthdayParadox(testSet[i], twins);
                float sumAvg = 0.0f; 
                float[] avgList = new float[20];
                for (int j = 0; j < 20; j++)
                {
                    float avg = CalculateAvgOverlap(overlaps, j);
                    avgList[j] = avg;
                    
                    Console.ForegroundColor = ConsoleColor.Red; 
                    Console.WriteLine("(for n={0} ) {1}. birthday paradox average overlaps: {2:0.000}", testSet[i], j + 1, avg);
                    Console.ForegroundColor = ConsoleColor.Gray;

                    sumAvg += avg;
                    Console.WriteLine();
                    Console.WriteLine("(for n={0} ) {1}. birthday paradox results:", testSet[i], j + 1);
                    Console.WriteLine();
                    Print(overlaps, j);
                    Console.WriteLine();
                    Console.WriteLine("press any key to continue...");
                    Console.ReadKey(true);
                    Console.WriteLine("----------------------------------------------------------------------------");
                    Console.WriteLine();
                }

                totalAvgList[i] = sumAvg / 20;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("average overlaps test results for n={0} : {1:0.000}", testSet[i], sumAvg / 20);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine("Avg table");
                Console.WriteLine("Tests    N = {0}", testSet[i]);
                Console.WriteLine("------   -------");
                for (int k = 0; k < 20; k++)
                {
                    Console.Write("{0:00}", k + 1);
                    Console.Write("{0, 8}", "");
                    Console.Write("{0:0.000}", avgList[k]);
                    Console.WriteLine();
                }
                Console.WriteLine("press any key to continue...");
                Console.ReadKey(true);
            }
            Console.WriteLine("Total Avg Table");
            Console.WriteLine("Tests     50     100     250     1000");
            Console.WriteLine("------   ----    ----    ----    -----");
            Console.Write("{0:00}", 20);
            Console.Write("{0, 7}", "");
            for (int i = 0; i < testSet.Length; i++)
            {
                Console.Write("{0:0.000}   ", totalAvgList[i]);
            }
            Console.WriteLine();
            Console.WriteLine("press any key to continue...");
            Console.ReadKey(true);
        }

        public static Person[] createPeople(int n, bool twins)
        {
            Person[] prsn = new Person[n];

            for(int i = 0; i < n; i++)
            {
                int year = rnd.Next(1990, 1999); //doesn't matter
                int month = rnd.Next(1, 13);
                int day = rnd.Next(1, DateTime.DaysInMonth(year, month) + 1); //[begin, end)

                Person p = new Person(new DateTime(year, month, day));
                prsn[i] = p;
                if (twins && (i + 1) % 20 == 0 && i + 1 < n && rnd.Next(0, 100) < 50) // every n%20 people in city may be twins 50% chance
                {
                    prsn[i + 1] = p; 
                    i++; 
                }
            }
            return prsn;
        }

        public static int[][][] BirthdayParadox(int n, bool twins = false)
        {
            int[][][] table = new int[20][][];
            for (int i = 0; i < 20; i++)
            {
                table[i] = new int[12][];
                for (int j = 0; j < 12; j++)
                    table[i][j] = new int[DateTime.DaysInMonth(1200, j + 1)]; //1200 leap year

                Person[] p = createPeople(n, twins);
                for (int x = 0; x < n; x++)
                {
                    table[i][p[x].Birthday.Month - 1][p[x].Birthday.Day - 1]++;


                }
            }
            return table;
        }

        public static void Print(int[][][] Overlaps, int tryNum)
        {
            Console.Write("Months ");
            for (int i = 0; i < 31; i++)
                Console.Write("{0:00} ", i + 1);
            Console.WriteLine();

            Console.Write("------ ");
            for (int i = 0; i < 31; i++)
                Console.Write("-- ");
            Console.WriteLine();

            for (int i = 0; i < 12; i++)
            {
                Console.Write("{0:00} ", i + 1);
                Console.Write("{0, 4}", "");
                for (int j = 0; j < 31; j++)
                {
                    if (j >= Overlaps[tryNum][i].Length)
                        Console.Write("{0, 2}", "-");
                    else
                    {
                        if (Overlaps[tryNum][i][j] > 1)
                            Console.ForegroundColor = ConsoleColor.Red;
                        else
                            Console.ForegroundColor = ConsoleColor.DarkGreen;

                        Console.Write("{0, 2} ", Math.Max(0, Overlaps[tryNum][i][j] - 1));

                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                Console.WriteLine();
            }
        }

        public static float CalculateAvgOverlap(int[][][] overlaps, int testNum)
        {
            int sumOverlap = 0;
            for (int i = 0; i < 12; i++)
            {
                foreach (int val in overlaps[testNum][i])
                {
                    if (val > 1)
                        sumOverlap += val - 1;
                }
            }
            return sumOverlap / 365f;
        }
    }
}
