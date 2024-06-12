using EmployeeSystemMangement.BLL.Interfaces;
using EmployeeSystemMangement.DAL.Data;
using EmployeeSystemMangement.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSystemMangement.BLL.Repositories
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly ApplicationDBContext _context;
        public EmployeeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.AsNoTracking().ToList();
        }
        public Employee GetById(int id)
        {
            return _context.Employees.Find(id);
        }
        public int Add(Employee Entity)
        {
            _context.Employees.Add(Entity);
            return _context.SaveChanges();
        }
        public int Update(Employee Entity)
        {
            _context.Employees.Update(Entity);
            return _context.SaveChanges();
        }
        public int Delete(Employee Entity)
        {
            _context.Remove(Entity);
            return _context.SaveChanges();
        }


    }
}
