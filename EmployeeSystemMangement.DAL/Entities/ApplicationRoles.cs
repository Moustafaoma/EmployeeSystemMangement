using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSystemMangement.DAL.Entities
{
	public class ApplicationRoles:IdentityRole
	{
		public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
