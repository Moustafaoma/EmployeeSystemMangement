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
    public class GenericRepository<T> : IBaseGenericRepository<T> where T : BaseEntity
    {
        private protected readonly ApplicationDBContext _context;
        public GenericRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            //Its Solution By Specific Design pattern
            if(typeof(T)==typeof(Employee))
                return (IEnumerable<T>)_context.Employees.Include(e=>e.Department).AsNoTracking().ToList();

            return _context.Set<T>().AsNoTracking().ToList();
        }
        public T GetById(int id)
        {
            //Its Solution By Specific Design pattern
            if (typeof(T) == typeof(Employee))
                return _context.Employees.Include(_e => _e.Department).FirstOrDefault(e => e.Id == id) as T;

                return _context.Set<T>().Find(id);
        }
        public int Add(T Entity)
        {
            _context.Set<T>().Add(Entity);
            return _context.SaveChanges();
        }
        public int Update(T Entity)
        {
            _context.Set<T>().Update(Entity);
            return _context.SaveChanges();
        }
        public int Delete(T Entity)
        {
            _context.Remove(Entity);
            return _context.SaveChanges();
        }

    }
}
