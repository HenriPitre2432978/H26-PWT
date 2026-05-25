using Mastermind.DataAccessLayer;
using Mastermind.Models;
using Mastermind.Resources;
using Mastermind.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Mastermind.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            return View(new LoginVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(model);

            Member? member = new DAL().MemberFact.GetByUsername(model.Username);

            if (member == null)
            {
                ModelState.AddModelError("", Resource.InvalidLogin);
                return View(model);
            }

            PasswordVerificationResult result =
                new PasswordHasher<Member>().VerifyHashedPassword(member, member.Password, model.Password);

            if (result != PasswordVerificationResult.Success)
            {
                ModelState.AddModelError("", Resource.InvalidLogin);
                return View(model);
            }

            List<Claim> claims = new()
            {
                new(ClaimTypes.Name, member.Username),
                new(ClaimTypes.Role, member.Role),
                new("FullName", member.FullName)
            };

            ClaimsPrincipal principal = new(
                new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)
            );

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Signup()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            return View(new SignupVM());
        }

        [AcceptVerbs("GET", "POST")]
        public JsonResult VerifySignupUsername(string username) => Json(!new DAL().MemberFact.Exists(username));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Signup(SignupVM model)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(model);

            DAL dal = new();

            if (dal.MemberFact.Exists(model.Username))
            {
                ModelState.AddModelError("Username", Resource.UsernameTaken);
                return View(model);
            }

            PasswordHasher<Member> hasher = new();
            Member newMember = new()
            {
                Username = model.Username.Trim(),
                FullName = model.FullName?.Trim() ?? "",
                Email = model.Email?.Trim() ?? "",
                Role = Member.ROLE_STANDARD,
                ImagePath = "",
                Password = hasher.HashPassword(null!, model.Password)
            };

            dal.MemberFact.Insert(newMember);

            Member? inserted = dal.MemberFact.GetByUsername(newMember.Username);

            // Create stats record after successful insert
            if (inserted != null)
                dal.MemberStatsFact.CreateForMember(inserted.Id);

            return RedirectToAction("Login");
        }

        [Authorize]
        [HttpGet]
        public IActionResult EditProfile()
        {
            string? username = User.Identity?.Name;

            if (string.IsNullOrWhiteSpace(username))
                return RedirectToAction("Login");

            Member? member = new DAL().MemberFact.GetByUsername(username);

            if (member == null)
                return RedirectToAction("Login");

            return View(new ProfileEditVM
            {
                Id = member.Id,
                FullName = member.FullName,
                Email = member.Email,
                Username = member.Username
            });
        }

        private string SaveImage(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return "";

            string folder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/uploads/members");

            Directory.CreateDirectory(folder);

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string path = Path.Combine(folder, fileName);

            using FileStream stream = new(path, FileMode.Create);
            file.CopyTo(stream);

            return $"/uploads/members/{fileName}";
        }

        [HttpGet]
        public IActionResult GetImage(int id)
        {
            DAL dal = new();

            Member? member = dal.MemberFact.Get(id);

            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                member.ImagePath.TrimStart('/')
            );

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            string contentType = Path.GetExtension(filePath).ToLower() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };

            return PhysicalFile(filePath, contentType);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProfile(ProfileEditVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            DAL dal = new();
            if (dal.MemberFact.ExistsOther(model.Username, model.Id))
            {
                ModelState.AddModelError("Username", Resource.UsernameTaken);
                return View(model);
            }

            Member? existing = dal.MemberFact.Get(model.Id);
            if (existing == null)
                return RedirectToAction("Login");

            existing.FullName = model.FullName.Trim();
            existing.Email = model.Email.Trim();
            existing.Username = model.Username.Trim();

            // IMAGE UPLOAD
            if (model.ImageFile != null)
            {
                // Delete previous image if exists
                if (!string.IsNullOrWhiteSpace(existing.ImagePath))
                {
                    string relativePath = existing.ImagePath
                        .Replace("~/", "")
                        .Replace("/", Path.DirectorySeparatorChar.ToString())
                        .TrimStart(Path.DirectorySeparatorChar);

                    string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);

                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                existing.ImagePath = SaveImage(model.ImageFile);
            }

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                existing.Password = new PasswordHasher<Member>().HashPassword(existing, model.Password);
            }

            dal.MemberFact.Update(existing);
            return RedirectToAction("Index", "Home");
        }

        [AcceptVerbs("GET", "POST")]
        public JsonResult VerifyProfileUsername(string username, int id) =>
            Json(!new DAL().MemberFact.ExistsOther(username, id));
    }
}