using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BUSINESS.Manager.Interface;
using CORE.Entities.Abstract;
using CORE.Interface;
using DTO.Abstract;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace BUSINESS.Manager.Concrete
{
    public abstract class BaseManager<A, C>(A service, IMapper mapper ) : IBaseManager<A, C>
        where A : IBaseRepository<C>
        where C : BaseEntity
    {
        private readonly A _service = service;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> AddAsync(BaseDTO dto)
        {
            var entity = _mapper.Map<C>(dto);
            return await _service.AddAsync(entity);
        }

        public async Task<bool> AddEntityAsync(C entity)
        {
            return await _service.AddAsync(entity);
        }
        public async Task<bool> UpdateAsync(BaseDTO dto, Guid id)
        {
            var entity = await _service.GetByIdAsync(id); // tracked olmalı (AsNoTracking YOK)
            if (entity == null)
                return false;

            _mapper.Map(dto, entity);

            var entry = _service.Entry(entity);

            // PK'yi asla modified yapma
            foreach (var p in entry.Properties.Where(p => p.Metadata.IsPrimaryKey()))
                p.IsModified = false;

            // FK adayları – sizdeki isimlere göre listeyi düzenleyin
            MarkUnmodified(entry, "AppUserId", "EmployeeId", "DepartmentId", "TitleId", "ProductId");

            return await _service.SaveChangesAsync() > 0;
        }

        private static void MarkUnmodified(EntityEntry entry, params string[] names)
        {
            foreach (var n in names)
            {
                var prop = entry.Properties.FirstOrDefault(p => p.Metadata.Name == n);
                if (prop != null) prop.IsModified = false;
            }
        }

        public async Task<bool> UpdateEntityAsync(C entity)
        => await _service.UpdateAsync(entity);

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _service.GetByIdAsync(id);
            if(entity == null) 
                return false;

            return await _service.DeleteAsync(entity); 
        }

        public async Task<bool> AnyAsync(Expression<Func<C, bool>> expression) => await _service.AnyAsync(expression);

        public async Task<List<T>> GetByDefaultsAsync<T>(Expression<Func<C, bool>> expression, Func<IQueryable<C>, IIncludableQueryable<C, object>>? join = null)
        {
            var entityList = await _service.GetByDefaultsAsync(expression,join);
            var dtoList = _mapper.Map<List<T>>(entityList);

            return dtoList;

        }

        public async Task<T?> GetByDefaultAsync<T>(Expression<Func<C, bool>> expression, Func<IQueryable<C>, IIncludableQueryable<C, object>>? join = null)
        {
            var entity = await _service.GetByDefaultAsync(expression, join);
            var dto = _mapper.Map<T>(entity);

            return dto;
        }

        public async Task<T?> GetByIdAsync<T>(Guid id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
                return default;

            var dto = _mapper.Map<T>(entity);
            return dto;
        }

        public async Task<List<TResult>> GetFilteredListAsync<TResult>(Expression<Func<C, TResult>> select, Expression<Func<C, bool>>? where = null, Func<IQueryable<C>, IOrderedQueryable<C>>? orderBy = null, Func<IQueryable<C>, IIncludableQueryable<C, object>>? join = null) => await _service.GetFilteredList(select, where, orderBy, join);

        public async Task<IDbContextTransaction> BeginTransactionAsync()
            => await _service.BeginTransactionAsync();

        public Task<int> GetCountAsync(Expression<Func<C, bool>>? predicate = null) => _service.CountAsync(predicate);
        
    }
}
