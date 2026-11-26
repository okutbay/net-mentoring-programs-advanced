using System.Linq.Expressions;

namespace ShopOn.Library.Data.Interfaces;

public interface IRepository<TEntity>
        where TEntity : class
{
    TEntity? GetById(int id);

    /// <summary>
    /// Some example usages:
    /// <code>
    /// var orderById = await orderRepository.GetAsync(o => o.Id == orderId);
    /// var orderByName = await orderRepository.GetAsync(o => o.Customer.Name == customerName);
    /// </code>
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="tracked"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<TEntity?> Get(Expression<Func<TEntity, bool>>? filter = null, bool tracked = true, CancellationToken token = default);

    Task<IEnumerable<TEntity>> GetAll(CancellationToken token = default);

    Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>>? filter = null,
        bool tracked = true, CancellationToken token = default);

    Task Add(TEntity entity, CancellationToken token = default);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    Task<int> SaveChanges(CancellationToken cancellationToken = default);

}
