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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        public DepartmentController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            if(departments.Count()==0)
                return NotFound("No Department Found");
            return View(departments);
        }
        public IActionResult Details(int? id,string viewName="Details")
        {
            if (id == null)
                return new BadRequestResult(); //400
            var department=_unitOfWork.DepartmentRepository.GetById(id.Value);
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
                _unitOfWork.DepartmentRepository.Add(department);
                var count = _unitOfWork.Complete();
                if (count>0)
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
                _unitOfWork.DepartmentRepository.Update(department);
                _unitOfWork.Complete();

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

        public IActionResult Delete(int? id)
        {
            return Details(id,"Delete");
        }
        [HttpPost]
        public IActionResult Delete(Department department)
        {
            try
            {
                _unitOfWork.DepartmentRepository.Delete(department);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error Occured");
                return View(department);

            }

        }


    }
}
