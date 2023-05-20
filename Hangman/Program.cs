using System;
using System.Linq;
using System.Collections.Generic;

namespace Hangman
{
    class Program
    {
        int life = 6;
        string name = " ";
        string hint = " ";
        List<char> used = new List<char>();
        string[] response = { "yes", "no", "y", "n" };

        public void Display()
        {
            Console.WriteLine("Player Name: {1}\tPlayer Lives: {0} \nHint: {2}", life, name, hint);
            Console.WriteLine("Used Letters: ");

            for (int i = 0; i < used.Count(); i++)
            {
                Console.Write(used.ToArray()[i] + " ");
            }
            Console.Write("\n\n\n");
        }

        public void Wait()
        {
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }

        public bool IsAlphabetic(string value)
        {
            foreach(char ch in value)
            {
                if (!char.IsLetter(ch))
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsUsed(char a)
        {
            foreach(char ch in used)
            {
                if((char)ch == a)
                {
                    return true;
                }
            }
            return false;
        }

        static void Main(string[] args)
        { 
            Program p = new Program();
            string a;
            char input;
            bool correct = false;
            bool loop = true;

            Console.WriteLine("Welcome to Hangman");
            p.Wait();

            Console.Clear();
            Console.WriteLine("Player Name: ");
            p.name = Console.ReadLine();

            

            do
            {
                p.used.Clear();
                p.hint = " ";
                p.life = 6;
                Console.Clear();
                Console.WriteLine("Enter the word:\t");
                a = Console.ReadLine();
                char[] word = a.ToUpper().ToCharArray();
                //ask for hint
                Console.WriteLine("\nEnter hint:\t");
                p.hint = Console.ReadLine();

                //blanks display
                char[] display = new char[word.Length];
                for (int i = 0; i < display.Length; i++)
                {
                    display[i] = '_';
                }

                //input guess
                while (p.life != 0)
                {
                    do
                    {
                        
                        do
                        {
                            Console.Clear();
                            p.Display();
                            foreach (char ch in display)
                            {
                                Console.Write(ch + " ");
                            }
                            Console.Write("\n\nGuess: \t");
                            a = Console.ReadLine();
                        } while (!(a.Length == 1 && p.IsAlphabetic(a)));
                        input = Convert.ToChar(a.ToUpper());
                    } while (p.IsUsed(input));


                    Console.Clear();



                    p.used.Add(input);

                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == input)
                        {
                            display[i] = input;
                            correct = true;
                        }
                    }



                    //loss
                    if (correct == false)
                    {
                        p.life = p.life - 1;
                    }

                    correct = false;

                    //win
                    if (!display.Contains('_'))
                    {
                        break;
                    }

                }
                p.Display();
                foreach (char ch in display)
                {
                    Console.Write(ch + " ");
                }
                p.Wait();
                Console.Clear();
                if (p.life == 0)
                {
                    Console.Write("Sorry, you lose. \n\nThe correct word was ");
                    foreach(char ch in word)
                    {
                        Console.Write(ch);
                    }
                }
                else
                {
                    Console.WriteLine("Congrats! You won with {0} lives remaining", p.life);
                }
                p.Wait();
                
                do
                {
                    Console.Clear();
                    Console.WriteLine("Do you want to play again?(Y/N)\t");
                    a = Console.ReadLine();
                } while (!p.response.Contains(a));
                if (a.ToLower() == "n")
                {
                    Console.Clear();
                    Console.WriteLine("Thank you for playing!");
                    loop = false;
                }
                
            } while (loop);
            
        }
    }
}
