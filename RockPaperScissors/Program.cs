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
			Scissors
		}

		private static readonly Dictionary<Move, List<Move>> moves = new Dictionary<Move, List<Move>>() 
		{
			{ 
				Move.Rock, new List<Move>()
				{ 
					Move.Scissors 
				}
			},
			{
				Move.Paper, new List<Move>()
				{
					Move.Rock
				}
			},
			{
				Move.Scissors, new List<Move>()
				{
					Move.Paper
				}
			},
		};

		private static readonly Random random = new Random();

		private static void Main(string[] args)
		{
			Move[] availableMoves = Enum.GetValues<Move>();
			string[] availableMovesNames = Enum.GetNames<Move>();

			while (true)
			{
				Console.Clear();

				Console.Write("Available Moves: ");
				Console.WriteLine(string.Join(", ", availableMovesNames));

				Console.WriteLine();

				Console.Write("Player 1 Move: ");
				string input = Console.ReadLine();

				while (availableMovesNames.Contains(input, StringComparer.OrdinalIgnoreCase) == false)
				{
					Console.Write("Doesn't Exist! Pick A Move: ");
					input = Console.ReadLine();
				}

				Console.WriteLine();

				Move playerOneMove = Enum.Parse<Move>(input, true);
				Move playerTwoMove = availableMoves[random.Next(0, availableMoves.Length)];

				Console.Write("Player 2 Move: ");
				Console.WriteLine(playerTwoMove.ToString());

				if (playerOneMove == playerTwoMove)
				{
					Console.WriteLine("Round Draw!");
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
				}

				Console.WriteLine();
				Console.WriteLine("Round End!");

				Console.ReadKey();
			}
		}
	}
}
