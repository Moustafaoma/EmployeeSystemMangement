using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSystemMangement.DAL.Entities
{
	public class ApplicationUsers:IdentityUser
	{
		
		public string FirstName { get; set; }
		
		public string LastName { get; set; }
		
		
		
		public bool IsAgree { get; set; }

	}
}
