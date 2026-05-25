using Mastermind.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Mastermind.ViewModels
{
    public class SignupVM
    {
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
            action: "VerifySignupUsername",
            controller: "Client",
            HttpMethod = "Get",
            ErrorMessage = "Ce nom d'utilisateur existe déjà."
        )]
        public string Username { get; set; } = string.Empty;

        [Required(
            ErrorMessageResourceName = "PasswordRequired",
            ErrorMessageResourceType = typeof(Resource)
        )]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required(
            ErrorMessageResourceName = "PasswordRequired",
            ErrorMessageResourceType = typeof(Resource)
        )]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}