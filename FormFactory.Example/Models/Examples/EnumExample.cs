using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FormFactory.Example.Models.Examples
{
    public class EnumExample
    {
        public EnumExample()
        {
            Position = null;
            Position = Positions.SeniorSubcontractor;
        }

        [Description("Enums are rendered as dropdowns - nullable ones have an empty option")]
        public Positions? Position { get; set; }

        [Description("Enums are rendered as dropdowns - nullable ones have an empty option")]
        public Positions NonNullablePosition { get; set; }
    }

    public enum Positions
    {
        Contractor,
        [Display(Name = "Snr. Subcontractor")]
        SeniorSubcontractor
    }
}