using EmployeeSystemMangement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSystemMangement.BLL.Interfaces
{
    public interface IEmployeeRepository: IBaseGenericRepository<Employee>
    {
        IQueryable<Employee> GetEmployeeByName(string name);
    }
}
