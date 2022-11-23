// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace bacit_dotnet.MVC.Models.Account;

//FJERNER WARNINGS!
#pragma warning disable
///////////////////////

public class RegisterViewModel
{
    [Required(ErrorMessage = "Vennligst legg inn et ansattnummer.")]
    [Display(Name = "Ansattnummer")]
    [StringLength(8, MinimumLength = 1)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Vennligst skriv inn fornavnet til den ansatte.")]
    [Display(Name = "Fornavn")]
    [StringLength(32, MinimumLength = 2)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Vennligst skriv inn etternavnet til den ansatte.")]
    [Display(Name = "Etternavn")]
    [StringLength(32, MinimumLength = 2)]
    public string LastName { get; set; }
    [Required]
    [Display(Name = "Rolle")]
    public string Role { get; set; }

    [Required(ErrorMessage = "Vennligst skriv inn et passord")]
    [StringLength(128, ErrorMessage = "{0} må være minst {2} karakterer langt.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Passord")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Vennligst repeter det samme passordet")]
    [StringLength(128)]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "Det er et misforhold mellom passordene")]
    public string ConfirmPassword { get; set; }
}
