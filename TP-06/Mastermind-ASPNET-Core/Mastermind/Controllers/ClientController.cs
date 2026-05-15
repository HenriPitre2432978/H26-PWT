using Mastermind.DataAccessLayer;
using Mastermind.Models;
using Mastermind.Resources;
using Mastermind.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Mastermind.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Member? member =
                new DAL().MemberFact
                    .GetByUsername(model.Username);

            if (member == null)
            {
                ModelState.AddModelError("", Resource.InvalidLogin);

                return View(model);
            }

            PasswordHasher<Member> hasher = new();

            PasswordVerificationResult result =
                hasher.VerifyHashedPassword(member, member.Password, model.Password);

            if (result != PasswordVerificationResult.Success)
            {
                ModelState.AddModelError(
                    "",
                    Resource.InvalidLogin
                );

                return View(model);
            }

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name,member.Username),

                new Claim(ClaimTypes.Role,member.Role),

                new Claim("FullName",member.FullName)
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
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return RedirectToAction(
                "Index",
                "Home"
            );
        }
        [HttpGet]
        public IActionResult Signup()
        {
            return View(new SignupVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Signup(SignupVM model)
        {
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

            return RedirectToAction("Login");
        }
    }
}