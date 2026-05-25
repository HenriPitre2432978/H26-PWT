namespace Mastermind.GameModels
{
    public class Game
    {
        public enum GameState
        {
            ComputerWin,
            PlayerWin,
            Running
        }

        public bool StatisticsSaved { get; set; } = false;

        public int NbColors { get; set; }
        public int NbPositions { get; set; }
        public int NbAttempts { get; set; }

        public GameState State { get; set; } = GameState.Running;
        public int CurrentPlayingRow { get; set; } = 1;
        public ComputerRow ComputerRow { get; set; } = new();
        public List<PlayerRow> PlayerRows { get; set; } = new();

        public Game(int nbColors, int nbPositions, int nbAttempts)
        {
            Random rnd = new();

            NbColors = nbColors;
            NbPositions = nbPositions;
            NbAttempts = nbAttempts;

            for (int i = 0; i < NbPositions; i++)
                ComputerRow.PawnColors.Add(rnd.Next(1, NbColors + 1));
        }

        public void Validate(PlayerRow playerRow)
        {
            if (State != GameState.Running)
                return;

            // Copy colors so matched pawns are not counted twice
            List<int> computerColors = new(ComputerRow.PawnColors);

            // Check pawns with correct color and correct position
            for (int position = 0; position < NbPositions; position++)
            {
                Pawn pawn = playerRow.Pawns[position];

                if (pawn.Color == ComputerRow.PawnColors[position])
                {
                    pawn.Mark = Pawn.MarkState.Black;

                    computerColors.Remove(pawn.Color);
                }
            }

            if (playerRow.NbBlackMarks == NbPositions)
                State = GameState.PlayerWin;
            else
            {
                // Check pawns with correct color but wrong position
                for (int position = 0; position < NbPositions; position++)
                {
                    Pawn pawn = playerRow.Pawns[position];

                    if (pawn.Mark == Pawn.MarkState.None
                        && computerColors.Contains(pawn.Color))
                    {
                        pawn.Mark = Pawn.MarkState.White;

                        computerColors.Remove(pawn.Color);
                    }
                }

                if (CurrentPlayingRow == NbAttempts)
                    State = GameState.ComputerWin;
            }

            PlayerRows.Add(playerRow);

            CurrentPlayingRow++;
        }
    }
}