using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors
{
	public class Program
	{
		private enum Move
		{
			Rock,
			Paper,
			Scissors,
			Lizard,
			Spock
		}

		private static readonly Dictionary<Move, Move[]> moves = new Dictionary<Move, Move[]>() 
		{
			{ 
				Move.Rock, new []
				{ 
					Move.Scissors,
					Move.Lizard
				}
			},
			{
				Move.Paper, new []
				{
					Move.Rock,
					Move.Spock
				}
			},
			{
				Move.Scissors, new []
				{
					Move.Paper,
					Move.Lizard
				}
			},
			{
				Move.Lizard, new []
				{
					Move.Paper,
					Move.Spock
				}
			},
			{
				Move.Spock, new []
				{
					Move.Rock,
					Move.Scissors
				}
			}
		};

		private static readonly Random random = new Random();

		private static void Main(string[] args)
		{
			Move[] availableMoves = Enum.GetValues<Move>();
			string[] availableMovesNames = Enum.GetNames<Move>();

			int totalRounds = 1;

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

				bool didDraw = true;
				while (didDraw == true)
				{
					Console.WriteLine($"Round {totalRounds} start!");
					Console.WriteLine("Your move will be hidden until both players have selected!");
					Console.Write("Player 1 move: ");
					string inputPlayerOne = GetHiddenInput(availableMovesNames);

					Move playerOneMove = Enum.Parse<Move>(inputPlayerOne, true);
					Move playerTwoMove;

					if (isHumanOpponent == true)
					{
						Console.Write("Player 2 move: ");
						string inputPlayerTwo = GetHiddenInput(availableMovesNames);

						playerTwoMove = Enum.Parse<Move>(inputPlayerTwo, true);
					}
					else
					{
						playerTwoMove = availableMoves[random.Next(0, availableMoves.Length)];
					}

					Console.WriteLine();

					Console.WriteLine($"Player 1 chose: {playerOneMove.ToString()}");
					Console.WriteLine($"Player 2 chose: {playerTwoMove.ToString()}");

					if (playerOneMove == playerTwoMove)
					{
						Console.WriteLine();
						Console.WriteLine("Round ended in a draw! Play again!");
						Console.WriteLine();

						didDraw = true;
						totalRounds += 1;

					}
					else
					{
						if (moves[playerOneMove].Contains(playerTwoMove))
						{
							Console.WriteLine("Player 1 Wins!");
						}
						else if (moves[playerTwoMove].Contains(playerOneMove))
						{
							Console.WriteLine("Player 2 Wins!");
						}

						didDraw = false;

						Console.WriteLine();
						Console.WriteLine($"Match ended in {totalRounds} round(s)!");
						Console.WriteLine("Press any button to play again!");
					}

					Console.ReadKey();
				}
			}
		}

		/// <summary>
		/// Set the text colour to that of the background to hide the user input (So the other player cannot see what was played)
		/// Input must match one of the allowed inputs given to the method, case insensisitive
		/// </summary>
		/// <param name="allowedInputs"></param>
		/// <returns></returns>
		private static string GetHiddenInput(string[] allowedInputs) 
		{
			Console.ForegroundColor = Console.BackgroundColor;

			string input = GetInput(allowedInputs);

			Console.ForegroundColor = ConsoleColor.Gray;
			return input;
		}

		private static string GetInput(string[] allowedInputs) 
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
