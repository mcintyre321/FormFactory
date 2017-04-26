using System;
using System.Collections.Generic;
using FormFactory.Attributes;

namespace FormFactory.AspMvc.Example.Models
{
    public class RegisterModel
    {
        [Required]
        [Email]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [Password]
        public string Password { get; set; }

        [Password]
        [FormFactory.Attributes.CompareAttribute("Password", ErrorMessage = "Passwords did not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Title")]
        public Titles? Title { get; set; }

        public string AgeRange { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Tell us about yourself")]
        [MultilineText]
        public string AboutYou { get; set; }

        [Required]
        public IEnumerable<ArtStyles> FavouriteArtStyle { get; set; }
        public enum ArtStyles
        {
            Abstract,
            ModernArt,
            [Display(Name = "Flora/fauna")]
            FloraFauna,
            [Display(Name = "Urban & Street Art")]
            UrbanAndStreetArt
        }

        [Date, DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime BritishDate { get; set; }

        [Date]
        public DateTime DefaultDate { get; set; }

        public bool RegularCheckBox { get; set; }

        [LabelOnRight]
        [Display(Name = "I accept the <a href='/terms'>Terms and Conditions</a>")]
        public bool TermsAndConditions { get; set; }
    }
}