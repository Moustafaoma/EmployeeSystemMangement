using AutoMapper;
using EmployeeSystemMangement.BLL.Interfaces;
using EmployeeSystemMangement.BLL.Repositories;
using EmployeeSystemMangement.DAL.Entities;
using EmployeeSystemMangement.PL.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeSystemMangement.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository,IDepartmentRepository departmentRepository, IWebHostEnvironment env,IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _env = env;
            _mapper = mapper;
        }
        public IActionResult Index(string name)
        {
            IEnumerable<Employee> employees;
            IEnumerable<EmployeeViewModel> mappedEmployees;

            if (string.IsNullOrEmpty(name))
            {
                 employees = _employeeRepository.GetAll();
                mappedEmployees = _mapper.Map<System.Collections.Generic.IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

                if (mappedEmployees.Count() == 0)
                    return NotFound("No employee found");

                return View(mappedEmployees);
            }
            else
            {
                 employees = _employeeRepository.GetEmployeeByName(name.ToLower());
                 mappedEmployees = _mapper.Map<System.Collections.Generic.IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

                if (mappedEmployees.Count() == 0)
                    return NotFound("No employee found");

                return View(mappedEmployees);
            }

        }
        public IActionResult Details(int?id,string viewName="Details")
        {
            if (!id.HasValue)
                return BadRequest();
            var employee=_employeeRepository.GetById(id.Value);
            var mappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);
            if (mappedEmployee is null)
                return NotFound();
            return View(viewName, mappedEmployee);

        }
        public IActionResult Create()
        {
            ViewBag.departments = _departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel Vmemployee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Auto mapping Manually
                    //var mappedEmployee = new Employee()
                    //{
                    //    DepartmentId = Vmemployee.DepartmentId,
                    //    Age = Vmemployee.Age,
                    //    CreationDate = Vmemployee.CreationDate,
                    //    Address = Vmemployee.Address,
                    //    Email = Vmemployee.Email,
                    //    Gender = Vmemployee.Gender,
                    //    PhoneNumber = Vmemployee.PhoneNumber,
                    //};
                    var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(Vmemployee);
                    var count = _employeeRepository.Add(mappedEmployee);
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
            return View(Vmemployee);
        }
        public IActionResult Edit(int? id,string viewName)
        {
            ViewBag.departments = _departmentRepository.GetAll();

            return Details(id,viewName);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel Vmemployee)
        {
            if (id != Vmemployee.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return View(Vmemployee);
            try
            {
                var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(Vmemployee);

                _employeeRepository.Update(mappedEmployee);
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
                return View(Vmemployee);
            }
        }
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(EmployeeViewModel Vmemployee)
        {
            try
            {
                var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(Vmemployee);
                _employeeRepository.Delete(mappedEmployee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error Occured");
                return View(Vmemployee);

            }

        }



    }
}
