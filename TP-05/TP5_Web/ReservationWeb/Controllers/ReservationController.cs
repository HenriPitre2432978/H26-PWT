using Microsoft.AspNetCore.Mvc;
using ReservationWeb.DataAccessLayer.Factories;
using ReservationWeb.ViewModels;
using ReservationWeb.Models;

public class ReservationController : Controller
{
    public IActionResult Details(int? id)
    {
        if (id == null)
            return View("~/Views/Shared/Message.cshtml", "Aucun identifiant fourni.");

        // Get la réservation
        ReservationFactory resFactory = new();
        var reservation = resFactory.Get(id.Value);

        if (reservation == null)
            return View("~/Views/Shared/Message.cshtml", "Réservation introuvable.");

        // Get le menu from id
        MenuChoiceFactory menuFactory = new();
        MenuChoice? menu = menuFactory.Get(reservation.MenuChoiceId);

        ReservationDetailsVM vm = new()
        {
            Reservation = reservation,
            MenuDescription = menu?.Description ?? "Sans description"
        };

        return View(vm);
    }
}