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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDBContext _context;
        public DepartmentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.AsNoTracking().ToList();
        }
        public Department GetById(int id)
        {
            return _context.Departments.Find(id);
        }
        public int Add(Department Entity)
        {
            _context.Departments.Add(Entity);
            return _context.SaveChanges();
        }
        public int Update(Department Entity)
        {
            _context.Departments.Update(Entity);
            return _context.SaveChanges();
        }
        public int Delete(Department Entity)
        {
            _context.Remove(Entity);
            return _context.SaveChanges();
        }

        
    }
}
