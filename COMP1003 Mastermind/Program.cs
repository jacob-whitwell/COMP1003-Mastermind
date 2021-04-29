using System;
using System.Collections.Generic;

namespace COMP1003_Mastermind
{
    public class Mastermind
    {
        private bool gameActive = false;
        private bool gameQuit = false;

        private int positionChosen = 0;
        private int colourChosen = 0;

        private int userGuess = 0;

        private int[] positionArray = new int[0];
        private int[] secretColourArray = new int[0];

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


            int[] positionArray = new int[positionChosen];
            int[] secretColourArray = new int[colourChosen];

            Console.WriteLine($"\nYou have chosen to play with {positionChosen} positions and {colourChosen} colours.\n");

            for (int i = 0; i < positionArray.Length; i++)
            {
                Random r = new Random();
                positionArray[i] = r.Next(1, 7);
                Console.WriteLine($"Position {i + 1}: {positionArray[i]}");
            }


            Console.WriteLine("The game is now configured. Type \"start\" to start the game.");
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
            Console.WriteLine($"Please input a guess between 1 and {colourChosen}");
            ReadUserInput();
        }

        public void BlackGuess()
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


            // Init the array/list to use
            string[] secretArray = { "1", "2", "3", "3" };
            List<string> secretList = new List<string>();
            List<string> missedGuessList = new List<string>();
            List<string> guessList = new List<string>();
            string secretnums = null;

            // Printing the loop for debug

            for (int i = 0; i < secretArray.Length; i++)
            {
                secretList.Add(secretArray[i]);
                secretnums += secretArray[i] + " ";
            }

            Console.WriteLine($"Secret numbers are: {secretnums}");

            // Init the variables to count
            int blackGuessCount = 0;
            int whiteGuessCount = 0;
            int indexCounter = 0;
            int loopCount = 0;
            int secretLength = secretList.Count;
            string guess = null;

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
                } else
                {
                    // If the number is NOT there, add it to a separate list for processing white guesses later
                    missedGuessList.Add(guess);
                }

                loopCount++;
            }

            if (secretList.Count == 0)
            {
                Console.WriteLine("\nList is now empty");
            }
            else
            { 
                // Printing out the ramining numbers within the list
                //Console.Write($"\nSecret numbers reamining: {secretList.Count}. Remaining: ");
                for (int i = 0; i < secretList.Count; i++)
                {
                    //Console.Write(secretList[i] + " ");
                }
            }

            /*Console.Write("\nMissed guesses list: ");
            for (int i = 0; i < missedGuessList.Count; i++)
            {
                Console.Write($"{missedGuessList[i]} ");
            }*/

            for (int i = 0; i < secretList.Count; i++)
            {
                if (missedGuessList.Contains(secretList[i]))
                {
                    missedGuessList.Remove(secretList[i]);
                    //secretList.Remove(secretList[i]);
                    whiteGuessCount++;
                }
            }


            Console.WriteLine($"\nBlack guesses: {blackGuessCount}");
            Console.WriteLine($"White guesses: {whiteGuessCount}");














            /*while (indexCounter < secretLength && secretList.Count != 0)
            {
                // Console.WriteLine("\nChecking index: " + indexCounter);
                if (secretList.Count == 0 || indexCounter >= secretList.Count)
                {
                    break;
                }

                string guess = null;

                Console.Write("User guess:");
                
                guess = Console.ReadLine();
                guessList.Add(guess);

                Console.WriteLine("index: " + indexCounter);

                while (loopCount < secretList.Count)
                {
                    // If guess is 1, and secretList[0] == 1, add to black guess and then remove the list item
                    if (guess == secretList[indexCounter])
                    {
                        blackGuessCount++;
                        //secretList.RemoveAt(indexCounter);
                        // Continue so that the index doesn't go up after a correct guess and removing the index
                        Console.WriteLine();
                        loopCount++;
                        continue;
                    }
                    loopCount++;
                }
                

                while (loopCount < secretList.Count)
                {
                    if (secretList.Contains(guess))
                    {
                        secretList.Remove(guess);
                        whiteGuessCount++;
                    }
                    loopCount++;
                }


                indexCounter++;

                *//*for (int i = 0; i < secretList.Count; i++)
                {
                    if (secretList.Contains(guess))
                    {
                        secretList.Remove(guess);
                    }
                    Console.WriteLine($"\nThe items left in the list are: {secretList[i]} - index ({i})");
                }*//*
            }

            Console.WriteLine($"Secret numbers are:\n{secretnums}");

            Console.WriteLine("Guessed this turn:");
            for(int i = 0; i < guessList.Count; i++)
            {
                Console.Write(guessList[i] + " ");

            }

            Console.WriteLine("\nUnguessed Secret List Remaining:");
            for (int i = 0; i < secretList.Count; i++)
            {
                Console.Write(secretList[i] + " ");
            }

            Console.WriteLine("\nBlack: " + blackGuessCount);
            Console.WriteLine("White: " + whiteGuessCount);*/

        }
    }
}