using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mastermind.Models
{
    public class Member
    {
        public const string ROLE_ADMIN = "Admin";
        public const string ROLE_STANDARD = "Standard";

        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom complet est requis")]
        [StringLength(20, ErrorMessage = "Maximum 20 caractères")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le courriel est requis")]
        [StringLength(50, ErrorMessage = "Maximum 50 caractères")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nom d'utilisateur est requis")]
        [StringLength(20, ErrorMessage = "Maximum 20 caractères")]
        [Remote(
            action: "VerifyUsername",
            controller: "Member",
            areaName: "Admin",
            AdditionalFields = nameof(Id),
            ErrorMessage = "Ce nom d'utilisateur existe déjà."
        )]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est requis")]
        [StringLength(100, ErrorMessage = "Maximum 100 caractères")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le rôle est requis")]
        [StringLength(20, ErrorMessage = "Maximum 20 caractères")]
        public string Role { get; set; } = ROLE_STANDARD;

        [StringLength(100, ErrorMessage = "Maximum 100 caractères")]
        public string ImagePath { get; set; } = string.Empty;

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        // Required for model binding
        public Member() { }

        public Member(int id, string fullname, string email, string username, string password, string role, string imagePath)
            => (Id, FullName, Email, Username, Password, Role, ImagePath) = (id, fullname, email, username, password, role, imagePath);
    }
}