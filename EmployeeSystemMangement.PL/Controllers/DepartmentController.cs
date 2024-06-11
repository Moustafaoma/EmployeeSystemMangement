using EmployeeSystemMangement.BLL.Interfaces;
using EmployeeSystemMangement.BLL.Repositories;
using EmployeeSystemMangement.DAL.Entities;
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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid)
            {
               var count= _departmentRepository.Add(department);
                if(count>0)
                    return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
    }
}
