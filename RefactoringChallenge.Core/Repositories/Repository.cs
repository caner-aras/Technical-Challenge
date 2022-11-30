using Microsoft.EntityFrameworkCore;
using RefactoringChallenge.Core.Entites.Interfaces;
using RefactoringChallenge.Core.Entities;
using RefactoringChallenge.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;
using RefactoringChallenge.Core.Repositories.Interfaces;

namespace RefactoringChallenge.Core.Repositories
{
    public class Repository<TEntity, TDTO> : IRepository<TEntity, TDTO>
        where TEntity : class, IEntity
        where TDTO : class, IDTO
    {
        private readonly NorthwindDbContext _db;
        private readonly DbSet<TEntity> _data;

        public Repository(NorthwindDbContext db)
        {
            _db = db;
            _data = _db.Set<TEntity>();
        }

        public IQueryable<TEntity> AsQueryable(bool readOnly = true)
        {
            return readOnly ? _data.AsNoTracking() : _data;
        }

        public async Task<IEnumerable<TDTO>> GetAllAsDtoAsync(IQueryable<TEntity> queryable = null)
        {
            queryable ??= _data;
            return await queryable.ProjectToType<TDTO>().ToListAsync();
        }

        public async Task<TDTO> FirstOrDefaultAsDtoAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _data.Where(predicate).ProjectToType<TDTO>().FirstOrDefaultAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _data.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _data.AddRangeAsync(entities);
            await _db.SaveChangesAsync();
            return entities;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _data.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _data.FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _data.Remove(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _data.RemoveRange(entities);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
