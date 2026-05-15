using Mastermind.DataAccessLayer;
using Mastermind.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mastermind.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Member.ROLE_ADMIN)]
    public class MemberController : Controller
    {
        public IActionResult List()
        {
            List<Member> list = new DAL().MemberFact.GetAll();

            list = list
                .OrderBy(x => x.Username)
                .ToList();

            return View(list);
        }

        public IActionResult Create()
        {
            ViewBag.IsEdit = false;

            return View("CreateEdit", new Member());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Member model)
        {
            PasswordHasher<Member> hasher = new();

            model.FullName = model.FullName?.Trim() ?? "";
            model.Email = model.Email?.Trim() ?? "";
            model.Username = model.Username?.Trim() ?? "";
            if (model.Role != Member.ROLE_ADMIN && model.Role != Member.ROLE_STANDARD)
                model.Role = Member.ROLE_STANDARD;

            model.ImagePath = model.ImagePath?.Trim() ?? "";

            ViewBag.IsEdit = false;

            if (!ModelState.IsValid)
                return View("CreateEdit", model);

            if (new DAL().MemberFact.Exists(model.Username))
            {
                ModelState.AddModelError(
                    "Username",
                    "Ce nom d'utilisateur existe déjà."
                );

                return View("CreateEdit", model);
            }

            model.Password =
                hasher.HashPassword(model, model.Password);

            new DAL().MemberFact.Insert(model);

            return RedirectToAction("List");
        }

        public JsonResult VerifyUsername(string username)
        {
            bool exists = new DAL().MemberFact.Exists(username);
            return Json(!exists);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(
                    "~/Views/Shared/Error.cshtml",
                    "Identifiant invalide."
                );

            Member? item =
                new DAL().MemberFact.Get(id.Value);

            if (item == null)
                return View(
                    "~/Views/Shared/Error.cshtml",
                    "Membre inexistant."
                );

            ViewBag.IsEdit = true;

            return View("CreateEdit", item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Member model)
        {
            if (model.Role != Member.ROLE_ADMIN && model.Role != Member.ROLE_STANDARD)
                model.Role = Member.ROLE_STANDARD;


            PasswordHasher<Member> hasher = new();
            ViewBag.IsEdit = true;

            if (!ModelState.IsValid)
                return View("CreateEdit", model);

            if (new DAL().MemberFact.ExistsOther(
                model.Username,
                model.Id))
            {
                ModelState.AddModelError(
                    "Username",
                    "Ce nom d'utilisateur existe déjà."
                );

                return View("CreateEdit", model);
            }

            Member? existing =
                new DAL().MemberFact.Get(model.Id);

            if (existing == null)
            {
                return View(
                    "~/Views/Shared/Error.cshtml",
                    "Membre inexistant."
                );
            }

            if (!string.IsNullOrWhiteSpace(model.Password))
                model.Password = hasher.HashPassword(model, model.Password);

            else
                model.Password = existing.Password;


            new DAL().MemberFact.Update(model);

            return RedirectToAction("List");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return View(
                    "~/Views/Shared/Message.cshtml",
                    "Aucun identifiant fourni."
                );

            Member? member =
                new DAL().MemberFact.Get(id.Value);

            if (member == null)
                return View(
                    "~/Views/Shared/Message.cshtml",
                    "Membre introuvable."
                );

            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Member model)
        {
            Member? existing =
                new DAL().MemberFact.Get(model.Id);

            if (existing == null)
            {
                return View(
                    "~/Views/Shared/Message.cshtml",
                    "Impossible de supprimer ce membre."
                );
            }

            new DAL().MemberFact.Delete(model.Id);

            return RedirectToAction("List");
        }
    }
}