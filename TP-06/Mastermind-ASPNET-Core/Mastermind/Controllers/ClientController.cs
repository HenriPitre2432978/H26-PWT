using Mastermind.DataAccessLayer;
using Mastermind.Models;
using Mastermind.Resources;
using Mastermind.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.IO;
using System.Security.Claims;

namespace Mastermind.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View(new LoginVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(model);

            Member? member = new DAL().MemberFact.GetByUsername(model.Username);

            if (member == null)
            {
                ModelState.AddModelError("", Resource.InvalidLogin);
                return View(model);
            }

            PasswordVerificationResult result = new PasswordHasher<Member>().VerifyHashedPassword(member, member.Password, model.Password);

            if (result != PasswordVerificationResult.Success)
            {
                ModelState.AddModelError("", Resource.InvalidLogin);
                return View(model);
            }

            List<Claim> claims = new()
            {
                new(ClaimTypes.Name,member.Username),
                new(ClaimTypes.Role,member.Role),
                new("FullName",member.FullName)
            };

            ClaimsIdentity identity =
                new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsPrincipal principal =
                new(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            return RedirectToAction(
                "Index",
                "Home"
            );
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Signup()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View(new SignupVM());
        }

        [AcceptVerbs("GET", "POST")]
        public JsonResult VerifySignupUsername(string username)
        {
            bool exists =
                new DAL().MemberFact.Exists(username);

            return Json(!exists);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Signup(SignupVM model)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(model);

            DAL dal = new DAL();

            if (dal.MemberFact.Exists(model.Username))
            {
                ModelState.AddModelError("Username", Resource.UsernameTaken);
                return View(model);
            }

            PasswordHasher<Member> hasher = new();

            Member newMember = new Member
            {
                Username = model.Username.Trim(),
                FullName = model.FullName?.Trim() ?? "",
                Email = model.Email?.Trim() ?? "",
                Role = Member.ROLE_STANDARD,
                ImagePath = "",
            };

            newMember.Password =
                hasher.HashPassword(newMember, model.Password);

            dal.MemberFact.Insert(newMember);

            Member? inserted =
    dal.MemberFact.GetByUsername(
        newMember.Username
    );

            if (inserted != null)
            {
                dal.MemberStatsFact
                    .CreateForMember(inserted.Id);
            }

            return RedirectToAction("Login");
        }

        [Authorize]
        [HttpGet]
        public IActionResult EditProfile()
        {
            string? username = User.Identity?.Name;

            if (string.IsNullOrWhiteSpace(username))
                return RedirectToAction("Login");

            Member? member =
                new DAL().MemberFact.GetByUsername(username);

            if (member == null)
                return RedirectToAction("Login");

            ProfileEditVM vm = new()
            {
                Id = member.Id,
                FullName = member.FullName,
                Email = member.Email,
                Username = member.Username
            };

            return View(vm);
        }
        private string SaveImage(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return "";

            string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/members");
            Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string path = Path.Combine(folder, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/uploads/members/" + fileName;
        }

        [HttpGet]
        public IActionResult GetImage(int id)
        {
            DAL dal = new DAL();

            Member? member = dal.MemberFact.Get(id);

            if (member == null || string.IsNullOrWhiteSpace(member.ImagePath))
            {
                // fallback image (default avatar)
                string defaultPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/img/default-avatar.png"
                );

                if (System.IO.File.Exists(defaultPath))
                {
                    return PhysicalFile(defaultPath, "image/png");
                }

                return NotFound();
            }

            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                member.ImagePath.TrimStart('/')
            );

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }


            string ext = Path.GetExtension(filePath).ToLower();

            string contentType = ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "application/octet-stream",
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
                ModelState.AddModelError(
                    "Username",
                    Resource.UsernameTaken
                );

                return View(model);
            }

            Member? existing =
                dal.MemberFact.Get(model.Id);

            if (existing == null)
                return RedirectToAction("Login");

            existing.FullName =
                model.FullName.Trim();

            existing.Email =
                model.Email.Trim();

            existing.Username =
                model.Username.Trim();


            // IMAGE UPLOAD
            if (model.ImageFile != null)
            {
                // SUPPRIMER ANCIENNE IMAGE
                if (!string.IsNullOrWhiteSpace(existing.ImagePath))
                {
                    string relativePath = existing.ImagePath;

                    // normalize
                    relativePath = relativePath.Replace("~/", "").Replace("/", Path.DirectorySeparatorChar.ToString());
                    relativePath = relativePath.TrimStart(Path.DirectorySeparatorChar);

                    string oldPath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        relativePath
                    );

                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                existing.ImagePath = SaveImage(model.ImageFile);
            }


            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                PasswordHasher<Member> hasher = new();

                existing.Password =
                    hasher.HashPassword(existing, model.Password);
            }

            dal.MemberFact.Update(existing);

            return RedirectToAction(
                "Index",
                "Home"
            );
        }
        [AcceptVerbs("GET", "POST")]
        public JsonResult VerifyProfileUsername(
    string username,
    int id
)
        {
            bool exists =
                new DAL().MemberFact
                    .ExistsOther(username, id);

            return Json(!exists);
        }
    }
}