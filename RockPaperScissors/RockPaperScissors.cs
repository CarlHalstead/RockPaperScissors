using System;
using System.Linq;
using System.Collections.Generic;

namespace RockPaperScissors
{
	public static class RockPaperScissors
	{
		/// <summary>
		/// The key for this dictionary is the name of each move, case insentitive. Each value being a collection of string
		/// that get defeated by this move. e.g. Rock beats Scissors and Lizard. Spock beats Rock and Scissors.
		/// </summary>
		public readonly static IReadOnlyDictionary<string, string[]> moves = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"Rock", new []
				{
					"Scissors",
					"Lizard"
				}
			},
			{
				"Paper", new []
				{
					"Rock",
					"Spock"
				}
			},
			{
				"Scissors", new []
				{
					"Paper",
					"Lizard"
				}
			},
			{
				"Lizard", new []
				{
					"Paper",
					"Spock"
				}
			},
			{
				"Spock", new []
				{
					"Rock",
					"Scissors"
				}
			}
		};

		public readonly static Dictionary<string, int> moveUses = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
		{
			{ "Rock", 0 },
			{ "Paper", 0 },
			{ "Scissors", 0 },
			{ "Lizard", 0 },
			{ "Spock", 0 },
		};

		public static int GetMoveUse(string moveName) 
		{
			return moveUses[moveName];
		}

		public static void AddMoveUse(string moveName) 
		{
			SetMoveUse(moveName, GetMoveUse(moveName) + 1);
		}

		public static void SetMoveUse(string moveName, int value) 
		{
			if (moveUses.ContainsKey(moveName) == false)
				return;

			moveUses[moveName] = value;
		}

		/// <summary>
		/// Reset the number of times each move has been used, ready for the next match
		/// </summary>
		public static void ClearMoveUses()
		{
			foreach (KeyValuePair<string, int> kvp in moveUses)
			{
				moveUses[kvp.Key] = 0;
			}
		}

		public static int CalculateWinner(string playerOne, string playerTwo)
		{
			if (moves[playerOne].Contains(playerTwo, StringComparer.OrdinalIgnoreCase))
			{
				return 1;
			}
			else if (moves[playerTwo].Contains(playerOne, StringComparer.OrdinalIgnoreCase))
			{
				return 2;
			}
			else
			{
				throw new Exception("Neither player has won. This should not be possible!");
			}
		}
	}
}
