using Mastermind.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Mastermind.ViewModels
{
    public class ProfileEditVM
    {
        public int Id { get; set; }

        [Required(
            ErrorMessageResourceName = "FullNameRequired",
            ErrorMessageResourceType = typeof(Resource)
        )]
        [Display(Name = "FullName", ResourceType = typeof(Resource))]
        [StringLength(20)]
        public string FullName { get; set; } = string.Empty;

        [Required(
            ErrorMessageResourceName = "EmailRequired",
            ErrorMessageResourceType = typeof(Resource)
        )]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required(
            ErrorMessageResourceName = "UsernameRequired",
            ErrorMessageResourceType = typeof(Resource)
        )]
        [Display(Name = "Username", ResourceType = typeof(Resource))]
        [StringLength(20)]
        [Remote(
            action: "VerifyProfileUsername",
            controller: "Client",
            AdditionalFields = nameof(Id),
            ErrorMessage = "Ce nom d'utilisateur existe déjà."
        )]
        public string Username { get; set; } = string.Empty;

        [Display(Name = "Password", ResourceType = typeof(Resource))]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resource))]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public IFormFile? ImageFile { get; set; }
        public string? ImagePath { get; set; } = string.Empty;
    }
}