using EmployeeSystemMangement.BLL.Interfaces;
using EmployeeSystemMangement.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EmployeeSystemMangement.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository ;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            if(departments.Count()==0)
                return NotFound("No Department Found");
            return View(departments);
        }
    }
}
