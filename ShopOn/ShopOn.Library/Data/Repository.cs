using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShopOn.Library.Data.Interfaces;

namespace ShopOn.Library.Data;

public class Repository<TEntity, TContext>(TContext context) : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
{
    protected TContext Context { get; } = context;

    public TEntity? GetById(int id)
    {
        return Set().Find(id);
    }

    public async Task<TEntity?> Get(Expression<Func<TEntity, bool>>? filter = null, bool tracked = true, CancellationToken token = default)
    {
        IQueryable<TEntity> query = Set();

        if (filter != null) query = query.Where(filter);
        if (!tracked) query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(token);
    }

    public async Task<IEnumerable<TEntity>> GetAll(CancellationToken token = default)
    {
        return await Set().ToListAsync(token);
    }

    public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>>? filter = null, 
        bool tracked = true, CancellationToken token = default)
    {
        IQueryable<TEntity> query = Set();

        if (filter != null) query = query.Where(filter);
        if (!tracked) query = query.AsNoTracking();

        return await query.ToListAsync(token);
    }

    public async Task Add(TEntity entity, CancellationToken token = default)
    {
        await Set().AddAsync(entity, token);
    }

    public void Update(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
    }

    public Task<int> SaveChanges(CancellationToken cancellationToken = default)
    {
        return Context.SaveChangesAsync(cancellationToken);
    }

    public void Delete(TEntity entity)
    {
        Set().Remove(entity);
    }

    protected DbSet<TEntity> Set()
    {
        return Context.Set<TEntity>();
    }
}
