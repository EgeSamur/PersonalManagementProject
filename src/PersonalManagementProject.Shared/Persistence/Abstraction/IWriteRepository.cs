using PersonalManagementProject.Shared.Domain.Entities;

namespace PersonalManagementProject.Shared.Persistence.Abstraction;

public interface IWriteRepository<TEntity> : IQuery<TEntity>
    where TEntity : BaseEntity
{
    Task AddAsync(TEntity entity);

    Task AddRangeAsync(ICollection<TEntity> entity);

    Task UpdateAsync(TEntity entity);

    Task UpdateRangeAsync(ICollection<TEntity> entity);

    Task DeleteAsync(TEntity entity);

    Task DeleteRangeAsync(ICollection<TEntity> entity);
}