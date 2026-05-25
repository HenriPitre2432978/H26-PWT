namespace Mastermind.GameModels
{
    public class PlayerRow
    {
        public List<Pawn> Pawns { get; set; } = new List<Pawn>();

        public int NbBlackMarks
        {
            get => Pawns.Count(pawn => pawn.Mark == Pawn.MarkState.Black);
        }

        public int NbWhiteMarks
        {
            get => Pawns.Count(pawn => pawn.Mark == Pawn.MarkState.White);
        }

        public PlayerRow() { }
    }
}
