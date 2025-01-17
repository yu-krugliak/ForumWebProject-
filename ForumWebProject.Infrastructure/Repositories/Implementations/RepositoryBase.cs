﻿using ForumWebProject.Infrastructure.Entities;
using ForumWebProject.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ForumWebProject.Infrastructure.Repositories.Implementations
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity<Guid>
    {
        private readonly DbContext _forumContext;

        protected RepositoryBase(DbContext context)
        {
            _forumContext = context;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _forumContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _forumContext.Set<TEntity>()
                .FindAsync(id);
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            return await _forumContext.Set<TEntity>().AnyAsync(e => e.Id == id);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            _forumContext.Set<TEntity>().Add(entity);
            await _forumContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            _forumContext.Set<TEntity>().Remove(entity);
            await _forumContext.SaveChangesAsync();

            return true;
        }

        public virtual async Task<bool> DeleteByIdAsync(Guid id)
        {
            var record = await _forumContext.Set<TEntity>().FindAsync(id);

            if (record is null)
            {
                return false;
            }

            _forumContext.Set<TEntity>().Remove(record);
            await _forumContext.SaveChangesAsync();

            return true;
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            _forumContext.Set<TEntity>().Update(entity);
            await _forumContext.SaveChangesAsync();

            return true;
        }

        public virtual async Task<int> CountItems()
        {
            return await _forumContext.Set<TEntity>().CountAsync();
        }
    }
}
