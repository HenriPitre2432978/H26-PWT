using Microsoft.AspNetCore.Mvc;
using ReservationWeb.ViewModels;
using ReservationWeb.DataAccessLayer.Factories;
using ReservationWeb.Models;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        // Factory pour récupérer les choix de menu depuis la BD
        MenuChoiceFactory factory = new();

        // Création du ViewModel
        HomeReservationVM vm = new()
        {
            // Remplir la liste des menus (ordre alphabétique)
            MenuChoices = factory.GetAll()
                .OrderBy(m => m.Description)
                .ToList()
        };

        // Envoi du ViewModel à la vue
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(HomeReservationVM vm)
    {
        MenuChoiceFactory menuFactory = new();

        if (!ModelState.IsValid)
        {
            //Reload menus to  avoid nulls in dropdown
            vm.MenuChoices = menuFactory.GetAll();
            return View(vm);
        }

        ReservationFactory resFactory = new();

        //Create model object from prompted data
        Reservation r = new(
            0,
            vm.Nom,
            vm.Courriel,
            vm.NbPersonnes,
            vm.DateReservation,
            vm.ChoixMenu
        );

        int newId = resFactory.Insert(r);

        //Go to details page of the new reservation
        return RedirectToAction("Details", "Reservation", new { id = newId });
    }
}