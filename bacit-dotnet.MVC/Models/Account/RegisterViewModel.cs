// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace bacit_dotnet.MVC.Models.Account;

//FJERNER WARNINGS!
#pragma warning disable
///////////////////////

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Ansattnummer")]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Fornavn")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Etternavn")]
    public string LastName { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}
