using Mastermind.Resources;
using System.ComponentModel.DataAnnotations;

namespace Mastermind.Areas.Admin.ViewModels
{
    public class ConfigEditVM
    {
        [Display(Name = "NbColors", ResourceType = typeof(Resource))]
        [Range(
            6,
            8,
            ErrorMessageResourceName = "NbColorsRange",
            ErrorMessageResourceType = typeof(Resource)
        )]
        public int NbColors { get; set; }

        [Display(Name = "NbPositions", ResourceType = typeof(Resource))]
        [Range(
            4,
            5,
            ErrorMessageResourceName = "NbPositionsRange",
            ErrorMessageResourceType = typeof(Resource)
        )]
        public int NbPositions { get; set; }

        [Display(Name = "NbTries", ResourceType = typeof(Resource))]
        [Range(
            6,
            12,
            ErrorMessageResourceName = "NbAttemptsRange",
            ErrorMessageResourceType = typeof(Resource)
        )]
        public int NbAttempts { get; set; }

        public ConfigEditVM()
        {
        }

        public ConfigEditVM(int nbColor, int nbPositions, int nbAttempts)
        {
            NbColors = nbColor;
            NbPositions = nbPositions;
            NbAttempts = nbAttempts;
        }
    }
}