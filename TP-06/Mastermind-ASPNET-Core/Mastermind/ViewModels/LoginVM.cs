using Mastermind.Resources;
using System.ComponentModel.DataAnnotations;

namespace Mastermind.ViewModels
{
    public class LoginVM
    {
        [Required(
            ErrorMessageResourceName = "UsernameRequired",
            ErrorMessageResourceType = typeof(Resource)
        )]
        [Display(
            Name = "Username",
            ResourceType = typeof(Resource)
        )]
        public string Username { get; set; } = string.Empty;

        [Required(
            ErrorMessageResourceName = "PasswordRequired",
            ErrorMessageResourceType = typeof(Resource)
        )]
        [Display(
            Name = "Password",
            ResourceType = typeof(Resource)
        )]
        public string Password { get; set; } = string.Empty;
    }
}