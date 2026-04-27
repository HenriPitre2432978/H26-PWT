using Microsoft.AspNetCore.Mvc;
using ReservationWeb.DataAccessLayer;
using ReservationWeb.DataAccessLayer.Factories;
using ReservationWeb.Models;

namespace ReservationWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReservationController : Controller
    {
        public IActionResult List()
        {
            ReservationFactory factory = new();
            var reservations = factory.GetAll();

            //Pass to the Controller base view with the list of Reservations
            return View(reservations);
        }

        public IActionResult Delete(int? id)
        {
            // Check if the id is null
            if (id == null)
                View("~/Views/Shared/Message.cshtml", "Aucun identifiant fourni.");

            // Get the reservation to delete
            ReservationFactory factory = new();
            Reservation? reservation = null;
            if (id != null)
            { reservation = factory.Get(id.Value); }

            if (reservation == null)
                return View("~/Views/Shared/Message.cshtml", "Réservation introuvable.");

            // Pass to the Controller base view with the reservation to delete
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Reservation model)
        {
            ReservationFactory factory = new();

            Reservation? existing = factory.Get(model.Id);
            // If the reservation doesn't exist, return to the Shared/Message view with a message
            if (existing == null)
                return View("~/Views/Shared/Message.cshtml", "Impossible de supprimer.");

            // Delete the reservation from the database
            factory.Delete(model.Id);

            return RedirectToAction("List");
        }
    }
}