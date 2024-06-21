using EmployeeSystemMangement.BLL.Repositories;
using EmployeeSystemMangement.DAL.Entities;
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
        //IEmployeeRepository EmployeeRepository { get; set; }
        //IDepartmentRepository DepartmentRepository { get; set; }
        IBaseGenericRepository<T> Repository<T>() where T : BaseEntity;
        int Complete();

        
    }
}
