using Xunit;

namespace RockPaperScissors.Tests
{
	public class RockPaperScissorsTests
	{
		[Fact]
		public void IsWinnerCalculatedCorrectly()
		{
			string playerOne = "Rock";
			string playerTwo = "Paper";

			int winner = RockPaperScissors.CalculateWinner(playerOne, playerTwo);

			Assert.Equal(2, winner);
		}
	}
}
