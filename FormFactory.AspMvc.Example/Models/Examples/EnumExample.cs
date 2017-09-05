using FormFactory.Attributes;

namespace FormFactory.AspMvc.Example.Models.Examples
{
    public class EnumExample
    {
        public EnumExample()
        {
            Position = Positions.SeniorSubcontractor;
        }

        [Description("Enums are rendered as dropdowns")]
        public Positions Position { get; set; }

    }

    public enum Positions
    {
        Contractor,
        [Display(Name = "Snr. Subcontractor")]
        SeniorSubcontractor
    }
}