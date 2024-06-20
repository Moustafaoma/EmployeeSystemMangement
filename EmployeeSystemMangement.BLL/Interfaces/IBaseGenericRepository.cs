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
        void Add(T Entity); 
        void Update(T Entity);
        void Delete(T Entity);
    }
}
