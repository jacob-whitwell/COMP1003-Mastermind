using System;
using System.Collections.Generic;

namespace COMP1003_Mastermind
{
    public class Mastermind
    {
        private bool gameActive = false;
        private bool gameQuit = false;

        private int blackGuessCount = 0;
        private int whiteGuessCount = 0;
        private int loopCount = 0;
        string guess = null;

        private int positionChosen = 0;
        private int colourChosen = 0;

        private int[] positionArray = new int[0];
        //private string[] secretArray = new string[0];

        List<string> secretList = new List<string>();
        List<string> missedGuessList = new List<string>();
        List<string> guessList = new List<string>();

        public void GameSetup()
        {

            // Player chooses how many positions to play between 1 and 9
            while (positionChosen <= 0 || positionChosen > 9)
            {
                Console.Write("Choose how many positions to play with: ");
                try
                {
                    positionChosen = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                }

                if (positionChosen <= 0 || positionChosen > 9)
                {
                    Console.WriteLine("Invalid number. Please choose a number between 1 and 9.\n");
                }
            }

            // Player chooses how many colours to play with between 1 and 6
            while (colourChosen <= 0 || colourChosen > 6)
            {
                Console.Write("Choose how many colours to play with: ");
                try
                {
                    colourChosen = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                }

                if (colourChosen <= 0 || colourChosen > 6)
                {
                    Console.WriteLine("Invalid number. Please choose a number between 1 and 6.\n");
                }
            }

            string[] positionArray = new string[positionChosen];

            Console.WriteLine($"\nYou have chosen to play with {positionChosen} positions and {colourChosen} colours.\n");

            for (int i = 0; i < positionArray.Length; i++)
            {
                Random r = new Random();
                positionArray[i] = r.Next(1, colourChosen + 1).ToString();
                Console.WriteLine($"Position {i + 1}: {positionArray[i]}");
            }

            for (int i = 0; i < positionArray.Length; i++)
            {
                //Console.WriteLine(positionArray[i]);
                secretList.Add(positionArray[i].ToString());
                //secretnums += positionArray[i] + " ";
            }

            ReadUserInput();
        }

        public string ReadUserInput()
        {
            string userInput = null;

            while (gameActive == false && gameQuit == false)
            {
                Console.WriteLine("Please input \"start\" or \"quit\".");
                userInput = Console.ReadLine();
                if (userInput == "start")
                {
                    gameActive = true;
                    return userInput;
                }

                if (userInput == "quit")
                {
                    Console.WriteLine("Thanks for playing!");
                    gameQuit = true;
                    return userInput;
                }
            }

            userInput = Console.ReadLine();
            return userInput;
        }

        public void GameLoop()
        {
            blackGuessCount = 0;
            whiteGuessCount = 0;
            // Init the array/list to use
            string secretnums = null;
            //int secretLength = secretList.Count;
            int loopCount = 0;
            // Printing the loop for debug
            // Init the variables to count


            while (loopCount < secretList.Count)
            {
                Console.Write($"Comparing: {secretList[loopCount]} with user guess: ");
                guess = Console.ReadLine();

                // If the guess is correct (black), add it to the counter and then remove it from the secret list.
                // This makes it so that you can't guess the same number multiple times.
                if (guess == secretList[loopCount])
                {
                    blackGuessCount++;
                    secretList.Remove(secretList[loopCount]);
                    continue;
                }
                else
                {
                    // If the number is NOT there, add it to a separate list for processing white guesses later
                    missedGuessList.Add(guess);
                }

                loopCount++;
            }

            for (int i = 0; i < secretList.Count; i++)
            {
                if (missedGuessList.Contains(secretList[i]))
                {
                    missedGuessList.Remove(secretList[i]);
                    //secretList.Remove(secretList[i]);
                    whiteGuessCount++;
                }
            }

            if (secretList.Count == 0)
            {
                Console.WriteLine("\nList is now empty");
                gameActive = false;
            }

            Console.WriteLine($"\nBlack guesses: {blackGuessCount}");
            Console.WriteLine($"White guesses: {whiteGuessCount}");
        }

        private void AssignLists()
        {

        }

        static void Main(string[] args)
        {
            Mastermind mastermind = new Mastermind();
            mastermind.GameSetup();

            while (mastermind.gameActive && mastermind.gameQuit == false)
            {
                mastermind.GameLoop();
            }

        }
    }
}