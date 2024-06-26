using System.Threading.Tasks;

namespace EmployeeSystemMangement.PL.Services.SendEmail
{
	public interface ISendEmail
	{
		Task SendEmailAsync(string from,string recipients,string subject ,string body);
		

		
	}
}
