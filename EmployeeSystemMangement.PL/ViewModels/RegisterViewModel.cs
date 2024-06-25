using EmployeeSystemMangement.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace EmployeeSystemMangement.PL.ViewModels
{
    public class RegisterViewModel
    {
		[Required(ErrorMessage = "First name is required")]
[StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last name is required")]
		[StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters")]
		public string LastName { get; set; }


		[Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Username { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        //[RegularExpression(@"^[a-zA-Z0-9._%+-]{5,}@(gmail\.com|yahoo\.com|hotmail\.com|outlook\.com|icloud\.com|protonmail\.com)$", ErrorMessage = "The email must have at least 5 characters before the '@' symbol and must be a valid domain (gmail.com, yahoo.com, hotmail.com, outlook.com, icloud.com, protonmail.com).")]
        public string Email { get; set; }
		[Required(ErrorMessage = "Password is required")]
		[StringLength(100,ErrorMessage = "Password must be at least 6 characters long")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Please confirm your password")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmedPassword { get; set; }
        public bool IsAgree { get; set; }





    }
}
