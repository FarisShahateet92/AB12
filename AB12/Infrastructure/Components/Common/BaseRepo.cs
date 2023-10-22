using AB12.Domain.Base.Common;
using AB12.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AB12.Infrastructure.Components.Common
{
    public class BaseRepo<T> where T : AuditableEntity
    {
        private readonly AppDbContext _context;
        public BaseRepo(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> GetByIdAsync(string id, bool includeSoftDeleted = false)
        {
            if (string.IsNullOrEmpty(id))
                throw new Exception($"id is required");

            var query = _context.Set<T>().Where(x => x.ID == id);

            if (!includeSoftDeleted)
                query = query.Where(x => x.DeletedAt == null);

            var entity = await query.FirstOrDefaultAsync();

            if (entity == null)
                throw new Exception($"ID = {id} not found.");

            return entity;
        }

        public virtual async Task<List<T>> GetAllAsync(bool includeSoftDeleted = false)
        {
            IQueryable<T> query = _context.Set<T>();

            if (!includeSoftDeleted)
                query = query.Where(x => x.DeletedAt == null);

            var entities = await query.ToListAsync();

            return entities;
        }

        public virtual async Task<List<T>> GetAllWithPagingAsync(int pageNumber, int pageSize, bool includeSoftDeleted = false)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                throw new Exception("pageNumber and pageSize must be greater than 0");

            IQueryable<T> query = _context.Set<T>();

            if (!includeSoftDeleted)
                query = query.Where(x => x.DeletedAt == null);

            int itemsToSkip = (pageNumber - 1) * pageSize;

            var entities = await query.Skip(itemsToSkip)
                                      .Take(pageSize)
                                      .ToListAsync();
            return entities;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // Check if the entity exists in the database.
            var existingEntity = await _context.Set<T>().FindAsync(entity.ID);
            if (existingEntity == null)
                throw new Exception($"Entity with ID {entity.ID} not found.");

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public virtual async Task<bool> SoftDeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (!_context.Set<T>().Local.Contains(entity))
                _context.Set<T>().Attach(entity);

            entity.DeletedAt = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }


        public virtual async Task<bool> DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
