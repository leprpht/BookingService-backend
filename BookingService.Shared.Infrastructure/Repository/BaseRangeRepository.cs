using BookingService.Database;
using BookingService.Shared.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingService.Shared.Infrastructure.Repository;

public abstract class BaseRangeRepository<TModel, TParent>(BookingServiceDbContext context)
    : IBaseRangeRepository<TModel>
    where TModel : class
    where TParent : class
{
    protected readonly BookingServiceDbContext Context = context;
    protected readonly DbSet<TModel> DbSet = context.Set<TModel>();
    protected abstract Expression<Func<TModel, bool>> ByParentId(Guid parentId);
    protected abstract string ParentIdPropertyName { get; }

    public virtual async Task AddRangeAsync(List<TModel> entities)
    {
        if (entities.Count == 0) return;
        await DbSet.AddRangeAsync(entities);
        await Context.SaveChangesAsync();
    }

    public virtual async Task UpdateRangeAsync(List<TModel> entities)
    {
        if (entities.Count == 0)
            throw new ArgumentException("No entities to update.");

        var updateIds = entities
            .Select(e => (Guid)e.GetType().GetProperty("Id")!.GetValue(e)!)
            .ToHashSet();

        var parentId = (Guid)entities.First().GetType().GetProperty(ParentIdPropertyName)!.GetValue(entities.First())!;

        var toDelete = await DbSet
            .Where(ByParentId(parentId))
            .ToListAsync();

        DbSet.RemoveRange(toDelete.Where(e => !updateIds.Contains((Guid)e.GetType().GetProperty("Id")!.GetValue(e)!)));
        DbSet.UpdateRange(entities);
        await Context.SaveChangesAsync();
    }

    public virtual async Task DeleteRangeAsync(Guid parentId)
    {
        var toDelete = await DbSet.Where(ByParentId(parentId)).ToListAsync();
        if (toDelete.Count == 0)
            throw new NotFoundException("No entities found to delete.");

        DbSet.RemoveRange(toDelete);
        await Context.SaveChangesAsync();
    }
}