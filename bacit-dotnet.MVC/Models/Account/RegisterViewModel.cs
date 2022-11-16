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
    public string Email { get; set; }

    [Required(ErrorMessage = "Vennligst skriv inn fornavnet til den ansatte.")]
    [Display(Name = "Fornavn")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Vennligst skriv inn etternavnet til den ansatte.")]
    [Display(Name = "Etternavn")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Vennligst skriv inn et passord")]
    [StringLength(100, ErrorMessage = "{0} må være minst {2} karakterer langt.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Passord")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Vennligst repeter det samme passordet")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "Det er et misforhold mellom passordene")]
    public string ConfirmPassword { get; set; }
}
