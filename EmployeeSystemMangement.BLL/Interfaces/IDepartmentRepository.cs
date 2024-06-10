using EmployeeSystemMangement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSystemMangement.BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department GetById(int id);
        int Add(Department Entity);
        int Update(Department Entity);
        int Delete(Department Entity);
    }
}
