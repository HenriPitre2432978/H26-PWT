using System.ComponentModel.DataAnnotations;

namespace ReservationWeb.Models
{
    public class MenuChoice
    {
        public int Id { get; set; }

        //Empecher null, espaces vide et limiter à 30 caractères
        [Required(ErrorMessage = "Description requise")]
        [StringLength(30, ErrorMessage = "Maximum 30 caractères")]
        public string Description { get; set; } = "";

        //Constructeur vide requis pour la désérialisation
        public MenuChoice()
        { }

        public MenuChoice(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
