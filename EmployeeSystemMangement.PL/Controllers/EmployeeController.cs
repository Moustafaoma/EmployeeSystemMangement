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
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(IEmployeeRepository employeeRepository,IDepartmentRepository departmentRepository, IWebHostEnvironment env)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _env = env;
        }
        public IActionResult Index()
        {
            var employees= _employeeRepository.GetAll();
            if (employees.Count()==0)
                return NotFound("No employee found");

            return View(employees);
        }
        public IActionResult Details(int?id,string viewName="Details")
        {
            if (!id.HasValue)
                return BadRequest();
            var employee=_employeeRepository.GetById(id.Value);
            if (employee is null)
                return NotFound();
            return View(viewName,employee);

        }
        public IActionResult Create()
        {
            ViewBag.departments = _departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var count = _employeeRepository.Add(employee);
                    if (count > 0)
                    {
                        TempData["Message"] = "This Employee created sucessfully.. ";
                        return RedirectToAction(nameof(Index));

                    }
                }
                catch (Exception ex)
                {
                    if(_env.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    else
                        ModelState.AddModelError(string.Empty, "Failed To Added Employee");

                }

            }
            return View(employee);
        }
        public IActionResult Edit(int? id,string viewName)
        {
            ViewBag.departments = _departmentRepository.GetAll();

            return Details(id,viewName);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return View(employee);
            try
            {
                _employeeRepository.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                /// if develpoment log the Exception in file 
                /// if Production or testing or ..  Show Friendly message or view 
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error Occured");
                return View(employee);
            }
        }
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                _employeeRepository.Delete(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error Occured");
                return View(employee);

            }

        }



    }
}
