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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
       
        public EmployeeRepository(ApplicationDBContext context):base(context)
        {

        }
       

        public IQueryable<Employee> GetEmployeeByName(string name) =>
             _context.Employees.Where(e => e.Name.ToLower().Contains(name));
  
        
    }
}
