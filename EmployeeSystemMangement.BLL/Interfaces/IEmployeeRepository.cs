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
        IQueryable<Employee> GetEmployeeByAddress(string address);
    }
}
