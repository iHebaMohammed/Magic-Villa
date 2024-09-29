﻿using Demo.BLL.Interfaces;
using Demo.BLL.Specifications;
using Demo.DAL.Data;
using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseEntity
    {
        private readonly MagicVillaDbContext _context;

        public GenaricRepository(MagicVillaDbContext context)
        {
            _context = context;
        }
        
        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();
        
        public async Task<T> GetByIdAsync(int id)
            => await _context.Set<T>().FindAsync(id);

        public async Task Create(T entity)
            => await _context.Set<T>().AddAsync(entity);

        public void Update(T entity)
            => _context.Set<T>().Update(entity);

        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);

        

        public Task<IReadOnlyList<T>> GetAllWithSpicifications(ISpecifications<T> specifications)
        {
            throw new NotImplementedException();
        }

        

        public Task<int> GetCountAsync(ISpecifications<T> specifications)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetEntityWithSpecification(ISpecifications<T> specifications)
        {
            throw new NotImplementedException();
        }


    }
}
