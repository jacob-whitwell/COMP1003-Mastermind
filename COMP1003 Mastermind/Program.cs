using System;
using System.Collections.Generic;

namespace COMP1003_Mastermind
{
    public class Mastermind
    {
        private bool gameActive = false;
        private bool gameQuit = false;
        private bool gameRestarted = false;

        private int blackGuessCount = 0;
        private int whiteGuessCount = 0;
        private int loopCount = 0;
        string guess = null;

        private int positionChosen = 0;
        private int colourChosen = 0;

        private int[] positionArray = new int[0];

        List<string> secretList = new List<string>();
        List<string> resetList = new List<string>();
        List<string> missedGuessList = new List<string>();
        List<string> guessList = new List<string>();

        public void GameSetup()
        {
            // Player chooses how many positions to play between 1 and 9.
            // Using ints makes this easier to ensure correct input is given
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

            // Assign the position array with random values that have been chosen for colourChosen.
            for (int i = 0; i < positionArray.Length; i++)
            {
                Random r = new Random();
                positionArray[i] = r.Next(1, colourChosen + 1).ToString();
                //Console.WriteLine($"Position {i + 1}: {positionArray[i]}");
            }

            // Fill the lists with the values: secretList is used within the game, resetList is used to restore values if guess unsuccessful.
            for (int i = 0; i < positionArray.Length; i++)
            {
                secretList.Add(positionArray[i].ToString());
                resetList.Add(positionArray[i].ToString());
            }

            ReadUserInput();
            Console.Clear();
            try
            {
                Console.SetCursorPosition(0, 0);
            }
            catch { }
        }

        // Used to process the users input
        public string ReadUserInput()
        {
            string userInput = null;

            while (gameActive == false && gameQuit == false || gameRestarted)
            {
                Console.WriteLine("Please input \"start\" or \"quit\".");
                userInput = Console.ReadLine();
                if (userInput == "start")
                {
                    gameActive = true;
                    gameRestarted = false;
                    return userInput;
                }

                // When user inputs quit, the game will finish.
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
            int positionGuessCount = 1;
            blackGuessCount = 0;
            whiteGuessCount = 0;
            int loopCount = 0;
            string guessString = null;

            while (loopCount < secretList.Count)
            {
                //Console.Write($"Comparing: {secretList[loopCount]} with user guess: ");
                //Console.Write($"Guess for position {positionGuessCount}: ");
                if (loopCount == 0)
                {
                    Console.Write("Please enter your guess: ");
                }
                guess = Console.ReadKey().KeyChar.ToString();
                Console.Write(" ");
                positionGuessCount++;
                guessString += guess + " ";
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
                    // If the number is NOT there, add it to a separate list for processing white guesses later.
                    missedGuessList.Add(guess);
                }

                

                // Increasing the loop counter allows the game to check the next number in the array to check if it's a black guess.
                loopCount++;
            }
            Console.WriteLine();

            // If the guess is in the list but in the wrong position, add to the white counter and remove from the list.
            // Removing from the list allows the number to be counted only once.
            for (int i = 0; i < secretList.Count; i++)
            {
                if (missedGuessList.Contains(secretList[i]))
                {
                    missedGuessList.Remove(secretList[i]);
                    whiteGuessCount++;
                }
            }
            guessList.Add($"{guessString} Black: {blackGuessCount}. White: {whiteGuessCount}");

            for (int i = 0; i < guessList.Count; i++)
            {
                Console.WriteLine($"Guess {i + 1} is: {guessList[i]}.");
            }
            Console.WriteLine();
            // Display the guesses to the user.
            /* Console.WriteLine($"\nBlack guesses: {blackGuessCount}");
             Console.WriteLine($"White guesses: {whiteGuessCount}");*/

            // If the user hasn't emptied the list by correctly guessing
            // Reset the list and then add the values again to create a full list
            if (secretList.Count != 0)
            {
                secretList.Clear();
                for (int i = 0; i < resetList.Count; i++)
                {
                    missedGuessList.Clear();
                    secretList.Add(resetList[i]);
                }
                loopCount = 0;
            }

            // Win condition when the list is empty
            if (secretList.Count == 0)
            {
                Console.WriteLine("Congratulations, you win!");
                Console.WriteLine("Please enter \"y\" to restart or \"n\" to quit.");
                string restart = Console.ReadLine();
                
                if (restart == "y")
                {
                    gameRestarted = true;
                    GameRestart();
                }
                
                gameActive = false;
            }
        }

        // Resets the game variables so that the user can start again.
        private void GameRestart()
        {
            colourChosen = 0;
            positionChosen = 0;
            missedGuessList.Clear();
            whiteGuessCount = 0;
            blackGuessCount = 0;
            GameSetup();
            GameLoop();
        }

        static void Main(string[] args)
        {
            Mastermind mastermind = new Mastermind();
            mastermind.GameSetup();


            while (mastermind.gameActive && mastermind.gameQuit == false)
            {
                mastermind.GameLoop();
            }

            Console.WriteLine("Thank you for playing Mastermind. See you again next time!");

        }
    }
}