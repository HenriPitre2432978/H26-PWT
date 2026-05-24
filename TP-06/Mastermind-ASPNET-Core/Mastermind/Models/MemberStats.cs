namespace Mastermind.Models
{
    public class MemberStats
    {
        public int MemberId { get; set; }

        public int GamesWon { get; set; }

        public int GamesLost { get; set; }

        public int? BestPerformance { get; set; }

        public MemberStats()
        {
        }

        public MemberStats(
            int memberId,
            int gamesWon,
            int gamesLost,
            int? bestPerformance
        )
        {
            MemberId = memberId;
            GamesWon = gamesWon;
            GamesLost = gamesLost;
            BestPerformance = bestPerformance;
        }
    }
}