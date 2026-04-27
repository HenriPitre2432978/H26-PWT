using Microsoft.AspNetCore.Mvc;
using ReservationWeb.DataAccessLayer;
using ReservationWeb.DataAccessLayer.Factories;
using ReservationWeb.Models;

namespace ReservationWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuChoiceController : Controller
    {
        public IActionResult List()
        {
            List<MenuChoice> list = DAL.MenuChoiceFact.GetAll();

            list = [.. list.OrderBy(x => x.Description)];

            //Pass to the Controller base view with the list of MenuChoice
            return View(list);
        }

        public IActionResult Create()
        {
            MenuChoice model = new();

            ViewBag.IsEdit = false;

            //Pass to the CreateEdit view with the MenuChoice model
            return View("CreateEdit", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken] //Ensure data is coming from the form and not from a malicious source
        public IActionResult Create(MenuChoice model)
        {
            model.Description = model.Description?.Trim() ?? "";
            ViewBag.IsEdit = false;

            // If model is not valid, return to the CreateEdit view with the model to show validation errors
            if (!ModelState.IsValid)
                return View("CreateEdit", model);

            // If exists a MenuChoice with the same description, add a model error and return to the CreateEdit view
            if (DAL.MenuChoiceFact.Exists(model.Description))
            {
                // Add a model error following the Description field, then remake view
                ModelState.AddModelError("Description", "Ce choix existe déjà.");
                return View("CreateEdit", model);
            }

            // Insert the new MenuChoice in the database
            DAL.MenuChoiceFact.Insert(model);

            // Redirect to the List view(action) to show the updated list of MenuChoice
            return RedirectToAction("List");
        }

        public IActionResult Edit(int? id)
        {
            // If id is null, return to the Shared/Error view with a message
            if (id == null)
                return View("~/Views/Shared/Error.cshtml", "Identifiant invalide.");

            MenuChoice item = DAL.MenuChoiceFact.Get(id.Value);

            if (item == null)
                return View("~/Views/Shared/Error.cshtml", "Item inexistant.");

            ViewBag.IsEdit = true;

            return View("CreateEdit", item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MenuChoice model)
        {
            ViewBag.IsEdit = true;

            if (!ModelState.IsValid)
                return View("CreateEdit", model);

            // Vérifier doublon (exclure lui-même)
            if (DAL.MenuChoiceFact.ExistsOther(model.Description, model.Id))
            {
                ModelState.AddModelError("Description", "Ce choix existe déjà.");
                return View("CreateEdit", model);
            }

            // Update the MenuChoice in the database
            DAL.MenuChoiceFact.Update(model);

            return RedirectToAction("List");
        }

        // GET: /Admin/MenuChoice/Delete/3
        public IActionResult Delete(int? id)
        {
            // If id is null, return to the Shared/Message view with a message
            if (id == null)
                return View("~/Views/Shared/Message.cshtml", "Aucun identifiant fourni.");

            MenuChoiceFactory factory = new();
            MenuChoice? menuChoice = factory.Get(id.Value);

            // If menuChoice is null, return to the Shared/Message view with a message
            if (menuChoice == null)
                return View("~/Views/Shared/Message.cshtml", "Choix de menu introuvable.");

            return View(menuChoice);
        }


        // POST: /Admin/MenuChoice/Delete/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(MenuChoice model)
        {
            MenuChoiceFactory factory = new();

            MenuChoice? existing = factory.Get(model.Id);

            // If existing is null, return to the Shared/Message view with a message
            if (existing == null)
            {
                return View("~/Views/Shared/Message.cshtml", "Impossible de supprimer : item inexistant.");
            }

            // Delete the MenuChoice from the database
            factory.Delete(model.Id);

            return RedirectToAction("List");
        }
    }
}