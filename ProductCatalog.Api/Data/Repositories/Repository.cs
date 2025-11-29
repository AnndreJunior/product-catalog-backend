using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalog.Api.Data.Repositories;

public abstract class Repository<TEntity, TKey> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;

    protected Repository(AppDbContext context)
    {
        Context = context;
        _dbSet = context.Set<TEntity>();
    }

    protected AppDbContext Context { get; }

    protected virtual async Task<IEnumerable<TEntity>> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>[]? includeFuncs = null)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeFuncs != null)
        { 
            foreach (Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc in includeFuncs)
            {
                query = includeFunc(query);
            }
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }

        return await query.ToListAsync();
    }

    public virtual async Task<TEntity?> GetById(TKey id) => await _dbSet.FindAsync(id);

    public virtual async Task Delete(TKey id)
    {
        TEntity? entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            return;
        }

        Delete(entity);
    }

    public virtual void Delete(TEntity entity) => _dbSet.Remove(entity);

    public virtual void Add(TEntity entity) => _dbSet.Add(entity);

    public virtual void Update(TEntity entity) => _dbSet.Update(entity);
}
