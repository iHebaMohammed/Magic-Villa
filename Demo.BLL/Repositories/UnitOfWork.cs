using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MagicVillaDbContext _context;
        private Hashtable _repositories;

        public UnitOfWork(MagicVillaDbContext context)
        {
            _context = context;
        }

        public IGenaricRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if(_repositories is null)
                _repositories = new Hashtable();
            
            var type = typeof(TEntity).Name;

            if(!_repositories.ContainsKey(type))
            {
                var repository = new GenaricRepository<TEntity>(_context);
                _repositories.Add(type, repository);
            }

            return (IGenaricRepository<TEntity>) _repositories[type];
        }

        public async Task<int> Complete()
            => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();
    }
}
