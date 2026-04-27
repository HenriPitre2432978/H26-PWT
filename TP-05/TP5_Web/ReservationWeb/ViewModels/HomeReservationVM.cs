using System.ComponentModel.DataAnnotations;
using ReservationWeb.Models;

namespace ReservationWeb.ViewModels
{
    public class HomeReservationVM
    {
        //Propriétés pour les champs du formulaire avec leurs contraintes respectives

        [Required(ErrorMessage = "Nom requis")]
        [StringLength(20, ErrorMessage = "Max 20 caractères")]
        public string Nom { get; set; } = "";

        [Required(ErrorMessage = "Courriel requis")]
        [EmailAddress(ErrorMessage = "Courriel invalide")]
        [StringLength(50, ErrorMessage = "Max 50 caractères")]
        public string Courriel { get; set; } = "";

        [Required(ErrorMessage = "Nombre requis")]
        [Range(1, 20, ErrorMessage = "Entre 1 et 20 personnes")]
        public int NbPersonnes { get; set; }

        [Required(ErrorMessage = "Date requise")]
        public DateTime DateReservation { get; set; }

        [Required(ErrorMessage = "Choix requis")]
        public int ChoixMenu { get; set; }

        public List<MenuChoice> MenuChoices { get; set; } = [];
    }
}