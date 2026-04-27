using ReservationWeb.Models;

namespace ReservationWeb.ViewModels
{
    public class ReservationDetailsVM
    {
        //Peut-être qu'un ViewModel pour cette page est un peu inutile, mais je suppose que c'est la "bonne pratique"
        public Reservation Reservation { get; set; } = null!;
        public string MenuDescription { get; set; } = "";
    }
}