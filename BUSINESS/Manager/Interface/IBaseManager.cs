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
        Task<bool> DeleteAsync(Guid id);

        Task<T?> GetByIdAsync<T>(Guid id);
        Task<List<T>> GetByDefaultsAsync<T>(Expression<Func<C, bool>> expression, Func<IQueryable<C>, IIncludableQueryable<C, object>>? join = null);

        Task<T?> GetByDefaultAsync<T>(Expression<Func<C, bool>> expression, Func<IQueryable<C>, IIncludableQueryable<C, object>>? join = null);


        Task<bool> AnyAsync(Expression<Func<C, bool>> expression);

        // TResult genel tip parametresi ile çalışan asenkron bir metot tanımlıyor
        // Geriye TResult tipinde bir liste dönecek ve bu liste bir Task içinde olacak
        Task<List<TResult>> GetFilteredListAsync<TResult>
            (
            // Birinci parametre: Select ifadesi (projeksiyon)
            // C tipindeki nesneleri TResult tipine dönüştüren bir lambda ifadesi
            Expression<Func<C, TResult>> select,

            // İkinci parametre: Where koşulu (filtreleme) - opsiyonel
            // C tipindeki nesneleri filtreleyen bir lambda ifadesi, null olabilir
            Expression<Func<C, bool>>? where = null,

            // Üçüncü parametre: OrderBy ifadesi (sıralama) - opsiyonel
            // IQueryable<C> alıp sıralanmış IOrderedQueryable<C> dönen bir fonksiyon, null olabilir
            Func<IQueryable<C>, IOrderedQueryable<C>>? orderBy = null,

            // Dördüncü parametre: Join ifadesi (ilişkili tablolar) - opsiyonel
            // IQueryable<C> alıp include edilmiş IIncludableQueryable dönen bir fonksiyon, null olabilir
            Func<IQueryable<C>, IIncludableQueryable<C, object>>? join = null
            );
        Task<int> GetCountAsync(Expression<Func<C, bool>>? predicate = null);
        Task<IDbContextTransaction> BeginTransactionAsync();

    }
}
