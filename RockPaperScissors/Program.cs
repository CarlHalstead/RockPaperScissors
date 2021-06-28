using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RockPaperScissors
{
	public class Program
	{
		private readonly static TextInfo culture = new CultureInfo("en-GB").TextInfo;

		private static readonly Random random = new Random();

		private static void Main(string[] args)
		{
			List<string> availableMovesNames = new List<string>(RockPaperScissors.moves.Keys);

			int totalRounds = 1;

			/*
			 * Allow the player to keep playing after each match until they exit the application
			 */ 
			while (true)
			{
				Console.Clear();

				Console.WriteLine("--- Rock Paper Scissors! ---");
				Console.WriteLine("Please select 1 or 2");
				Console.WriteLine("1) Versus Robot");
				Console.WriteLine("2) Versus Human");
				Console.WriteLine();

				string inputOpponent = GetInput(new[]{ "1", "2" });
				bool isHumanOpponent = (inputOpponent == "2");

				Console.WriteLine();

				Console.Write("Available Moves: ");
				Console.WriteLine(string.Join(", ", availableMovesNames));

				Console.WriteLine();

				/*
				 * The players will keep battling until there is a victor. Draws do not end the match
				 */ 
				bool didDraw = true;
				while (didDraw == true)
				{
					Console.WriteLine($"Round {totalRounds} start!");
					Console.WriteLine("Your move will be hidden until both players have selected!");
					Console.WriteLine();

					Console.Write("Player 1 move: ");
					string inputPlayerOne = GetHiddenInput(availableMovesNames);

					Console.Write("Player 2 move: ");
					string inputPlayerTwo;

					if (isHumanOpponent == true)
						inputPlayerTwo = GetHiddenInput(availableMovesNames);
					else
						inputPlayerTwo = availableMovesNames[random.Next(0, availableMovesNames.Count)];

					Console.WriteLine();

					Console.WriteLine($"Player 1 chose: {culture.ToTitleCase(inputPlayerOne)}");
					Console.WriteLine($"Player 2 chose: {culture.ToTitleCase(inputPlayerTwo)}");

					RockPaperScissors.moveUses[inputPlayerOne] += 1;
					RockPaperScissors.moveUses[inputPlayerTwo] += 1;

					if (inputPlayerOne.Equals(inputPlayerTwo, StringComparison.OrdinalIgnoreCase))
					{
						Console.WriteLine();
						Console.WriteLine("Round ended in a draw! Play again!");
						Console.WriteLine();

						didDraw = true;
						totalRounds += 1;
					}
					else
					{
						Console.WriteLine($"Player {RockPaperScissors.CalculateWinner(inputPlayerOne, inputPlayerTwo)} wins!");

						didDraw = false;

						KeyValuePair<string, int> mostUsedMove = new KeyValuePair<string, int>();

						foreach (KeyValuePair<string, int> kvp in RockPaperScissors.moveUses)
						{
							if (kvp.Value > mostUsedMove.Value)
								mostUsedMove = kvp;
						}

						Console.WriteLine();
						Console.WriteLine($"Match ended in {totalRounds} round(s)!");
						Console.WriteLine($"Most used move: {mostUsedMove.Key} used {mostUsedMove.Value} times");
						Console.WriteLine("Press any button to play again!");
					}

					Console.ReadKey();
				}

				RockPaperScissors.ClearMoveUses();
			}
		}

		/// <summary>
		/// Set the text colour to that of the background to hide the user input (So the other player cannot see what was played)
		/// Input must match one of the allowed inputs given to the method, case insensisitive
		/// </summary>
		/// <param name="allowedInputs"></param>
		/// <returns></returns>
		private static string GetHiddenInput(IEnumerable<string> allowedInputs) 
		{
			Console.ForegroundColor = Console.BackgroundColor;

			string input = GetInput(allowedInputs);

			Console.ForegroundColor = ConsoleColor.Gray;
			return input;
		}

		private static string GetInput(IEnumerable<string> allowedInputs) 
		{
			string input = Console.ReadLine();

			while (allowedInputs.Contains(input, StringComparer.OrdinalIgnoreCase) == false)
			{
				Console.Write($"Pick An Option ({string.Join(", ", allowedInputs)}): ");
				input = Console.ReadLine();
			}

			return input;
		}
	}
}
