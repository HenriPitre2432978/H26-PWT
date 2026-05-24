namespace Mastermind.Areas.Admin.ViewModels
{
    public class HomeVM
    {
        public int NbColors { get; set; }
        public int NbPositions { get; set; }
        public int NbAttempts { get; set; }
        public int TotalWins { get; set; }
        public int TotalLosses { get; set; }
        public HomeVM(int nbColor, int nbPositions, int nbAttempts, int totalWins, int totalLosses)
        {
            NbColors = nbColor;
            NbPositions = nbPositions;
            NbAttempts = nbAttempts;

            TotalWins = totalWins;
            TotalLosses = totalLosses;
        }
    }
}
