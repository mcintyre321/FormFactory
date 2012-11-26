using System.ComponentModel.DataAnnotations;

namespace FormFactory.Example.Models
{
    public enum Position
    {
        Contractor,
        [Display(Name = "Snr. Subcontractor")]SeniorSubcontractor
    }
}