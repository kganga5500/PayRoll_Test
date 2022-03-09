﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PaylocityProduct.Domain.Interfaces;
using PaylocityProduct.Domain.Models;
using PaylocityProduct.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace PaylocityProduct.Infrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly PayrollDBContext Db;

        protected readonly DbSet<TEntity> DbSet;

        protected Repository(PayrollDBContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }
    
        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remove(TEntity entity)
        {
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}