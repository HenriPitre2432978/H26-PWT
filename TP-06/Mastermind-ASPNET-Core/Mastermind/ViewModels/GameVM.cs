using Mastermind.GameModels;

namespace Mastermind.ViewModels
{
    public class GameVM
    {
        public Game Game { get; set; }
        
        //TODO: Ajoutez les statistiques du joueur connecté

        public GameVM(Game game)
        {
            Game = game;
        }
    }
}
