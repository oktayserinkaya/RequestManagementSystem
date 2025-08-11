﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Abstract;
using CORE.Enums;
using CORE.Interface;
using DATAACCESS.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace DATAACCESS.Services
{
    public abstract class BaseService<T>(AppDbContext context) : IBaseRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context = context;
        private readonly DbSet<T> _table = context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            Console.WriteLine($"[DEBUG] AddAsync çağrıldı: {entity.GetType().Name}");
            await _table.AddAsync(entity);
            var result = await SaveAsync();
            Console.WriteLine($"[DEBUG] SaveAsync sonucu: {result}");
            return result;
        }


        public async Task<bool> UpdateAsync(T entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = Status.Modified;
            _table.Update(entity);

            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            entity.DeletedDate = DateTime.Now;
            entity.Status = Status.Passive;
            _table.Update(entity);

            return await SaveAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> expression) => _table.AnyAsync(expression);

        public async Task<List<T>> GetByDefaultsAsync(
        Expression<Func<T, bool>> expression,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? join = null)
        {
            IQueryable<T> query = _table;
            if (join != null)
                query = join(query);

            // ❗ ÖNCE: return await _table.Where(expression).ToListAsync();
            return await query.Where(expression).ToListAsync();
        }

        public async Task<T?> GetByDefaultAsync(
     Expression<Func<T, bool>> expression,
     Func<IQueryable<T>, IIncludableQueryable<T, object>>? join = null)
        {
            IQueryable<T> query = _table;
            if (join != null)
                query = join(query);

            return await query.FirstOrDefaultAsync(expression);
        }


        public async Task<T?> GetByIdAsync(Guid id) => await _table.FirstOrDefaultAsync(x => x.Status != Status.Passive && x.Id == id);

        public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>>? where = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? join = null)
        {
            IQueryable<T> query = _table;

            if (join != null)
                query = join(query);
            if (where != null)
                query = query.Where(where);
            if (orderBy != null)
                return await orderBy(query).Select(select).ToListAsync();

            return await query.Select(select).ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            if (predicate is null)
                return await _table.CountAsync();
            return await _table.CountAsync(predicate);
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();

        private async Task<bool> SaveAsync() => await _context.SaveChangesAsync() > 0;

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
        public EntityEntry Entry(object entity) => _context.Entry(entity);
    }
}