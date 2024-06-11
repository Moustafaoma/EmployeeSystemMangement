using EmployeeSystemMangement.BLL.Interfaces;
using EmployeeSystemMangement.BLL.Repositories;
using EmployeeSystemMangement.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace EmployeeSystemMangement.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IWebHostEnvironment _env;
        public DepartmentController(IDepartmentRepository departmentRepository, IWebHostEnvironment env)
        {
            _departmentRepository = departmentRepository ;
            _env = env;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            if(departments.Count()==0)
                return NotFound("No Department Found");
            return View(departments);
        }
        public IActionResult Details(int? id,string viewName="Details")
        {
            if (id == null)
                return new BadRequestResult(); //400
            var department=_departmentRepository.GetById(id.Value);
            if (department is null)
                return NotFound("Not Dept found"); //405
            return View(viewName,department);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [HttpGet]
        public IActionResult Edit(int? id) {
        
        return Details(id,"Edit");
        
        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,Department department)
        {
            if(id!=department.Id)
                return BadRequest();

            if(!ModelState.IsValid)
                return View(department);
            try
            {
                _departmentRepository.Update(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                /// if develpoment log the Exception in file 
                /// if Production or testing or ..  Show Friendly message or view 
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty,ex.Message);
                else
                    ModelState.AddModelError(string.Empty,"Error Occured");
                return View(department);
            }


        }





    }
}
