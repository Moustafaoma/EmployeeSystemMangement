using EmployeeSystemMangement.BLL.Interfaces;
using EmployeeSystemMangement.DAL.Data;
using EmployeeSystemMangement.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeeSystemMangement.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        //public IEmployeeRepository EmployeeRepository { get ; set ; }
        //public IDepartmentRepository DepartmentRepository { get; set; }

        private readonly ApplicationDBContext _context;
        private Hashtable _repositories;
        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
            _repositories = new Hashtable();

        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IBaseGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var key=typeof(T).Name;
            if (!_repositories.ContainsKey(key))
            {
                if (key == nameof(Employee))
                {   
                    var repository = new EmployeeRepository(_context);
                    _repositories.Add(key, repository);
                }
                else
                {
                    var repository = new GenericRepository<T>(_context);
                    _repositories.Add(key, repository);

                }

                
            }
            return _repositories[key] as IBaseGenericRepository<T>;
            
        }
    }
}
