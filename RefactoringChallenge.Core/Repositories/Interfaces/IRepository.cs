using RefactoringChallenge.Core.Entities.Interfaces;
using RefactoringChallenge.Core.Models.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RefactoringChallenge.Core.Repositories.Interfaces
{
    public interface IRepository<TEntity, TDTO>
        where TEntity : class, IEntity
        where TDTO : class, IDTO
    {
        IQueryable<TEntity> AsQueryable(bool readOnly = true);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TDTO>> GetAllAsDtoAsync(IQueryable<TEntity> queryable = null);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TDTO> FirstOrDefaultAsDtoAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities);
    }
}
