namespace ReservationWeb.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Courriel { get; set; } = string.Empty;
        public int NbPersonne { get; set; }
        public DateTime DateReservation { get; set; }
        public int MenuChoiceId { get; set; }

        //Constructeur vide requis pour la désérialisation
        public Reservation()
        { }

        public Reservation(int id, string nom, string courriel, int nbPersonne, DateTime dateReservation, int menuChoiceId)
        {
            Id = id;
            Nom = nom;
            Courriel = courriel;
            NbPersonne = nbPersonne;
            DateReservation = dateReservation;
            MenuChoiceId = menuChoiceId;
        }
    }
}
