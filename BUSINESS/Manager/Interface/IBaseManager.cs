using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Abstract;
using CORE.Interface;
using DTO.Abstract;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace BUSINESS.Manager.Interface
{
    public interface IBaseManager<A,C>
        where A : IBaseRepository<C>
        where C : BaseEntity

    {
        Task<bool> AddAsync(BaseDTO dto);
        Task<bool> AddEntityAsync(C entity);
        Task<bool> UpdateAsync(BaseDTO dto, Guid id);

        Task<bool> UpdateEntityAsync(C entity);
        Task<bool> DeleteAsync(Guid id);

        Task<T?> GetByIdAsync<T>(Guid id);
        Task<List<T>> GetByDefaultsAsync<T>(Expression<Func<C, bool>> expression, Func<IQueryable<C>, IIncludableQueryable<C, object>>? join = null);

        Task<T?> GetByDefaultAsync<T>(Expression<Func<C, bool>> expression, Func<IQueryable<C>, IIncludableQueryable<C, object>>? join = null);


        Task<bool> AnyAsync(Expression<Func<C, bool>> expression);

       
        Task<List<TResult>> GetFilteredListAsync<TResult>
            (
            
            Expression<Func<C, TResult>> select,

          
            Expression<Func<C, bool>>? where = null,

            
            Func<IQueryable<C>, IOrderedQueryable<C>>? orderBy = null,

           
            Func<IQueryable<C>, IIncludableQueryable<C, object>>? join = null
            );
        Task<int> GetCountAsync(Expression<Func<C, bool>>? predicate = null);
        Task<IDbContextTransaction> BeginTransactionAsync();

    }
}
