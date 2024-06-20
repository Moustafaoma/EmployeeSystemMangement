using EmployeeSystemMangement.BLL.Repositories;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeeSystemMangement.BLL.Interfaces
{
    public interface IUnitOfWork:IDisposable 
    {
        IEmployeeRepository EmployeeRepository { get; set; }
        IDepartmentRepository DepartmentRepository { get; set; }
        int Complete();
        
    }
}
