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
    public class DepartmentRepository : GenericRepository<BaseEntity>
    {
        public DepartmentRepository(ApplicationDBContext context):base(context)
        {
            
        }
    }
}
