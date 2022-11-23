// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace bacit_dotnet.MVC.Models.Account;

public class LoginViewModel
{
    [Required(ErrorMessage = "Vennligst fyll inn ansattnummer")]
    [StringLength(8, MinimumLength = 1)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Vennligst skriv inn et passord")]
    [DataType(DataType.Password)]
    [StringLength(128, ErrorMessage = "{0} må være minst {2} karakterer langt.", MinimumLength = 6)]
    public string Password { get; set; }

    [Display(Name = "Husk meg?")]
    public bool RememberMe { get; set; }
}
