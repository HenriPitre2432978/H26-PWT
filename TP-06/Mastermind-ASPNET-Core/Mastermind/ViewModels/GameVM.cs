using Mastermind.GameModels;
using Mastermind.Models;

namespace Mastermind.ViewModels
{
    public class GameVM
    {
        public Game Game { get; set; }
        public MemberStats? Stats { get; set; }

        public GameVM(Game game, MemberStats? stats)
        {
            Game = game;
            Stats = stats;
        }
    }
}