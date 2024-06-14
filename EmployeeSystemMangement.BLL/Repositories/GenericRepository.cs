﻿using EmployeeSystemMangement.BLL.Interfaces;
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
            return _context.Set<T>().AsNoTracking().ToList();
        }
        public T GetById(int id)
        {
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