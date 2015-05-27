using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sazonova.NsuDotNetCourse.NumberGuess
{
    class NumberGuess
    {
        static int MinValue = 0;
        static int MaxValue = 100;
        static string Quit = "q";
        static string Proposal;
        enum Comparation
        { 
            Less,
            Larger,
            Nearly
        }
        static int HistoryLength = 1000;
        static string[] Insults = 
        { 
            "Faugh.",
            "Are you so stupid?",
            "You are silly...",
            "Who is the Loser? YOU."
        };
        static string SorryMessage = "I'm sorry. Bye-bye!";
        static string AttemptsMessage = "Number of attempts: ";
        static string LoserMessage = "You are looser, {0}";
        static string TimeMessage = "Time is {0} min.";

        static void Main(string[] args)
        {
            Console.WriteLine("Enter user name please: ");
            string userName = Console.ReadLine();
            Proposal = String.Format("Try to guess number from {0} to {1}", MinValue,  MaxValue);
          
            string answer = "";
            Random rand = new Random((int)DateTime.Now.ToBinary());
            int count = 0;
            Comparation[] history = new Comparation[HistoryLength];
            int failCount = 0;
            DateTime start = DateTime.Now;

            while (true) 
            {
                int number = rand.Next(MinValue, MaxValue);

                Console.WriteLine(Proposal);
                answer = Console.ReadLine();

                if (Quit.CompareTo(answer) == 0)
                {
                    Console.WriteLine(SorryMessage);
                    break;
                }

                int guess = int.Parse(answer);
                if (guess == number)
                {
                    history[count] = Comparation.Nearly;

                    Console.WriteLine(String.Concat(AttemptsMessage, count + 1));
                    for (int i = 0; i < count; ++i)
                    {
                        Console.Write("{0}: ", i);
                        switch (history[i]) 
                        {
                            case Comparation.Larger:
                                Console.WriteLine(">");
                                break;
                            case Comparation.Less:
                                Console.WriteLine("<");
                                break;
                            case Comparation.Nearly:
                                Console.WriteLine("=");
                                break;
                        }
                    }
                    TimeSpan time = DateTime.Now - start;

                    Console.WriteLine(String.Format(TimeMessage, time.Minutes));
                }
                else 
                {
                    failCount++;
                    if (number > guess)
                    {
                        history[count] = Comparation.Less;
                    }
                    else
                    {
                        history[count] = Comparation.Larger;
                    }
                    if (failCount == 4) 
                    {
                        failCount = 0;
                        Console.WriteLine(String.Format(LoserMessage, userName));
                        Console.WriteLine(Insults[rand.Next(Insults.Length)]);
                    }
                }
                count++;
            }

        }
    }
}
