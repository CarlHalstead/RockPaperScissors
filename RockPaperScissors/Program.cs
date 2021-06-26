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

		private static readonly Dictionary<Move, List<Move>> moveDefeats = new Dictionary<Move, List<Move>>() 
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

			Console.Write("Available Moves: ");
			Console.WriteLine(string.Join(", ", availableMovesNames));

			Console.WriteLine();

			Console.Write("Please Pick A Move: ");
			string input = Console.ReadLine();

			while (availableMovesNames.Contains(input, StringComparer.OrdinalIgnoreCase) == false)
			{
				Console.WriteLine("Move Does Not Exist!");
				input = Console.ReadLine();
			}

			Move playerMove = Enum.Parse<Move>(input, true);
			Move aiMove = availableMoves[random.Next(0, availableMoves.Length)];

			Console.Write("AI Has Chosen: ");
			Console.WriteLine(aiMove.ToString());

			Console.Write("This Defeats: ");
			Console.WriteLine(string.Join(", ", moveDefeats[aiMove]));

			if (playerMove == aiMove)
			{
				// Draw
			}
			else 
			{
				// Victory for someone
			}
		}
	}
}
