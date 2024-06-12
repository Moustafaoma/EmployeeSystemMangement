using EmployeeSystemMangement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSystemMangement.BLL.Interfaces
{
    public interface IBaseGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        int Add(T Entity); 
        int Update(T Entity);
        int Delete(T Entity);
    }
}
