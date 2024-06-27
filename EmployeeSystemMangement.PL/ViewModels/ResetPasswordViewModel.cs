using EmployeeSystemMangement.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace EmployeeSystemMangement.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
		[Required(ErrorMessage = "New Password is required")]
		[StringLength(100, ErrorMessage = "Password must be at least 6 characters long")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }
		[Required(ErrorMessage = "Please confirm your password")]
		[DataType(DataType.Password)]
		[Compare(nameof(NewPassword), ErrorMessage = "The New password and confirmation password do not match.")]
		public string ConfirmedPassword { get; set; }



	}
}
